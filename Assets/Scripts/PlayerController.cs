using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    public float speed;
    public GameObject pacdotSpawner;
    public int initRowPosition;
    public int initColumnPosition;
    public GameObject gameController;

    private Rigidbody2D rigidbody2D;
    private Vector3 nextRotation;
    private Vector2 nextVelocity;
    private int turnKind;
    private GameObject turn;

    //      2    
    //  4       1
    //      8

    //  0000...1111
    //  DLUR

    public enum Turn
    {
        //LEFT_RIGHT = 5,
        //UP_DOWN = 12,

        UP_RIGHT = 3,
        UP_LEFT = 6,
        DOWN_LEFT = 12,
        DOWN_RIGHT = 9,

        UP_LEFT_RIGHT = 7,
        DOWN_LEFT_RIGHT = 13,
        UP_DOWN_LEFT = 14,
        UP_DOWN_RIGHT = 11,

        UP_DOWN_LEFT_RIGHT = 15,
    }

    
    bool checkTurn(int turnKind, Vector3 rotation)
    {
        // LEFT
        if (rotation.z == 180)
        {
            return (1 & (turnKind >> 2)) == 1;
        }
        // RIGHT
        else if (rotation.z == 0)
        {
            return (1 & (turnKind >> 0)) == 1;
        }
        // UP
        else if (rotation.z == 90)
        {
            return (1 & (turnKind >> 1)) == 1;
        }
        // DOWN
        else if (rotation.z == 270)
        {
            return (1 & (turnKind >> 3)) == 1;
        }
        return true;
    }

    // Use this for initialization
    void Start()
    {
        gameObject.transform.position = pacdotSpawner.transform.position
            + pacdotSpawner.GetComponent<PacdotSpawner>().spacing * new Vector3(initColumnPosition, -initRowPosition, 0);

        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = Vector2.zero;

        nextRotation = Vector3.zero;
        nextVelocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pacdot")
        {
            if (collision.gameObject.GetComponent<Renderer>().enabled == true)
            {  
                collision.gameObject.GetComponent<Renderer>().enabled = false;
                if (gameObject.transform.position != collision.gameObject.transform.position)
                {
                    animator.SetBool("IsEat", true);
                    gameController.GetComponent<GameController>().IncreaseScore();
                }
            }

            turn = collision.gameObject;
            turnKind = turn.GetComponent<PacdotBehaviour>().GetTurn();
            
            if (System.Enum.IsDefined(typeof(Turn), turnKind))
            {
                gameObject.transform.position = collision.gameObject.transform.position;
                
                if (checkTurn(turnKind, nextRotation))
                {
                    gameObject.transform.rotation = Quaternion.Euler(nextRotation);
                    rigidbody2D.velocity = new Vector2(nextVelocity.x, nextVelocity.y);
                }
                else if (!checkTurn(turnKind, gameObject.transform.rotation.eulerAngles))
                {
                    nextVelocity = Vector2.zero;
                    rigidbody2D.velocity = Vector2.zero;
                }
            }                
        }
    }

    // Update is called once per frame
    void Update () {
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Eat"))
        {
            animator.SetBool("IsEat", false);        
        }

        bool keyPressed = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextRotation = new Vector3(0, 0, 180);
            nextVelocity = speed * Vector2.left;
            keyPressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextRotation = new Vector3(0, 0, 0);
            nextVelocity = speed * Vector2.right;
            keyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextRotation = new Vector3(0, 0, 90);
            nextVelocity = speed * Vector2.up;
            keyPressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextRotation = new Vector3(0, 0, 270);
            nextVelocity = speed * Vector2.down;
            keyPressed = true;
        }

        turnKind = turn.GetComponent<PacdotBehaviour>().GetTurn();       
        if (keyPressed && rigidbody2D.velocity == Vector2.zero && checkTurn(turnKind ,nextRotation)
            || Mathf.Abs(Quaternion.Euler(nextRotation).eulerAngles.z - gameObject.transform.rotation.eulerAngles.z) == 180)
        {         
            gameObject.transform.rotation = Quaternion.Euler(nextRotation);
            rigidbody2D.velocity = new Vector2(nextVelocity.x, nextVelocity.y);
        }
    }
}
