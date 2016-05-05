using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toMainScene() {
		Application.LoadLevel (1);
	}

	public void quitApplication() {
		Application.Quit();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			Application.LoadLevel (0);
		}
	}
}
