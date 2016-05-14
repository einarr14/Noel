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
			if (level == 1) {
				Application.LoadLevel (4);
			}
            if(level == 4)
            {
                Application.LoadLevel(2);
            }
			if (level == 3) {
				Application.LoadLevel (0);
			}
		}
	}
}
