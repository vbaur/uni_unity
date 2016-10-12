using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody2D rb;
    private bool isColliding;
    private CircleCollider2D cc;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent< Rigidbody2D >();
        this.cc = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void FixedUpdate () {
        int speed = 5;
        var gate = GameObject.Find("score");

        if (isColliding)
        {
            var heading = gate.transform.position - this.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            rb.velocity = new Vector3(
                direction.x,
                direction.y,
                0
            );
        } else
        {
            var ball = GameObject.Find("ball");
            var ballCc = ball.GetComponent<CircleCollider2D>();
            var ballRadius = ballCc.radius;
            var heading = gate.transform.position - ball.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            var radiusSum = this.cc.radius + ballRadius;
            var radiusTol = this.cc.radius * this.transform.localScale.x + 0.1f;
            var moveToPosition = direction * (-1 * (radiusSum + radiusTol)) + ball.transform.position;
            var hitBallAt = direction * (-1 * radiusSum) + ball.transform.position;
            var enemyToBall = ball.transform.position - this.transform.position;
           
            var vectorTolerance = enemyToBall.normalized - direction;

            var moveDirection = moveToPosition - this.transform.position;

            if (Mathf.Abs(vectorTolerance.x) < 0.1 && Mathf.Abs(vectorTolerance.y) < 0.1)
            {
                moveDirection = hitBallAt - this.transform.position;
            }
            else
            {
                //
                var distToBall = enemyToBall.magnitude - (ballCc.radius * ball.transform.localScale.x) - (this.cc.radius * this.transform.localScale.x);
                if(Mathf.Abs(moveDirection.magnitude - distToBall) > 0.1)
                {
                    var rightVector = new Vector3(-1 * enemyToBall.y, enemyToBall.x);
                    Debug.Log(rightVector);
                    var newPoint = ball.transform.position + (rightVector.normalized * 5);
                    moveDirection = newPoint - this.transform.position;
                }
            }

            moveDirection = moveDirection.normalized * 2;

            rb.velocity = new Vector3(
                moveDirection.x,
                moveDirection.y,
                0
            );
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.isColliding = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        this.isColliding = false;
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("sdfsdfswdfsdfsdf");
    }
}

