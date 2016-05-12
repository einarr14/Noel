using UnityEngine;
using System.Collections;

public class CoffinController : UnitController {

	private Animator animator;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		base.Start ();
		animator = GetComponent<Animator>();
		source = GetComponent<AudioSource> ();
		reset ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}


	protected override void wordAction ()
	{
		GameObject.Find ("Bat").transform.position = this.transform.position;
		source.Play ();
		animator.SetBool("open", true);
		privateText.text = "";
		word = "";
		wordDone = "";
		wordLeft = word;
	}

	public override void reset ()
	{
		rb2d.transform.position = originalPosition;
		animator.SetBool("open", false);
		word = "UNLOCK";
		wordLeft = word;
		wordDone = "";
		timeSlowed = true;
		hostile = false;
	}
}
