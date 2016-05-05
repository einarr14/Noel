using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;
	public Vector3[] monsterPositions;
	public GameObject[] ghosts;
	public Vector3[] ghostPositions;
	public GameObject[] items;
	public Vector3[] itemPositions;
	public GameObject[] doors;
	public Vector3[] doorPositions;
    public string[] killPhrases;
    public string[] riddles;
    public string[] riddlesAnswer;
    private PlayerHealth playerHealth ;

    // Use this for initialization
    public void initiateLevel (int level) {
		if (level == 1) {
            riddles = new string[2];
            riddlesAnswer = new string[2];
			monsters = new GameObject[3];
			ghosts = new GameObject[2];
			items = new GameObject[3];
			doors = new GameObject[2];
            killPhrases = new string[9];
			monsterPositions = new Vector3[3];
			ghostPositions = new Vector3[2];

			itemPositions = new Vector3[3];
			doorPositions = new Vector3[2];

            riddles[0] = "";
            riddles[1] = "";
            riddlesAnswer[0] = "";
            riddlesAnswer[1] = "";


            monsters[0] = GameObject.Find ("Spider1");
			monsters[1] = GameObject.Find ("Spider2");
			monsters[2] = GameObject.Find ("SpiderQueen");

			ghosts[0] = GameObject.Find ("Ghost1");
			ghosts[1] = GameObject.Find ("Ghost2");

			doors [0] = GameObject.Find ("Door");
			doors [1] = GameObject.Find ("Door2");

			items [0] = GameObject.Find ("Crate");
			items [1] = GameObject.Find ("SpiderQueenWeb");
			items [2] = GameObject.Find ("Door1");

            killPhrases[0] = "Savage garden";
            killPhrases[1] = "Murdertrain";
            killPhrases[2] = "Poppy";
            killPhrases[3] = "John Doe";
            killPhrases[4] = "Yo mama";
            killPhrases[5] = "Elderly bingonight";
            killPhrases[6] = "Zoolander 2";
            killPhrases[7] = "Hot chocolate";
            killPhrases[8] = "Charlie's angels";
            

            for (int i = 0; i < monsters.Length; i++) {
				monsterPositions [i] = monsters [i].transform.position;
			}
			for (int i = 0; i < ghosts.Length; i++) {
				ghostPositions [i] = ghosts [i].transform.position;
			}
			for (int i = 0; i < items.Length; i++) {
				itemPositions [i] = items [i].transform.position;
			}
			for (int i = 0; i < doors.Length; i++) {
				doorPositions [i] = doors [i].transform.position;
		
				doors [i].GetComponent<DoorController> ().freezeRotation ();
			}
		}
	}

	public void resetLevel () {
		for (int i = 0; i < monsters.Length; i++) {
			monsters [i].transform.position = monsterPositions [i];
		}
		for (int i = 0; i < ghosts.Length; i++) {
			ghosts [i].transform.position = ghostPositions [i];
		}
		for (int i = 0; i < items.Length; i++) {
			items [i].transform.position = itemPositions [i];
		}
		for (int i = 0; i < doors.Length; i++) {
			doors [i].transform.position = doorPositions [i];
			doors [i].GetComponent<DoorController> ().freezeRotation ();
			doors [i].GetComponent<DoorController> ().resetRotation ();
		}
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerHealth.setHealth();
		playerHealth.fillHealth();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
