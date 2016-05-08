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
        input.Select();
        input.ActivateInputField();
    }
	
	// Update is called once per frame
	void Update () {
		
		
			string sentence = input.text;
			input.text = "";
            GameObject.Find("Player").GetComponent<PlayerController>().kill(sentence);
			GameObject.Find ("Player").GetComponent<PlayerController> ().openDoor (sentence);
      
		
	}
}
