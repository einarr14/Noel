using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb2d;
	public bool underAttack = false;

	GameManager gameManager;
	BoardManager boardManager;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		boardManager = GameObject.Find ("BoardManager").GetComponent<BoardManager> ();
	}
    void FixedUpdate()
    {
        Vector2 currPoint = rb2d.position;
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
		if (MoveHorizontal != 0 || MoveVertical != 0) {
			float Direction = Mathf.Sqrt (MoveHorizontal * MoveHorizontal + MoveVertical * MoveVertical);
			MoveHorizontal = MoveHorizontal / Direction;
			MoveVertical = MoveVertical / Direction;
			Vector2 Movement = new Vector2 (MoveHorizontal, MoveVertical);
			Vector2 newPosition = currPoint + speed * Movement;
			rb2d.MovePosition (newPosition);
			if (MoveVertical > 0) {
				rb2d.rotation = ((180 + (Mathf.Acos (MoveHorizontal) * 360 / 3.14F)) / 2) + 180;
			} else {
				rb2d.rotation = ((180 - (Mathf.Acos (MoveHorizontal) * 360 / 3.14F)) / 2) + 180;
			}
			rb2d.velocity = new Vector2(0F, 0F);
    	}
	}
	// Update is called once per frame
	void Update () {
        underAttack = false;
		for (int i = 0; i < boardManager.monsters.Length; i++) {
			if (boardManager.monsters [i].GetComponent<SpiderController>().inRange() ){
				underAttack = true;
    
                string killchar = Input.compositionString;
                if (killchar == boardManager.monsters[i].GetComponent<SpiderController>().label.text)
                {
                    boardManager.monsters[i].GetComponent<SpiderController>().eliminate();
                }
			}
		}
        if(underAttack)
        {
            return;
        }
		underAttack = false;
	}

	public bool getUnderAttack() {
		return underAttack;
	}

}