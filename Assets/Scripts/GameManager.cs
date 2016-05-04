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
	public GameObject player;
	public GameObject health;
	public GameObject manager;
	private int level = 1;

	//Awake is always called before any Start functions
	void Awake()
	{
		/*//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		manager = GameObject.Find ("BoardManager");
		boardManager = manager.GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();*/
	}

	void Start () {
		manager = GameObject.Find ("BoardManager");
		boardManager = manager.GetComponent<BoardManager>();
		Debug.Log (boardManager);
		player = GameObject.Find ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerController = player.GetComponent<PlayerController> ();
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

	}
}