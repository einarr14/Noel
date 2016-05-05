using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;
	public Vector3[] monsterPositions;
    public string[] killPhrases;

	// Use this for initialization
	public void initiateLevel (int level) {
		if (level == 1) {
			monsters = new GameObject[3];
            killPhrases = new string[9];
			monsterPositions = new Vector3[3];
			monsters[0] = GameObject.Find ("Spider1");
			monsters[1] = GameObject.Find ("Spider2");
			monsters[2] = GameObject.Find ("SpiderQueen");
            killPhrases[0] = "Chicks with dicks.";
            killPhrases[1] = "Murdertrain.";
            killPhrases[2] = "Poppy";
            killPhrases[3] = "John Doe";
            killPhrases[4] = "Yo mama";
            killPhrases[5] = "Elderly bingonight.";
            killPhrases[6] = "Zoolander 2";
            killPhrases[7] = "Hot chocolate";
            killPhrases[8] = "Charlie's angels";

            //monsters[3] = GameObject.Find ("Ghost1");
            //monsters[4] = GameObject.Find ("Ghost2");
            for (int i = 0; i < monsters.Length; i++) {
				monsterPositions [i] = monsters [i].transform.position;
			}
		}
	}

	public void resetLevel () {
		for (int i = 0; i < monsters.Length; i++) {
			monsters [i].transform.position = monsterPositions [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
