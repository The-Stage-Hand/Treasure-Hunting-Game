using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Animator PlayerAnim;
    public Vector3 change;
    //movement controls
	public static bool canMove;
	public float movespeed = 2f;
    public static int health = 5;
    public static bool Vulnerable = true;
    public static bool attacking = false;
    public GameObject player;
    public static int CashCount;
    public Text healthText;


    // Use this for initialization
    void Start ()
    {
        healthText.text = "Hp: " + health;
        PlayerAnim = GetComponent<Animator>();
        canMove = true;
        CashCount = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Movement controls
        if (canMove == true)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            //4 way walking movement
            if (Input.GetKey(KeyCode.A)) //Left Movement
            {
                transform.position -=
                new Vector3(movespeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D)) //Right Movement
            {
                transform.position +=
                new Vector3(movespeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.S)) //Down Movement
            {
                transform.position -=
                new Vector3(0, movespeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W)) //Up Movement
            {
                transform.position +=
                new Vector3(0, movespeed * Time.deltaTime);
            }

            if(change != Vector3.zero)
            {
                PlayerAnim.SetFloat("moveX", change.x);
                PlayerAnim.SetFloat("moveY", change.y);
                PlayerAnim.SetBool("moving", true);
            }
            else
            {
                PlayerAnim.SetBool("moving", false);
            }

            //Combat controls
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canMove = false;
                Debug.Log("starting..");
                StartCoroutine(SwingSword());
            }
        }

        //Death sequence
        if(health <= 0)
        {
            //turn off player boxcollider and movement
            canMove = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            //turn off the ui elements
            //play player death animation
            //call gameover sequence
            GameOver();
        }
    }


    private IEnumerator SwingSword()
    {
        PlayerAnim.SetBool("Attack", true);
        yield return new WaitForSeconds(.3f);
        PlayerAnim.SetBool("Attack", false);
        canMove = true;
    }

    void onCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("owch begin");
            //take damage
            health--;
            healthText.text = "Hp: " + health;
            //Call knockback sequence
            Debug.Log("owch end");
        }
    }

     void KnockBack()
    {
        Vulnerable = false;
        
        Vulnerable = true;
    }

    void GameOver()
    {
        //play death music
        //play fade out animation
        //load retry and quit buttons
    }
}
