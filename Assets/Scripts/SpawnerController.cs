using UnityEngine;
using System.Collections;

public class SpawnerController : UnitController {

	bool spawned;
	public int numSpiders;
	public float delay;

	public GameObject spider;

	// Use this for initialization
	void Start () {
		base.Start ();
		spawned = false;
		timeSlowed = true;
		word = "";
		wordDone = word;
		wordLeft = word;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if (!spawned) {
			if (distance < maxRange) {
				StartCoroutine (StartSpawning ());
				spawned = true;
			}
		}
	}

	IEnumerator StartSpawning() {
		int i = 0;
		while (i < numSpiders) {
			GameObject newSpider = Instantiate (spider);
			newSpider.transform.position = this.transform.position;
			Debug.Log ("SPAWNING");
			yield return new WaitForSeconds (delay);
			i++;
		}
	}
}
