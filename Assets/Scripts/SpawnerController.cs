using UnityEngine;
using System.Collections;

public class SpawnerController : UnitController {

	bool spawned;
	public int numSpiders;
	public float delay;

	GameObject spider;

	// Use this for initialization
	void Start () {
		spawned = false;
	}
	
	// Update is called once per frame
	void Update () {
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
			GameObject newBat = Instantiate (spider);
			newBat.transform.position = this.transform.position;
			yield return new WaitForSeconds (delay);
			i++;
		}
	}
}
