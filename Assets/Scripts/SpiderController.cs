using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiderController : MonoBehaviour {
    public int strength;
	public float speed;
	private Rigidbody2D rb2d;
	public float maxRange;
	public float minRange;
	public float biteRange;
	private bool attacking = false;
	private GameObject player;
	public int damage;
	public float attackSpeed;
	PlayerHealth playerHealth;
    private Animator animator;
    private GUIText killText;
    public Text label;
    private int randnum;
    private BoardManager boardManager;
	private GameObject myCanvas;
	public string canvasName;
    private string theKillWord;
    private string killWordLeft;
    private string killWordDone;
    private bool firsttime;
	public bool timeSlowed = false;
	private AudioSource source;

	void Awake ()
	{
		source = GetComponent<AudioSource> ();
	}

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        randnum = Mathf.FloorToInt(Random.value * 8);
        myCanvas = GameObject.Find(canvasName);
    }

	// Update is called once per frame
	void Update () {
        
		myCanvas.transform.position = this.transform.position;
        if (!GameManager.instance.ghostpause)
        {
            var playerPoint = player.transform.position;
            Vector2 currPoint = rb2d.position;
            float distance = Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y), 2F));
            if (distance < maxRange && distance > minRange)
			{
                float moveY = (playerPoint.y - currPoint.y) / distance;
                float moveX = (playerPoint.x - currPoint.x) / distance;
                Vector2 Movement = new Vector2(moveX, moveY);
                Vector2 newPosition = currPoint + speed * Movement;
                animator.SetBool("SpiderWalk", true);
                //rb2d.MoveRotation (Mathf.Acos (moveX));
                if (moveY > 0)
                {
                    rb2d.rotation = (180 + (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
					//myCanvas.transform.RotateAround(this.transform.position, new Vector3(0,0,1),0);
                }
                else
                {
                    rb2d.rotation = (180 - (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
					//myCanvas.transform.RotateAround(this.transform.position, new Vector3(0,0,1),0);
                }

                rb2d.MovePosition(newPosition);
            }
            else if (distance > maxRange)
            {
                rb2d.velocity = new Vector2(0, 0);
                animator.SetBool("SpiderWalk", false);
            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
            }
            if (distance < biteRange && attacking == false)
            {
                attacking = true;


                BitePlayer();

            }
        }
	}

	private void BitePlayer() {
		StartCoroutine (DoBitePlayer());
	}

	private IEnumerator DoBitePlayer () {
		source.Play ();
		playerHealth.TakeDamage (damage);
        animator.SetTrigger("SpiderBite");
        yield return new WaitForSeconds (attackSpeed);
        
        attacking = false;
	}

	public bool inRange () {
		var playerPoint = player.transform.position;
		Vector2 currPoint = rb2d.position;
		float distance = Mathf.Sqrt (Mathf.Pow ((playerPoint.x - currPoint.x), 2F) + Mathf.Pow ((playerPoint.y - currPoint.y), 2F));
		if (distance < maxRange) {

            label.text =  "<color=#800000ff>" + killWordDone + "</color>" + killWordLeft;
            return true;
            
        }
        label.text = "";
        return false;
	}
    public void eliminate ()
    {
		Vector3 curpos = this.transform.position;
		curpos.y += 1000F;
		this.transform.position = curpos;
        killWordDone = "";
        killWordLeft = theKillWord;
		timeSlowed = false;
    }
    public void increaseLetters()
    {

        killWordDone = killWordDone + killWordLeft[0];
        killWordLeft = killWordLeft.Remove(0, 1);
        if (killWordDone == theKillWord)
        {
            eliminate();
        }
    }
    public char getChar()
    {
        return killWordLeft[0];
    }

    public void initializeWord(string word)
    {
        theKillWord = word;
        Debug.Log(theKillWord);
        killWordLeft = theKillWord;
        killWordDone = "";
		timeSlowed = false;
    }
}


