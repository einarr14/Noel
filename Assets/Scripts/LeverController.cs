using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeverController : MonoBehaviour {
	private AudioSource source;
    GameManager gameManager;

	void Awake () {
		source = GameObject.Find("Door1").GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("yo");
		if (other.name == "Crate" || other.name == "Crate2") {
            Debug.Log("hi");
            if (SceneManager.GetActiveScene().buildIndex != 5)
            {
                GameObject.Find("Door1").transform.position = new Vector3(1000, 1000, 1000);
                source.Play();
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    GameObject.Find("4th").transform.position = new Vector3(1000, 1000, 1000);
                }
            }
            else
            {
                Debug.Log(gameManager.teleportLeverCount);
                gameManager.teleportLeverCount++;
                if(gameManager.teleportLeverCount == 2)
                {
                    GameObject.Find("Door1").transform.position = new Vector3(1000, 1000, 1000);
                    source.Play();
                }
            }
			
		}
	}
}
