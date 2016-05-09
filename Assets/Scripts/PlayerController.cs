﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public bool underAttack = false;
    public bool immobile = false;
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


    }
    void FixedUpdate()
    {
        if (immobile)
        {
            return;
        }
        if (!GameManager.instance.ghostpause)
        {
            move(1,typechar);
        }
    }
    // Update is called once per frame
    void Update()
    {
		Time.timeScale = timeScale;
		//rb2d.velocity = new Vector2(0F, 0F);
        if (!GameManager.instance.ghostpause)
        {
            underAttack = false;
            for (int i = 0; i < boardManager.monsters.Length; i++)
            {
                if (boardManager.monsters[i].GetComponent<SpiderController>().inRange())
                {
					if (!boardManager.monsters[i].GetComponent<SpiderController>().timeSlowed) {
						Debug.Log ("SLowint time");
						StartCoroutine(slowTime(0.2F, 1F));
						boardManager.monsters [i].GetComponent<SpiderController> ().timeSlowed = true;
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
            if (boardManager.monsters[i].GetComponent<SpiderController>().inRange())
            {
                typechar = boardManager.monsters[i].GetComponent<SpiderController>().getChar();
                for (int j = 0; j<killword.Length; j++)
                {
                    if (killword[j] == boardManager.monsters[i].GetComponent<SpiderController>().getChar())
                    {
                        
                        boardManager.monsters[i].GetComponent<SpiderController>().increaseLetters();
                       
                    }
                }
            }
        }
    }

	public void openDoor(string open) 
	{
        open = open.ToUpper();
		for (int i = 0; i < boardManager.doors.Length; i++) 
		{
			if (boardManager.doors [i].GetComponent<DoorController> ().inRange()) 
			{
                for (int j = 0; j < open.Length; j++)
                {
                    if (open[j] == boardManager.doors[i].GetComponent<DoorController>().getChar())
                    {
                        
                        boardManager.doors[i].GetComponent<DoorController>().increaseLetters();
                    }
                }
			}
		}
	}
    public void askghost(string ask)
    {
         ask = ask.ToUpper();
            for (int i = 0; i < boardManager.ghosts.Length; i++)
            {
                if (boardManager.ghosts[i].GetComponent<GhostController>().type == "block")
                { 
                    if (boardManager.ghosts[i].GetComponent<GhostController>().inRange())
                    {
                        typechar = boardManager.ghosts[i].GetComponent<GhostController>().getChar();
                        for (int j = 0; j < ask.Length; j++)
                        {
                            if (ask[j] == boardManager.ghosts[i].GetComponent<GhostController>().getChar())
                            {
                                
                                boardManager.ghosts[i].GetComponent<GhostController>().increaseLetters();
                            }
                        }
                    }
                }
            
            }
    }

    public void answerghost(string sentence)
    {
        for (int i = 0; i < boardManager.ghosts.Length; i++)
        {
            if (boardManager.ghosts[i].GetComponent<GhostController>().inRange())
            {
                if (sentence.ToLower().Contains(boardManager.ghosts[i].GetComponent<GhostController>().answer.text))
                {
                    boardManager.ghosts[i].GetComponent<GhostController>().increaseHealth();
                    boardManager.ghosts[i].GetComponent<GhostController>().eliminate();
                   
                    

                }
                else
                {
                    boardManager.ghosts[i].GetComponent<GhostController>().damagePlayer();
                    if (boardManager.ghosts[i].GetComponent<GhostController>().type != "block")
                    {
                        boardManager.ghosts[i].GetComponent<GhostController>().eliminate();
                    }
                    
                }
            }
        }
    }
    private void move(int direction, char killChar) // direction should shuld be 1 or -1 to determine the direction
    {
        Vector2 currPoint = rb2d.position;
		MoveHorizontal = 0;
		MoveVertical = 0;
        if (killChar != 'A' || killChar != 'D')
        {
			if (Input.GetButton ("Right")) {
				MoveHorizontal++;
			}
			if (Input.GetButton ("Left")) {
				MoveHorizontal--;
			}
        }

        if (killChar != 'W' || killChar != 'D')
        {
			if (Input.GetButton ("Up")) {
				MoveVertical++;
			}
			if (Input.GetButton ("Down")) {
				MoveVertical--;
			}
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
}

	

