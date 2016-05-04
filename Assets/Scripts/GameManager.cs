// reference: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	PlayerHealth playerHealth;
	PlayerController playerController;
	BoardManager boardManager;
	Animator anim;
	float restartTimer;    

	private int level = 1;

	//Awake is always called before any Start functions
	void Awake()
	{
	}

	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager>();
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		anim = GetComponent<Animator> ();
		InitGame ();

	}

	//Initializes the game for each level.
	void InitGame()
	{
		boardManager.initiateLevel(level);
	}



	//Update is called every frame.
	void Update()
	{
		if (playerHealth.currentHealth < 1) {
			Debug.Log ("here");
			gameOver ();
		}

	}

	void gameOver() {
		playerHealth.GetComponent<Renderer> ().enabled = false;
		anim.SetTrigger ("GameOver");
	}
}