using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	GameManager gameManager;
	BoardManager boardManager;
	public InputField input;
	public InputField otherInput;
	private bool writing = false;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && writing == false) {
			writing = true;
			GameObject.Find ("Player").GetComponent<PlayerController> ().immobile = true;
			input.Select ();
			input.ActivateInputField ();
		} 
		if (Input.GetKeyDown ("return") && writing == true) {
			writing = false;
			GameObject.Find ("Player").GetComponent<PlayerController> ().immobile = false;
			string sentence = input.text;
			input.text = "";
			otherInput.Select ();
			otherInput.ActivateInputField ();
            GameObject.Find("Player").GetComponent<PlayerController>().kill(sentence);
			GameObject.Find ("Player").GetComponent<PlayerController> ().openDoor (sentence);
            GameObject.Find("Player").GetComponent<PlayerController>().answerghost(sentence);
        }
		else if (Input.GetKeyDown("return")) {
			otherInput.Select ();
			otherInput.ActivateInputField ();
		}
	}
}
