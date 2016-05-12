using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	GameManager gameManager;
	BoardManager boardManager;
	public InputField input;
	public InputField otherInput;
	private bool movebox = false;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager> ();
        input.Select();
        input.ActivateInputField();
        Vector3 curpos = input.transform.position;
        curpos.y += 1000F;
        input.transform.position = curpos;
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.ghostpause)
        {
            string sentence = input.text;
            input.text = "";
            GameObject.Find("Player").GetComponent<PlayerController>().kill(sentence);
            //GameObject.Find("Player").GetComponent<PlayerController>().openDoor(sentence);
            //GameObject.Find("Player").GetComponent<PlayerController>().askghost(sentence);
            if (Input.GetKeyDown("return"))
            {
                input.Select();
                input.ActivateInputField();
            }
        }
        else
        {

            if (!movebox)
            {
                Vector3 curpos = input.transform.position;
                curpos.y -= 1000F;
                input.transform.position = curpos;
                movebox = true;
            }
            if (Input.GetKeyDown("return"))
            {

                Vector3 curpos = input.transform.position;
                curpos.y += 1000F;
                input.transform.position = curpos;
                string sentence = input.text;
                input.text = "";
                movebox = false;
                input.Select();
                input.ActivateInputField();
                GameObject.Find("Player").GetComponent<PlayerController>().answerghost(sentence);
                GameManager.instance.ghostscreen();
				Debug.Log ("JALLA");
            }
        }
        




    }
}
