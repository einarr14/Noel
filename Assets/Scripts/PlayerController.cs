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
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);
        rb2d.AddForce(Movement*speed);

    }
	// Update is called once per frame
	void Update () {
	
	}
}
