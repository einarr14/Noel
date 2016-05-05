using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;
	public Vector3[] monsterPositions;
	public GameObject[] ghosts;
	public Vector3[] ghostPositions;
	public GameObject[] items;
	public Vector3[] itemPositions;
    public string[] killPhrases;
    
	// Use this for initialization
	public void initiateLevel (int level) {
		if (level == 1) {
            
			monsters = new GameObject[3];
			ghosts = new GameObject[2];
			items = new GameObject[5];
            killPhrases = new string[9];
			monsterPositions = new Vector3[3];
			ghostPositions = new Vector3[2];
			itemPositions = new Vector3[5];
            
            monsters[0] = GameObject.Find ("Spider1");
			monsters[1] = GameObject.Find ("Spider2");
			monsters[2] = GameObject.Find ("SpiderQueen");

			ghosts[0] = GameObject.Find ("Ghost1");
			ghosts[1] = GameObject.Find ("Ghost2");

			items [0] = GameObject.Find ("Door");
			items [1] = GameObject.Find ("Door1");
			items [2] = GameObject.Find ("Door2");
			items [3] = GameObject.Find ("Crate");
			items [4] = GameObject.Find ("SpiderQueenWeb");

            killPhrases[0] = "Chicks with dicks.";
            killPhrases[1] = "Murdertrain.";
            killPhrases[2] = "Poppy";
            killPhrases[3] = "John Doe";
            killPhrases[4] = "Yo mama";
            killPhrases[5] = "Elderly bingonight.";
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
