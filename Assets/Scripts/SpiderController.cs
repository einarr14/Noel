using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiderController : MonsterController {
    private Animator animator;
	private AudioSource spiderAttack;
	private AudioSource spiderDie;
    private float moveY;
    private float moveX;

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
        CircleCollider2D circle = this.GetComponent<CircleCollider2D>();
        //sizeRange = circle.radius + 0.1F;
    }

	protected void initializeKillPhrases()
	{
		killPhrases = new string[21];

		killPhrases[0] = "TARANTULA";
		killPhrases[1] = "BLACK WIDOW";
		killPhrases[2] = "CAMEL SPIDER";
		killPhrases[3] = "VENOMOUS FANGS";
		killPhrases[4] = "STEP ON IT";
		killPhrases[5] = "KILL IT WITH FIRE";
		killPhrases[6] = "ORDER ARANAEE";
		killPhrases[7] = "ARTHROPODS";
		killPhrases[8] = "ARACHNIDS";
		killPhrases[9] = "NASTY CREATURE";
		killPhrases[10] = "CEPHALOTHORAX";
		killPhrases[11] = "ANTIVENOM";
		killPhrases[12] = "MYAGALOMORPH";
		killPhrases[13] = "ARACHNOPHOBIA";
		killPhrases[14] = "SQUISHY SPIDER";
		killPhrases[15] = "LEAVE ME ALONE";
		killPhrases[16] = "SPIDERLING";
		killPhrases[17] = "GOLIATH BIRDEATER";
		killPhrases[18] = "SPIDERMAN";
		killPhrases[19] = "CARTWHEELING";
		killPhrases[20] = "AMAZON SPIDER";
	}

	// Update is called once per frame
	protected void Update () {
		base.Update ();
        if (!GameManager.instance.ghostpause)
        {
			move ();
            rotiation();
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
            if (collision)
            {
                moveY = (playerPoint.y - currPoint.y + collisionVector.y) / Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
                moveX = (playerPoint.x - currPoint.x + collisionVector.x )/ Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
            }

            else
            {
                moveY = (playerPoint.y - currPoint.y) / distance;
                moveX = (playerPoint.x - currPoint.x) / distance;
            }
            Vector2 Movement = new Vector2(moveX, moveY);
            Vector2 newPosition = currPoint + speed * Movement;
            
            animator.SetBool("SpiderWalk", true);
            rb2d.MovePosition(newPosition);
            Vector2 monsterVec = new Vector2(0F,0F);
            if (monsterInRange(ref monsterVec))
            {
                rb2d.MovePosition(currPoint + monsterVec * speed);
            }
            
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
    private void rotiation()
    {
        moveY = (playerPoint.y - currPoint.y) / distance;
        moveX = (playerPoint.x - currPoint.x) / distance;

        if (moveY > 0)
        {
            rb2d.rotation = (180 + (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
        }
        else
        {
            rb2d.rotation = (180 - (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
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
		word = killPhrases[Mathf.FloorToInt(Random.value * killPhrases.Length)];
        wordLeft = word;
        wordDone = "";
		timeSlowed = false;
    }


	
}


