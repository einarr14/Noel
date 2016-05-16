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
    public int leverColorCount;
    public int teleportLeverCount;
	private bool dead;

    private int level;

	//Awake is always called before any Start functions
	void Awake()
	{
        level = SceneManager.GetActiveScene ().buildIndex;
        //level = 1;
	}

	void Start () {
		dead = false;
        instance = GameObject.FindObjectOfType<GameManager>();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager>();
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		//anim = GetComponent<Animator> ();
		initialPosition = GameObject.Find ("Player").transform.position;
		InitGame ();
        ghostpause = false;
        leverColorCount = 0;
        teleportLeverCount = 0;

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
			StartCoroutine(gameOver ());
		}

	}

	IEnumerator gameOver() {
		dead = true;
		GameObject.Find ("Point light").GetComponent<LightController> ().fadeOut();
		yield return new WaitForSeconds(2F);
		boardManager.resetLevel ();
		resetPlayer ();
		dead = false;
	}

	void resetPlayer() {
		//GameObject.Find ("Player").transform.position = initialPosition;
		if (boardManager.checkpoint == true) {
			Vector3 newPos = new Vector3 (-21, -15, 0);
			playerController.teleport (newPos);
		}
		dead = false;
		playerController.mobilize ();
	}
    
    public void ghostscreen()
    {
        ghostpause = !ghostpause;
    }

	public bool playerDead() {
		return dead;
	}
}