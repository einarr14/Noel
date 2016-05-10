using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool isPaused;
	public GameObject pauseMenuCanvas;
	public InputField input;
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
			input.Select ();
			input.ActivateInputField();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
		}
	}

	public void Resume() {
		isPaused = false;
	}

	public void Mute() {
		
	}

	public void MainMenu() {
		Application.LoadLevel (0);
	}
}
