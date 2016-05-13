using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitController : MonoBehaviour {

	protected Rigidbody2D rb2d;
	protected GameObject player;
	protected string word;
	protected string wordLeft;
	protected string wordDone;
	protected float distance;
	protected Vector3 playerPoint;
	protected Vector2 currPoint;
	protected Vector3 originalPosition;
	protected GameObject privateCanvas; 
	protected Text privateText;
	protected Vector3 textPos;
    protected string type;
    public bool hostile;
    public bool timeSlowed;

	public float maxRange;
	public float minRange;
	//public Text label;

	void Awake () {
		
	}

	// Use this for initialization
	protected void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		originalPosition = rb2d.transform.position;
		textPos = new Vector3 (0, 2, 0);
		privateCanvas = new GameObject("privateCanvas", typeof(Canvas));
		privateCanvas.AddComponent<Text>();
		privateText = privateCanvas.GetComponent<Text> ();
		privateText.transform.position = textPos;
		privateCanvas.transform.position = rb2d.transform.position;
		privateCanvas.transform.localScale = new Vector3 (0.01F, 0.01F, 1);
		privateText.font = GameObject.Find ("Canvas").GetComponentInChildren<Text> ().font;
		privateText.alignment = TextAnchor.MiddleCenter;
		privateText.horizontalOverflow = HorizontalWrapMode.Overflow;
		privateText.fontSize = 50;
        timeSlowed = true;
		GameObject.Find ("BoardManager").GetComponent<BoardManager> ().addUnit (this);
	}
	
	// Update is called once per frame
	protected void Update () {
        rb2d.velocity = new Vector2(0F, 0F);
        rb2d.angularVelocity = 0F;
        if (!GameManager.instance.ghostpause) {
			playerPoint = player.transform.position;
			currPoint = rb2d.position;
			distance = Mathf.Sqrt (Mathf.Pow ((playerPoint.x - currPoint.x), 2F) + Mathf.Pow ((playerPoint.y - currPoint.y), 2F));
			privateCanvas.transform.position = textPos + rb2d.transform.position;
		}
	}

	public bool inRange () {
		{
			if (distance <= maxRange)
			{
				privateText.text =  "<color=#800000ff>" + wordDone + "</color>" + wordLeft;
				return true;
			}
			privateText.text = "";
			return false;
		}
	}

	public void increaseLetters() {
		if (wordLeft != "") {
			wordDone = wordDone + wordLeft [0];
			wordLeft = wordLeft.Remove(0, 1);
		}
		if (wordDone == word)
		{
			wordAction();
		}
	}

	protected virtual void wordAction() {
		
	}

	public char getChar() {
		if (wordLeft != "") {
			return wordLeft[0];
		}
		return '\0';
	}

	public virtual void reset () {

	}
    protected virtual void move()
    {

    }
    public virtual void damagePlayer()
    {

    }
    public string getType()
    {
        return type;
    }
    public virtual void increaseHealth()
    {

    }
    public virtual void eliminate()
    {
		GameObject.Find ("BoardManager").GetComponent<BoardManager> ().removeUnit (this);
    }
    public virtual string getAnswer()
    {
        return "";
    }
}
