using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = rb.transform.position;

        if(pos.y > 8.6f && pos.y < 12.67f)
        {
            pos.x = Mathf.Clamp(pos.x, -27.25f, -2.17f);
            pos.y = Mathf.Clamp(pos.y, 8.6f, 12.67f);
        }
        else
        {
            pos.x = Mathf.Clamp(pos.x, -26.09f, -3.54f);
            pos.y = Mathf.Clamp(pos.y, 3.3f, 18.09f);
        }

        rb.transform.position = pos;
    }
}
