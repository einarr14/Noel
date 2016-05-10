using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostController : MonsterController {
    public Text answer;
    private string riddle;
    public string type;

	private AudioSource source;

	void Awake ()
	{
		source = GetComponent<AudioSource> ();
	}

    // Use this for initialization
	protected void Start () {
		base.Start ();
		riddle = label.text;
		reset ();
    }
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
        if (!GameManager.instance.ghostpause)
        {
            if (type != "block")
            {
                if (distance < maxRange && distance > minRange)
                {
					source.time = 0.9f;
					source.Play ();
					move ();
                }
                else if (distance <= minRange)
                {
                    GameManager.instance.ghostscreen();
                    label.text = riddle;

                }
                else
                {
                    rb2d.velocity = new Vector2(0, 0);
                }
            }
        }
	}
		
	protected override void wordAction()
    {
		GameManager.instance.ghostscreen();
		label.text = riddle;
		wordLeft = word;
		wordDone = "";
    }

    public void increaseHealth()
    {
        playerHealth.IncreaseHealth(damage);
    }

	public void eliminate()
	{
		Vector3 curpos = this.transform.position;
		curpos.y += 1000F;
		this.transform.position = curpos;
	}

	public override void reset ()
	{
		rb2d.transform.position = originalPosition;
		if (type == "block") {
			word = "TALK";
			wordLeft = word;
			wordDone = "";
		} else {
			word = "";
			wordLeft = "";
			wordDone = "";
		}
	}
}
