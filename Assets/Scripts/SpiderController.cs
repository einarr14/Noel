using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiderController : MonsterController {
    private Animator animator;
	private AudioSource spiderAttack;
	private AudioSource spiderDie;

	void Awake ()
	{
		AudioSource[] source = GetComponents<AudioSource> ();
		spiderAttack = source [0];
		spiderDie = source [1];
        type = "Spider";
	}
    // Use this for initialization
	protected void Start()
    {
		base.Start ();
		timeSlowed = false;
		attacking = false;
        animator = GetComponent<Animator>();
		initializeKillPhrases ();
		reset ();
    }

	// Update is called once per frame
	protected void Update () {
		base.Update ();
        if (!GameManager.instance.ghostpause)
        {
			move ();
            if (distance < biteRange && attacking == false)
            {
                attacking = true;
                BitePlayer();
            }
        }
	}

	protected override void move() {
		if (distance < maxRange && distance > minRange)
		{
            float moveY = 0;
            float moveX = 0;
            if (collision)
            {
                moveY = (playerPoint.y - currPoint.y + collisionVector.y) / Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
                moveX = (playerPoint.x - currPoint.x + collisionVector.x )/ Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
                Debug.Log("this");
            }

            else
            {
                moveY = (playerPoint.y - currPoint.y) / distance;
                moveX = (playerPoint.x - currPoint.x) / distance;
                Debug.Log("that");
            }
            Vector2 Movement = new Vector2(moveX, moveY);
            Debug.Log(Movement);
            Vector2 newPosition = currPoint + speed * Movement;
            moveY = (playerPoint.y - currPoint.y) / distance;
            moveX = (playerPoint.x - currPoint.x) / distance;
            animator.SetBool("SpiderWalk", true);
			if (moveY > 0)
			{
				rb2d.rotation = (180 + (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
			}
			else
			{
				rb2d.rotation = (180 - (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
			}
			rb2d.MovePosition(newPosition);
		}
		else if (distance > maxRange)
		{
			rb2d.velocity = new Vector2(0, 0);
			animator.SetBool("SpiderWalk", false);
		}
		else
		{
			rb2d.velocity = new Vector2(0, 0);
		}
	}

	private void BitePlayer() {
		StartCoroutine (DoBitePlayer());
	}

	private IEnumerator DoBitePlayer () {
		spiderAttack.Play ();
		playerHealth.TakeDamage (damage);
        animator.SetTrigger("SpiderBite");
        yield return new WaitForSeconds (attackSpeed);
        
        attacking = false;
	}


	protected override void wordAction ()
    {
		spiderDie.Play ();
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
		word = killPhrases[Mathf.FloorToInt(Random.value * 9)];
        wordLeft = word;
        wordDone = "";
		timeSlowed = false;
    }

	
}


