using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public enum GameState {Idle, Playing, Ended, Ready};

public class GameController : MonoBehaviour {

	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.2f; 
	public float scaleTime = 6f; 
	public float scaleInc = .25f;
	public float newTimeScale = 1f; 
	public RawImage background; 
	public RawImage platform; 
	public GameObject uiIdle; 
	public GameObject uiScore;
	public GameState gameState = GameState.Idle;
	public GameObject player;
	public GameObject enemyGenerator;
	public Text pointsText; 
	private AudioSource musicPlayer; 
	private int points = 0; 

	// Use this for initialization
	void Start () {
		musicPlayer = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		bool userAction = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);
		//Empieza el juego 
		if (gameState == GameState.Idle && userAction) {
			gameState = GameState.Playing;
			uiIdle.SetActive (false);
			uiScore.SetActive (true);
			player.SendMessage ("updateState", "PlayerRun");
			enemyGenerator.SendMessage ("StartGenerator");
			musicPlayer.Play (); 
			InvokeRepeating ("GameTimeScale", scaleTime, scaleTime);
			player.SendMessage ("DustPlay"); 
		}
		// Juego en marcha
		else if (gameState == GameState.Playing)
			Parallax ();
		// Juego preparado para reiniciarse
		else if (gameState == GameState.Ready) {
			if (userAction) {
				RestartGame (); 
				ResetTimeScale ();
			}
		}

	}

	void Parallax () {
		float finalSpeed = parallaxSpeed * Time.deltaTime; 
		background.uvRect = new Rect (background.uvRect.x + finalSpeed, 0f, 1f, 1f); 
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed, 0f, 1f, 1f); 
	}

	public void RestartGame () {
		SceneManager.LoadScene ("scene"); 
	}

	void GameTimeScale() {
		Time.timeScale = Time.timeScale + scaleInc; 
		Debug.Log("Ritmo incrementado " + Time.timeScale.ToString()); 
	}

	public void ResetTimeScale() {
		CancelInvoke ("GameTimeScale"); 
		Time.timeScale = newTimeScale;  
		Debug.Log ("Ritmo de juego reestablecido " + Time.timeScale.ToString ()); 
	}

	public void IncreasePoints() {
		pointsText.text = (++points).ToString (); 
	}
}
