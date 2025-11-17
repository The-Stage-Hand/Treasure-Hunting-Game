using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestscript : MonoBehaviour {


	Animator anim;

	// Use this for initialization
	void Start() {
		anim = GetComponent<Animator>();
	
	}

	// Update is called once per frame
	void Update() {
	
	}



	void OnCollisionEntert2d(Collision collision) 
	{
	if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("opened chest start");
			anim.SetBool("open", true);
			Debug.Log("opened chest end");
		
		}
	
	
	}
}
