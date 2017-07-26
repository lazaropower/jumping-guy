using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject game; 
	public GameObject enemyGenerator; 

	private Animator animator; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		bool gamePlaying = game.GetComponent<GameController> ().gameState == GameState.Playing; 
		if (gamePlaying && (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0))) {
			updateState ("PlayerJump"); 
		}
	}

	public void updateState(string state = null) {
		if (state != null)
			animator.Play (state); 
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			updateState ("PlayerDie");
			game.GetComponent<GameController> ().gameState = GameState.Ended;
			enemyGenerator.SendMessage ("CancelGenerator", true); 
		}
	}

	void GameReady() {
		game.GetComponent<GameController> ().gameState = GameState.Ready; 
	}
}
