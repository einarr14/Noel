using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public bool underAttack = false;
	public bool immobile;
    private float MoveVertical;
    private float MoveHorizontal;
    private char typechar;
	public float timeScale;


    GameManager gameManager;

    BoardManager boardManager;
    private string killchar = "";

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
		timeScale = 1;
		immobile = false;
    }
    void FixedUpdate()
    {
		Time.timeScale = timeScale;
        if (!GameManager.instance.ghostpause)
        {
            move();
        }
    }
    // Update is called once per frame
    void Update()
    {
		rb2d.velocity = new Vector2(0F, 0F);
        if (!GameManager.instance.ghostpause)
        {
            underAttack = false;
            for (int i = 0; i < boardManager.monsters.Length; i++)
            {
                if (boardManager.monsters[i].GetComponent<UnitController>().inRange())
                {
                    if (boardManager.monsters[i].GetComponent<UnitController>().hostile)
                    {
                        if (!boardManager.monsters[i].GetComponent<UnitController>().timeSlowed)
                        {
                            StartCoroutine(slowTime(0.5F, 0.5F));
                            boardManager.monsters[i].GetComponent<UnitController>().timeSlowed = true;
                        }
                    }
					
                    underAttack = true;
                }
            }
            if (underAttack)
            {
                return;
            }
            underAttack = false;
        }
    }
	
	private IEnumerator slowTime (float slowTo, float duration) {
		timeScale = 1;
		HOTween.To (this, duration, "timeScale", slowTo);
		yield return new WaitForSeconds (duration);

		HOTween.To (this, duration, "timeScale", 1);
	}


    public bool getUnderAttack()
    {
        return underAttack;
    }


    public void kill(string killword)
    {
        killword = killword.ToUpper();

        for (int i = 0; i < boardManager.monsters.Length; i++)
        {
            if ("ChaseGhost"  != boardManager.monsters[i].GetComponent<UnitController>().getType() )
            {
                if (boardManager.monsters[i].GetComponent<UnitController>().inRange())
                {
                    for (int j = 0; j < killword.Length; j++)
                    {
                        if (killword[j] == boardManager.monsters[i].GetComponent<UnitController>().getChar())
                        {

                            boardManager.monsters[i].GetComponent<UnitController>().increaseLetters();

                        }
                    }
                }
            }
        }
    }
    public void answerghost(string sentence)
    {
        for (int i = 0; i < boardManager.monsters.Length; i++)
        {
            if (boardManager.monsters[i].GetComponent<UnitController>().getType() == "ChaseGhost" || boardManager.monsters[i].GetComponent<UnitController>().getType() == "BlockGhost")
            {
                if (boardManager.monsters[i].GetComponent<UnitController>().inRange())
                {
                    if (sentence.ToLower().Contains(boardManager.monsters[i].GetComponent<UnitController>().getAnswer()))
                    {
                        boardManager.monsters[i].GetComponent<UnitController>().increaseHealth();
                        boardManager.monsters[i].GetComponent<UnitController>().eliminate();
                    }
                    else
                    {
                        boardManager.monsters[i].GetComponent<UnitController>().damagePlayer();
                        if (boardManager.monsters[i].GetComponent<UnitController> ().getType() == "ChaseGhost")
                        {
                            boardManager.monsters[i].GetComponent<UnitController>().eliminate();
						} else if (boardManager.monsters[i].GetComponent<UnitController> ().getType() == "BlockGhost") {
							boardManager.monsters [i].GetComponent<UnitController> ().reset ();
						}
                    }
                }  
            }
        }
    }
    private void move() // direction should shuld be 1 or -1 to determine the direction
    {
		if (immobile)
		{
			return;
		}
        Vector2 currPoint = rb2d.position;
		MoveHorizontal = 0;
		MoveVertical = 0;
			if (Input.GetKey (KeyCode.RightArrow)) {
				MoveHorizontal++;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				MoveHorizontal--;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				MoveVertical++;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				MoveVertical--;
			}

        
        if (MoveHorizontal != 0 || MoveVertical != 0)
        {
            float Direction = Mathf.Sqrt(MoveHorizontal * MoveHorizontal + MoveVertical * MoveVertical);
            MoveHorizontal = MoveHorizontal / Direction;
            MoveVertical = MoveVertical / Direction;
            Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical).normalized;
            Vector2 newPosition = currPoint + speed * Movement;
            rb2d.MovePosition(newPosition);
            if (MoveVertical > 0)
            {
                rb2d.rotation = ((180 + (Mathf.Acos(MoveHorizontal) * 360 / 3.14F)) / 2) + 180;
            }
            else
            {
                rb2d.rotation = ((180 - (Mathf.Acos(MoveHorizontal) * 360 / 3.14F)) / 2) + 180;
            }
        }
    }

	public void immobilize () {
		immobile = true;
	}

	public void mobilize () {
		immobile = false;
	}

}

	

