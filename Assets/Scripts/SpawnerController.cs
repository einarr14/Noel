using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

	bool spawned;
	bool musicPlayed;
	public int numSpiders;
	public float delay;
	private int numSpawned;
	private bool musicSwapped;

	public GameObject spider;

	// Use this for initialization
	void Start () {
		musicPlayed = false;
		spawned = false;
		numSpawned = 0;
		musicSwapped = false;
	}

	void Update () {
		if (numSpawned == numSpiders && musicSwapped == false) {
			musicSwapped = true;
			StartCoroutine (MusicBack ());
			GameObject.Find ("PanicDoorOpen").transform.position = new Vector3 (1000, -6, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name != "Player") {
			return;
		}
		if (!spawned) {
			GameObject.Find ("PanicDoorClose").transform.position = new Vector3 (-25.5F, -6, 0);
			GameObject.Find ("Audio Source").GetComponent<AudioSource> ().Stop();
			GameObject.Find ("Scream").GetComponent<AudioSource> ().Play();
			GameObject.Find ("Danger Music").GetComponent<AudioSource> ().Play();
			musicPlayed = true;
			StartCoroutine (StartSpawning ());
			spawned = true;
		}
	}

	IEnumerator StartSpawning() {
		int i = 0;
		while (i < numSpiders) {
			string temp = "Spawn" + (i % 3);
			Debug.Log (temp);
			GameObject newSpider = Instantiate (spider);
			newSpider.transform.position = GameObject.Find(temp).transform.position;
			SpiderController ctrl = newSpider.GetComponent<SpiderController> ();
			ctrl.maxRange = 30;
			ctrl.damage = 2;
			yield return new WaitForSeconds (delay);
			i++;
			numSpawned++;
		}
	}

	IEnumerator MusicBack () {
		yield return new WaitForSeconds (2F);
		GameObject.Find ("Audio Source").GetComponent<AudioSource> ().Play();
		GameObject.Find ("Danger Music").GetComponent<AudioSource> ().Stop();
	}
}
