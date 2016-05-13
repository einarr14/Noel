using UnityEngine;
using System.Collections;

public class SpiderWebController : UnitController {

	public float slowRange;
	private float playerSpeed;

	// Use this for initialization
	void Start () {
		base.Start();
		hostile = true;
		reset();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
		shouldSlow ();
	}

	private void shouldSlow () {
		if (distance < slowRange) {
			player.GetComponent<PlayerController> ().immobilize ();
		}
	}

	protected override void wordAction ()
	{
		player.GetComponent<PlayerController> ().mobilize ();
        Vector3 curpos = this.transform.position;
		curpos.y += 1000F;
		this.transform.position = curpos;
		wordDone = "";
		wordLeft = word;
		GameObject.Find ("BoardManager").GetComponent<BoardManager> ().removeUnit (this);
	}

	public override void reset ()
	{
		rb2d.transform.position = originalPosition;
		word = "REMOVE";
		wordLeft = word;
		wordDone = "";
	}
}
