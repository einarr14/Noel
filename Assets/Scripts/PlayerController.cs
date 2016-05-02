using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate ()
    {
		Vector2 currPoint = rb2d.position;
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);
		Vector2 newPosition = currPoint + speed * Movement;
		rb2d.MovePosition (newPosition);

    }
	// Update is called once per frame
	void Update () {
	
	}
}
