using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestscript : MonoBehaviour {


	Animator anim;

	// Use this for initialization
	void Start() {
		anim = GetComponent<Animator>();
		anim.SetTrigger("closed");
	}

	// Update is called once per frame
	void Update() {
		anim.SetTrigger("closed");
	}



	void Oncollisionentert2d(Collision collision) 
	{
	if (collision.gameObject.CompareTag("Player"))
		{
			anim.SetTrigger("open");
		
		}
	
	
	}
}
