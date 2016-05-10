using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorController : UnitController {

	public float openTimeDelay;
	public float openRotation;
	private AudioSource source;

	// Use this for initialization
    void Awake ()
    {
		source = GetComponent<AudioSource> ();
    }
	protected void Start () {
		base.Start ();
		rb2d.freezeRotation = true;
		reset ();
        
	}

	// Update is called once per frame
	protected void Update () {
		base.Update ();
		if (inRange ()) {
		}
	}

	public override void reset() {
		word = "OPEN";
		wordDone = "";
		wordLeft = word;
		rb2d.rotation = 0;
		rb2d.transform.position = originalPosition;
	}

	protected override void wordAction ()
	{
		StartCoroutine (rotateDoor () );
	}

	private IEnumerator rotateDoor () {
		source.Play ();
		while (Mathf.Abs(rb2d.rotation - openRotation) > 1) {
			if (openRotation >= rb2d.rotation) {
				rb2d.rotation += 1;
			} else {
				rb2d.rotation -= 1;
			}
			yield return new WaitForSeconds (openTimeDelay);
		}
	}
}
