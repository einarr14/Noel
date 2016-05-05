using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Crate") {
			GameObject.Find ("Door1").transform.position = new Vector3 (1000, 1000, 1000);
		}
	}
}
