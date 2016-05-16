using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;

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
    public AudioSource success;
    public AudioSource failure;


    GameManager gameManager;

    BoardManager boardManager;
    private string killchar = "";

    // Use this for initialization
    void Start()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        success = audio[2];
        failure = audio[3];
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
			foreach (UnitController unit in boardManager.units )
            {
                if (unit.inRange())
                {
                    if (unit.hostile)
                    {
                        if (!unit.timeSlowed)
                        {
                            StartCoroutine(slowTime(0.8F, 0.3F));
                            unit.timeSlowed = true;
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
		UnitController[] localUnits = new UnitController[boardManager.units.Count];
		boardManager.units.CopyTo (localUnits);
		foreach (UnitController unit in localUnits )
        {
            if ("ChaseGhost"  != unit.getType() )
            {
                if (unit.inRange())
                {
                    for (int j = 0; j < killword.Length; j++)
                    {
                        if (killword[j] == unit.getChar())
                        {

                            unit.increaseLetters();

                        }
                    }
                }
            }
        }
    }
    public void answerghost(string sentence)
    {
		UnitController[] localUnits = new UnitController[boardManager.units.Count];
		boardManager.units.CopyTo (localUnits);
		foreach (UnitController unit in localUnits )
        {
            if (unit.getType() == "ChaseGhost" || unit.getType() == "BlockGhost")
            {
                if (unit.inRange())
                {
                    if (sentence.ToLower().Contains(unit.getAnswer()))
                    {
                        success.Play();
                        unit.increaseHealth();
                        unit.eliminate();
                    }
                    else
                    {
                        failure.Play();
                        unit.damagePlayer();
                        if (unit.getType() == "ChaseGhost")
                        {
                            unit.eliminate();
						} else if (unit.getType() == "BlockGhost") {
							unit.reset ();
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

	public void teleport(Vector3 newPos) {
		rb2d.MovePosition (newPos);
	}
}

	

