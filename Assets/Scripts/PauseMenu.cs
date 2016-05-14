using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool isPaused;
	public bool isMuted;
	public GameObject pauseMenuCanvas;
	public InputField input;
	public Text muteText;

	void Start() {
		isPaused = false;
		isMuted = false;
		muteText.text = "Mute";
	}

	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			input.Select ();
			input.ActivateInputField();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
            if(isPaused)
            {
                Time.timeScale = 1f;
            }
			isPaused = !isPaused;
		}
	}

	public void Resume() {
		isPaused = false;
		Time.timeScale = 1f;
	}

	public void Mute() {
		if (!isMuted) {
			AudioListener.volume = 0f;
			muteText.text = "Unmute";
		} else {
			AudioListener.volume = 1f;
			muteText.text = "Mute";
		}

		isMuted = !isMuted;
	}

	public void MainMenu() {
		Application.LoadLevel (0);
	}
}
