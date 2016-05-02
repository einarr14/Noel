using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb2d;
	public int range;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		var playerPoint = GameObject.Find("Player").transform.position;
		Vector2 currPoint = rb2d.position;
		float distance = Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x),2F) + Mathf.Pow((playerPoint.y - currPoint.y), 2F));
		if (distance < range) {
			float moveY = (playerPoint.y - currPoint.y) / distance;
			float moveX = (playerPoint.x - currPoint.x) / distance;
			Vector2 Movement = new Vector2 (moveX, moveY);
			Vector2 newPosition = currPoint + speed * Movement;
			rb2d.MovePosition (newPosition);
		} else {
			rb2d.velocity = new Vector2(0, 0);
		}

	}
}

