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
    public static int cash = 0;
    public static bool Vulnerable = true;
    public static bool attacking = false;
    public GameObject player;
    public static int CashCount;
    public Text healthText;
    public Text cashText;
    public Color flash;
    public Color normal;
    public int FlashNum;
    public float flashTime;
    public SpriteRenderer mysprite;
    public Text popuptext;
    AudioSource aud;
    public AudioClip hurt, money, swing;
    // Use this for initialization
    void Start()
    {
        healthText.text = "Hp: " + health;
        cashText.text = "cash: " + cash;
        PlayerAnim = GetComponent<Animator>();
        canMove = true;
        CashCount = 0;
    }

    // Update is called once per frame
    void Update() {

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

            if (change != Vector3.zero)
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
        if (health <= 0)
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
        aud.PlayOneShot(swing);
    }

    void OnCollisionEnter2D(Collision2D other)  //FIXME: collisions not registering
    {
        if (other.gameObject.tag == "enemy")
        {
            if (Vulnerable && health > 0)
            {
                Debug.Log("owch begin");
                //take damage
                health--;
                healthText.text = "Hp: " + health;
                //Call knockback sequence
                StartCoroutine(KnockBack());
                Debug.Log("owch end");
                aud.PlayOneShot(hurt);
            }
            else
            {
                GameOver();
            }
        }

        if (other.gameObject.tag == "chest")
        {
            aud.PlayOneShot(money);
            StartCoroutine(treasurepopup());



        }
    }

    IEnumerator KnockBack()
    {
        //play damage sound

        Vulnerable = false;
        //start blinking
        int time = 0;
        while (time < FlashNum)
        {
            mysprite.color = flash;
            yield return new WaitForSeconds(flashTime);
            mysprite.color = normal;
            yield return new WaitForSeconds(flashTime);
            time++;
            Debug.Log(time);
        }
        mysprite.color = normal;
        Vulnerable = true;
    }

    IEnumerator treasurepopup()
    {
        popuptext.text = "you found treasure! \n placeholder text \n worth: n/a";
        cash++;
      cashText.text = "cash: " + cash;
        yield return new WaitForSeconds(5f);
        Destroy(popuptext);

    }


    void GameOver()
    {
        //play death music
        //play fade out animation
        //load retry and quit buttons
    }
}
