using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] monsters;
	public GameObject[] ghosts;
	public GameObject[] items;
	public GameObject[] doors;
    public GameObject[] bats;
    private PlayerHealth playerHealth ;


    // Use this for initialization
    void Awake()
    {
		
    } 
    public void initiateLevel (int level) {
		if (level == 1) {
			monsters = new GameObject[3];
			ghosts = new GameObject[2];
			items = new GameObject[3];
			doors = new GameObject[2];
            bats = new GameObject[1];

            bats[0] = GameObject.Find ("Bat");

			monsters[1] = GameObject.Find ("Spider2");
            monsters[0] = GameObject.Find ("Spider1");
			monsters[2] = GameObject.Find ("SpiderQueen");

			ghosts[0] = GameObject.Find ("Ghost1");
			ghosts[1] = GameObject.Find ("Ghost2");

			doors [0] = GameObject.Find ("Door");
			doors [1] = GameObject.Find ("Door2");

			items [0] = GameObject.Find ("Crate");
			items [1] = GameObject.Find ("SpiderQueenWeb");
			items [2] = GameObject.Find ("Door1");
		}
	}

	public void resetLevel () {
        for (int i = 0; i < bats.Length; i++)
        {
            bats[i].GetComponent<BatController>().reset();
        }
        for (int i = 0; i < monsters.Length; i++) {
			monsters [i].GetComponent<SpiderController> ().reset();
        }
		for (int i = 0; i < ghosts.Length; i++) {
			ghosts [i].GetComponent<GhostController> ().reset ();
		}
//		for (int i = 0; i < items.Length; i++) {
//			items [i].GetComponent<ItemController> ().reset ();
//		}
		for (int i = 0; i < doors.Length; i++) {
			doors [i].GetComponent<DoorController> ().reset ();
		}
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerHealth.setHealth();
		playerHealth.fillHealth();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
