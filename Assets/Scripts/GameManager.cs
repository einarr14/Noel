// reference: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	PlayerHealth playerHealth;
	PlayerController playerController;
	BoardManager boardManager;
	//Animator anim;
	float restartTimer;
	Vector3 initialPosition;
    public bool ghostpause;

    private int level;

	//Awake is always called before any Start functions
	void Awake()
	{
		level = SceneManager.GetActiveScene ().buildIndex;
        Debug.Log(level);
	}

	void Start () {
        instance = GameObject.FindObjectOfType<GameManager>();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager>();
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		//anim = GetComponent<Animator> ();
		initialPosition = GameObject.Find ("Player").transform.position;
		InitGame ();
        ghostpause = false;

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
			gameOver ();
		}

	}

	void gameOver() {
		resetPlayer ();
		boardManager.resetLevel ();
	}

	void resetPlayer() {
		GameObject.Find ("Player").transform.position = initialPosition;
	}
    
    public void ghostscreen()
    {
        ghostpause = !ghostpause;
    }
}