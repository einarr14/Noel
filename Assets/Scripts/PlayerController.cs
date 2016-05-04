using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb2d;
	public bool underAttack = false;

	GameManager gameManager;
	BoardManager boardManager;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager> ();
	}
	void FixedUpdate ()
    {
		Vector2 currPoint = rb2d.position;
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);
		Vector2 newPosition = currPoint + speed * Movement;
		rb2d.MovePosition (newPosition);

    }
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < boardManager.monsters.Length; i++) {
			if (boardManager.monsters [i].GetComponent<SpiderController>().inRange() ){
				underAttack = true;
				Debug.Log ("Under Attack");
				return;
			}
		}
		Debug.Log ("Not under Attack");
		underAttack = false;
	}

	public bool getUnderAttack() {
		return underAttack;
	}
}
