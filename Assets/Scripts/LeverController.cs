using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeverController : MonoBehaviour {
	private AudioSource source;

	void Awake () {
		source = GameObject.Find("Door1").GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Crate") {
			GameObject.Find ("Door1").transform.position = new Vector3 (1000, 1000, 1000);
			source.Play ();
			if (SceneManager.GetActiveScene ().buildIndex == 3) {
				GameObject.Find ("4th").transform.position = new Vector3 (1000, 1000, 1000);
			}
		}
	}
}
