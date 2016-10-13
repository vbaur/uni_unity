using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody2D rb;
    private bool isColliding;
    private CircleCollider2D cc;

	void Start () {
        this.rb = GetComponent< Rigidbody2D >();
        this.cc = GetComponent<CircleCollider2D>();
    }
	

    void FixedUpdate () {
        var gate = GameObject.Find("score");
        const int speed = 5;

        // Surandama kokia trajektorija kamuolys turėtų judėti link vartų:
        var ball = GameObject.Find("ball");
        var heading = gate.transform.position - ball.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (isColliding)
        {
            // Jei kamuolys yra liečiamas ==> toliau judėti link vartų
            rb.velocity = new Vector3(
                direction.x,
                direction.y,
                0
            );
        } else
        {
            var ballCc = ball.GetComponent<CircleCollider2D>();

            // Atitinkamai scale'inam collider'ių spindulius
            var ballRadius = ballCc.radius * ball.transform.localScale.x;
            var enemyRadius = this.cc.radius * this.transform.localScale.x;

            // Sudeda spindulius ir pasiunčiam kamuolį link pozicijos iš kurios būtų galima pradėti jį mušti/varytis link vartų
            var radiusSum = enemyRadius + ballRadius;
            var moveToPosition = direction * (-1.2f * radiusSum) + ball.transform.position;
            var moveDirection = moveToPosition - this.transform.position;

            // Kamuolį norime mušti tik tada kai esame įsitikinę, jog pastumtas jis judės link vartų
            // t.y. DI valdomo priešininko krypties vektorius eis per kamuolio centrą
            // tolerancija reikalinga nes realiomis salygomis tie vektoriai nesutaps, tad norime leisti DI kažkiek klysti
            var enemyToBall = ball.transform.position - this.transform.position;
            var vectorTolerance = enemyToBall.normalized - direction;
            var TOL = 0.1;
            
            if (Mathf.Abs(vectorTolerance.x) < TOL && Mathf.Abs(vectorTolerance.y) < TOL)
            {
                // Jei kryptis yra tolerancijos ribose, tai judame nebe link įsivaizduojamo taško, 
                // tačiau link vartų
                moveDirection = enemyToBall;
                var enemyToGate = gate.transform.position - this.transform.position;

                if(enemyToGate.magnitude < 8)
                {
                    // TODO: perkelt mušimą į vartus į atskirą funkciją, nes ja naudosis ir žaidėjo script'as
                    var ballRB = ball.GetComponent<Rigidbody2D>();
                    ballRB.AddForce(enemyToBall * 2);
                }
            }
            else
            {
                // Jei kryptis nėra tolerancijos ribose, reikia atsistoti į tokią vietą iš kurios būtų galima 
                // mušti/varytis kamuolį link vartų:

                // Taip pat reikia atsižvelti į tai, jog judant link tokios pozicijos, mums gali trukdyti pats kamuolys,
                // o jį liesdami patektume į amžiną ciklą, vis stumdami jį iš netinkamo taško

                // Patikriname ar susidūrimas su kamuoliu įvyktų anksčiau nei pasiektume reikiamą poziciją
                var distToBall = enemyToBall.magnitude - ballRadius - enemyRadius; // Atstumas iki susidūrimo
                // Taip pat naudojame toleranciją dėl tų pačių priežasčių
                if(Mathf.Abs(moveDirection.magnitude - distToBall) > TOL)
                {
                    // Jei numatome susidūrimą, tai reiktų pasitraukti į šoną:

                    // Gauname vektorių statmeną DI krypties link kamuolio vektoriui.
                    // Jį naudosime, kad apeitume kamuolį pro šoną:
                    var rightVector = new Vector3(-1 * enemyToBall.y, enemyToBall.x); // TODO: pakeisti, kad pasirinktų optimalią apėjimo pusę
                    
                    // Naujas taškas yra nuo kamuolio centro į šoną(DI atžvilgiu) nutolęs per |5| <--- tiesiog pasirinkta konstanta
                    var newPoint = ball.transform.position + (rightVector.normalized * 5);
                    moveDirection = newPoint - this.transform.position;
                }
            }

            // Priverčiam rutuliuką judėti mūsų nuspresta kryptime, tam tikru greičiu
            moveDirection = moveDirection.normalized * speed;

            rb.velocity = new Vector3(
                moveDirection.x,
                moveDirection.y,
                0
            );

            // TODO: kliūčių išvengimas, judėjimas link vartų kai kamuolio/DI judėjimas yra ribojamas
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.name == "ball")
            this.isColliding = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        this.isColliding = false;
    }
}

