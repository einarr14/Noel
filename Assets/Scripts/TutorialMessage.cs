using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour {
	public Text tutorialText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		tutorialText.text = other.GetComponent<Text> ().text;
	}
}
