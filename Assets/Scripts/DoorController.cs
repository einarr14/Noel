using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private GameObject player;
	public float minRange;
	public float maxRange;
	public Text label;

	// Use this for initialization
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
			label.text = "Open";
			return true;
		}
		label.text = "";
		return false;
	}

	public void freezeRotation() {
		rb2d.freezeRotation = true;
	}

	public void unFreezeRotation() {
		rb2d.freezeRotation = false;
	}
}
