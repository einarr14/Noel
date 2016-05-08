using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public bool underAttack = false;
    public bool immobile = false;
    private


    GameManager gameManager;

    BoardManager boardManager;
    private string killchar = "";

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();


    }
    void FixedUpdate()
    {
        if (immobile)
        {
            return;
        }
        if (!GameManager.instance.ghostpause)
        {
            Vector2 currPoint = rb2d.position;
            float MoveHorizontal = Input.GetAxis("Horizontal");
            float MoveVertical = Input.GetAxis("Vertical");
            if (MoveHorizontal != 0 || MoveVertical != 0)
            {
                float Direction = Mathf.Sqrt(MoveHorizontal * MoveHorizontal + MoveVertical * MoveVertical);
                MoveHorizontal = MoveHorizontal / Direction;
                MoveVertical = MoveVertical / Direction;
                Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);
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
                rb2d.velocity = new Vector2(0F, 0F);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.ghostpause)
        {
            underAttack = false;
            for (int i = 0; i < boardManager.monsters.Length; i++)
            {
                if (boardManager.monsters[i].GetComponent<SpiderController>().inRange())
                {
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
                for(int j = 0; j<killword.Length; j++)
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
                   
                        GameManager.instance.ghostscreen();

                }
                else
                {
                    boardManager.ghosts[i].GetComponent<GhostController>().damagePlayer();
                    if (boardManager.ghosts[i].GetComponent<GhostController>().type != "block")
                    {
                        boardManager.ghosts[i].GetComponent<GhostController>().eliminate();
                        GameManager.instance.ghostscreen();
                    }
                }
            }
        }
    }
}

	

