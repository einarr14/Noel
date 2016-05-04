using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;

	// Use this for initialization
	void Start () {
		
	}

	public void initiateLevel (int level) {
		if (level == 1) {
			monsters = new GameObject[3];
			monsters[0] = GameObject.Find ("Spider1");
			monsters[1] = GameObject.Find ("Spider2");
			monsters[2] = GameObject.Find ("QueenSpider");
			//monsters[3] = GameObject.Find ("Ghost1");
			//monsters[4] = GameObject.Find ("Ghost2");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
