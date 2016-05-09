using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private GameObject player;
	public float minRange;
	public float maxRange;
	public Text label;
    private string wordDone;
    private string wordLeft;
    private string unlock;
	public float openTimeDelay;
	public float openRotation;

	// Use this for initialization
    void Awake ()
    {
        unlock = "UNLOCK";
        wordDone = "";
        wordLeft = unlock;
    }
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		player = GameObject.Find ("Player");
        
	}

	// Update is called once per frame
	void Update () {
		inRange ();
	}

	public bool inRange () {
		var playerPoint = player.transform.position;
		Vector2 currPoint = rb2d.position;
		float distance = Mathf.Sqrt (Mathf.Pow ((playerPoint.x - currPoint.x), 2F) + Mathf.Pow ((playerPoint.y - currPoint.y), 2F));
		if (distance < maxRange) {
			label.text = "<color=#800000ff>" + wordDone + "</color>" + wordLeft; ;
			return true;
		}
		label.text = "";
		return false;
	}

	public void resetWord() {
		unlock = "UNLOCK";
		wordDone = "";
		wordLeft = unlock;
	}

	public void freezeRotation() {
		rb2d.freezeRotation = true;
	}

	public void unFreezeRotation() {
		rb2d.freezeRotation = false;
	}

	public void resetRotation() {
		rb2d.rotation = 0;
	}

	private IEnumerator rotateDoor () {
		while (Mathf.Abs(rb2d.rotation - openRotation) > 1) {
			if (openRotation >= rb2d.rotation) {
				rb2d.rotation += 1;
			} else {
				rb2d.rotation -= 1;
			}

			yield return new WaitForSeconds (openTimeDelay);
		}
	}

    public void increaseLetters()
    {

        wordDone = wordDone + wordLeft[0];
        wordLeft = wordLeft.Remove(0, 1);
        if (wordDone == unlock)
        {
			StartCoroutine(rotateDoor());
        }
    }
    public char getChar()
    {
		if (wordLeft.Length > 0) {
			return wordLeft [0];
		}
		return ' ';
    }
}
