using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private bool isColliding;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x -= Time.deltaTime * 5;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x += Time.deltaTime * 5;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y += Time.deltaTime * 5;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y -= Time.deltaTime * 5;
            this.transform.position = position;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("sdfsdfswdfsdfsdf");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("sdfsdfswdfsdfsdf");
    }
}

