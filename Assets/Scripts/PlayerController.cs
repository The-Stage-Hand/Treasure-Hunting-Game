using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static Animator PlayerAnim;

    //movement controls
	public static bool canMove;
	public float movespeed = 2f;
    public static int health = 5;
    public static bool Vulnerable = true;

    
	// Use this for initialization
	void Start ()
    {
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

        //Movement controls
        if (canMove == true)
        {
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
            if (Input.GetKey(KeyCode.Space)) //Sword Attack
            {

            }

            //Combat controls
            if (Input.GetKey(KeyCode.Space))
            {
                //Player stops a second
                canMove = false;
                //player does sword animation

                //FIXME:add both an animation for 4 dirictional swinging, as well as starting and stopping hitboxes in the animation

                //player goes back to moving
                canMove = true;
            }
        }
        

        //Death sequence
        if(health <= 0)
        {
            //turn off player boxcollider
            canMove = false;
            //turn off the ui elements
            //play player death animation
            //call gameover sequence
        }
    }

    void onCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && Vulnerable)
        {
            //take damage
            health--;

            //Call knockback sequence
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
