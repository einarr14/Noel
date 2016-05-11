﻿using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;
	
	public GameObject[] items;
	
    
    private PlayerHealth playerHealth ;


    // Use this for initialization
    void Awake()
    {
		
    } 
    public void initiateLevel (int level) {
		if (level == 1) {
			monsters = new GameObject[8];
            items = new GameObject[3];

            
			monsters[1] = GameObject.Find ("Spider2");
            monsters[0] = GameObject.Find ("Spider1");
			monsters[2] = GameObject.Find ("SpiderQueen");
            monsters[3] = GameObject.Find("Bat");

            monsters[4] = GameObject.Find ("Ghost1");
			monsters[5] = GameObject.Find ("Ghost2");

			monsters [6] = GameObject.Find ("Door");
			monsters [7] = GameObject.Find ("Door2");

			items [0] = GameObject.Find ("Crate");
			items [1] = GameObject.Find ("SpiderQueenWeb");
			items [2] = GameObject.Find ("Door1");
		}
		if (level == 3) { 	// Tutorial
			monsters = new GameObject[2];
			monsters [0] = GameObject.Find ("Door");
			monsters [1] = GameObject.Find ("Spider");
		}
	}

	public void resetLevel () {
        
        for (int i = 0; i < monsters.Length; i++) {
			monsters [i].GetComponent<UnitController> ().reset();
        }
//		for (int i = 0; i < items.Length; i++) {
//			items [i].GetComponent<ItemController> ().reset ();
//		}
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerHealth.setHealth();
		playerHealth.fillHealth();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
