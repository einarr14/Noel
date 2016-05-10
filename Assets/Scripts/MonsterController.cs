using UnityEngine;
using System.Collections;

public class MonsterController : UnitController {

	protected PlayerHealth playerHealth;

	public float speed;
	public int damage;

	void Awake () {
	}

	// Use this for initialization
	protected void Start () {
		base.Start ();
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	protected virtual void move () {
		//source.Play ();
		float moveY = (playerPoint.y - currPoint.y) / distance;
		float moveX = (playerPoint.x - currPoint.x) / distance;
		Vector2 Movement = new Vector2(moveX, moveY);
		Vector2 newPosition = currPoint + speed * Movement;
		rb2d.MovePosition(newPosition);
	}

//	protected virtual void wordAction() {
//
//	}

	public void damagePlayer()
	{
		playerHealth.TakeDamage(damage);
	}
}
