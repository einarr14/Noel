using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toMainMenu() {
		Application.LoadLevel (0);
	}

	public void toMainScene() {
		Application.LoadLevel (1);
	}

	public void tutorialScene() {
		Application.LoadLevel (3);
	}

	public void quitApplication() {
		Application.Quit();
	}
}
