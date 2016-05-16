using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour {
	public int level;

	// Use this for initialization
	void Start () {
		level = SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			if (level == 1) { 	// Main
				Application.LoadLevel (4);
			}
			if (level == 3) {	// Tutorial
				Application.LoadLevel (0);
			}
            if (level == 4)		// Level 2
            {
                Application.LoadLevel(5);
            }
            if (level == 5)		// Level 3
            {
                Application.LoadLevel(6);
            }
			if (level == 6) { 	// Level 4
				Application.LoadLevel (2);
			}
		}
	}
}
