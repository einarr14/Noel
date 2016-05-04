using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb2d;
	public bool underAttack = false;
	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
    void FixedUpdate()
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
                rb2d.rotation = ((180 + (Mathf.Acos(MoveHorizontal) * 360 / 3.14F)) / 2)+180;
            }
            else
            {
                rb2d.rotation = ((180 - (Mathf.Acos(MoveHorizontal) * 360 / 3.14F)) / 2)+180;
            }
        }

    }
	// Update is called once per frame
	void Update () {

	}
}