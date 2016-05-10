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

	public float maxRange;
	public float minRange;
	public Text label;

	void Awake () {
		
	}

	// Use this for initialization
	protected void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		originalPosition = rb2d.transform.position;
	}
	
	// Update is called once per frame
	protected void Update () {
		if (!GameManager.instance.ghostpause) {
			playerPoint = player.transform.position;
			currPoint = rb2d.position;
			distance = Mathf.Sqrt (Mathf.Pow ((playerPoint.x - currPoint.x), 2F) + Mathf.Pow ((playerPoint.y - currPoint.y), 2F));
		}
	}

	public bool inRange () {
		{
			if (distance <= maxRange)
			{
				label.text =  "<color=#800000ff>" + wordDone + "</color>" + wordLeft;
				return true;
			}
			label.text = "";
			return false;
		}
	}

	public void increaseLetters() {
		wordDone = wordDone + wordLeft[0];
		wordLeft = wordLeft.Remove(0, 1);
		if (wordDone == word)
		{
			wordAction();
		}
	}

	protected virtual void wordAction() {
		
	}

	public char getChar() {
		return wordLeft[0];
	}

	public virtual void reset () {

	}
}
