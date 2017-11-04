using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public Animator animator;
    public float speed;

    private Rigidbody2D rigidbody2D;
    
	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("isEat", false);

        if (Input.GetKeyDown(KeyCode.S))
            animator.SetBool("isEat", true);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            rigidbody2D.velocity = new Vector2(-speed, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody2D.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            rigidbody2D.velocity = new Vector2(0, speed);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            rigidbody2D.velocity = new Vector2(0, -speed);
        }
    }
}
