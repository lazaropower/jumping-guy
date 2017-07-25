using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animator; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0)) {
			updateState ("PlayerJump"); 
		}
	}

	public void updateState(string state = null) {
		if (state != null)
			animator.Play (state); 
	}
}
