using UnityEngine;
using System.Collections;

public class MainPlayerBehaviour : MonoBehaviour {
    private Rigidbody2D rb;
    private GameObject ball;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
        this.ball = GameObject.Find("ball");
    }
	
	// Update is called once per frame
	void Update () {
        float speed = 5;

        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movex * speed, movey * speed);

        Vector3 pos = rb.transform.position;
        pos.x = Mathf.Clamp(pos.x, -27.45f, -2.09f);
        pos.y = Mathf.Clamp(pos.y, 1.81f, 19.37f);
        rb.transform.position = pos;

        if (Input.GetKeyDown("space"))
        {
            var ballRB = this.ball.GetComponent<Rigidbody2D>();
            var meToBall = this.ball.transform.position - rb.transform.position;
            var ballCc = this.ball.GetComponent<CircleCollider2D>();

            if (meToBall.magnitude < (ballCc.radius * ball.transform.localScale.x * 3))
                ballRB.AddForce(meToBall * 100);
        }
    }
}
