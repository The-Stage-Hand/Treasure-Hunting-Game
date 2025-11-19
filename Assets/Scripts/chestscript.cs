using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestscript : MonoBehaviour {


	Animator anim;
	public GameObject popup;

	// Use this for initialization
	void Start() {
		anim = GetComponent<Animator>();
	
	}

	// Update is called once per frame
	void Update() {
	
	}



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("opened chest start");
			anim.SetBool("open", true);
			Debug.Log("opened chest end");
		}
	}
}
