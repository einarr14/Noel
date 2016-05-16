using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool isPaused;
	public bool isMuted;
	public GameObject pauseMenuCanvas;
	public InputField input;
	public Text muteText;
    private GameObject player;
    private PlayerHealth playerHealth;
    private int difficulty;
    public Text difficultyText;

	void Start() {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
		isPaused = false;
		isMuted = false;
		muteText.text = "Mute";
        //difficulty = 0;
        difficulty = GameObject.Find("SavedVariables").GetComponent<SavedVariables>().getDifficulty();
        initializeDifficulty();

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

    public void initializeDifficulty()
    {
        if (difficulty == 0)
        {
            difficultyText.text = "Difficulty: Normal";
            playerHealth.changeDifficulty(1f);
        }
        else if (difficulty == 1)
        {
            difficultyText.text = "Difficulty: Hard";
            playerHealth.changeDifficulty(1.4f);
        }
        else if (difficulty == 2)
        {
            difficultyText.text = "Difficulty: Extreme";
            playerHealth.changeDifficulty(2f);
        }
        else if (difficulty == 3)
        {
            difficultyText.text = "Difficulty: Easy";
            playerHealth.changeDifficulty(0.5f);
            difficulty = -1;
        }


    }

    public void Difficulty()
    {
        difficulty++;
        GameObject.Find("SavedVariables").GetComponent<SavedVariables>().setDifficulty(difficulty);
        if(difficulty == 0)
        {
            difficultyText.text = "Difficulty: Normal";
            playerHealth.changeDifficulty(1f);
        }
        else if(difficulty == 1)
        {
            difficultyText.text = "Difficulty: Hard";
            playerHealth.changeDifficulty(1.4f);
        }
        else if(difficulty == 2)
        {
            difficultyText.text = "Difficulty: Extreme";
            playerHealth.changeDifficulty(2f);
        }
        else if(difficulty == 3)
        {
            difficultyText.text = "Difficulty: Easy";
            playerHealth.changeDifficulty(0.5f);
            difficulty = -1;
        }
        
        
    }

	public void MainMenu() {
        Time.timeScale = 1f;
		isPaused = false;
		Application.LoadLevel (0);
	}
}
