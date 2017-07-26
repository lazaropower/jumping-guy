using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject game; 
	public GameObject enemyGenerator; 
	public AudioClip jumpClip; 
	public AudioClip dieClip; 

	private Animator animator; 
	private AudioSource audioPlayer; 
	private float startY; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> (); 
		startY = transform.position.y; 
	}
	
	// Update is called once per frame
	void Update () {
		bool gamePlaying = game.GetComponent<GameController> ().gameState == GameState.Playing;
		bool userAction = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);
		bool isGrounded = transform.position.y == startY; 
			
		if (gamePlaying && isGrounded && userAction) {
			updateState ("PlayerJump"); 
			audioPlayer.clip = jumpClip; 
			audioPlayer.Play (); 
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
			game.SendMessage ("ResetTimeScale", .5f); 
			game.GetComponent<AudioSource> ().Stop(); 
			audioPlayer.clip = dieClip; 
			audioPlayer.Play (); 
		}
	}

	void GameReady() {
		game.GetComponent<GameController> ().gameState = GameState.Ready; 
	}
}
