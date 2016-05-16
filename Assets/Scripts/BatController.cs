using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatController : MonsterController {

    
    public int lifeSteal;
    private float moveY;
    private float moveX;
    private Animator animator;
    private AudioSource batAttack;
    private AudioSource batDie;

    void Awake()
    {
        AudioSource[] source = GetComponents<AudioSource>();
        batAttack = source[0];
        batDie = source[1];
        type = "Bat";
    }

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        attacking = false;
        animator = GetComponent<Animator>();
        initializeKillPhrases();
        reset();
        timeSlowed = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        if (!GameManager.instance.ghostpause)
        {
            move();
            rotiation();
            if (distance < biteRange && attacking == false)
            {
                attacking = true;
                BitePlayer();
            }
        }
    }

	protected void initializeKillPhrases()
	{
		killPhrases = new string[22];

		killPhrases[0] = "DRACULA";
		killPhrases[1] = "BLOODSUCKER";
		killPhrases[2] = "STAKE";
		killPhrases[3] = "CRANBERRY";
		killPhrases[4] = "DESTRUCTION";
		killPhrases[5] = "SUNLIGHT";
		killPhrases[6] = "FANGS";
		killPhrases[7] = "COFFIN";
		killPhrases[8] = "VAMPIRE";
		killPhrases[9] = "CREATURE";
		killPhrases[10] = "TWILIGHT";
		killPhrases[11] = "CULLEN";
		killPhrases[12] = "STALKER";
		killPhrases[13] = "UNDEAD";
		killPhrases[14] = "TRANSYLVANIA";
		killPhrases[15] = "SPARKLING";
		killPhrases[16] = "BLOODTHIRSTY";
		killPhrases[17] = "NOSFERATU";
		killPhrases[18] = "TERROR";
		killPhrases[19] = "NIGHT";
		killPhrases[20] = "GARLIC";
		killPhrases [21] = "BANANA";

	}

    protected override void move()
    {
		if (gameManager.playerDead()) {
			return;
		}
        if (distance < maxRange && distance > minRange)
        {
            if (collision)
            {
                moveY = (playerPoint.y - currPoint.y + collisionVector.y) / Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
                moveX = (playerPoint.x - currPoint.x + collisionVector.x) / Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x + collisionVector.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y + collisionVector.y), 2F));
            }
            else
            {
                moveY = (playerPoint.y - currPoint.y) / distance;
                moveX = (playerPoint.x - currPoint.x) / distance;
            }
            Vector2 Movement = new Vector2(moveX, moveY);
            Vector2 newPosition = currPoint + speed * Movement;
            //animator.SetBool("SpiderWalk", true)
            rb2d.MovePosition(newPosition);
            // þetta er illa dapurt movement fix til að þeir ýti ekki hvor öðrum
            Vector2 monsterVec = new Vector2(0F, 0F);
            if (monsterInRange(ref monsterVec))
            {
                rb2d.MovePosition(currPoint + monsterVec * speed);
            }
        }
        else if (distance > maxRange)
        {
            rb2d.velocity = new Vector2(0, 0);
           // animator.SetBool("SpiderWalk", false);
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

    private void BitePlayer()
    {
        StartCoroutine(DoBitePlayer());
    }

    private IEnumerator DoBitePlayer()
    {
        batAttack.Play();
        playerHealth.TakeDamage(damage);
        backupLetters(lifeSteal);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackSpeed);

        attacking = false;
    }


    protected override void wordAction()
    {
        batDie.Play();
        Vector3 curpos = this.transform.position;
        curpos.y += 1000F;
        this.transform.position = curpos;
        wordDone = "";
        wordLeft = word;
        timeSlowed = false;
		GameObject.Find ("BoardManager").GetComponent<BoardManager> ().removeUnit (this);
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
        word = killPhrases[Mathf.FloorToInt(Random.value * killPhrases.Length)];
        wordLeft = word;
        wordDone = "";
        timeSlowed = false;
    }

   
    private void backupLetters(int numberOfLetters)
    {
        for (int i = 0; i< numberOfLetters;i++)
        {
            if (wordLeft != word)
            {
                wordLeft = wordDone[wordDone.Length - 1] + wordLeft;
                wordDone = wordDone.Remove((wordDone.Length - 1), 1);
            }
        }
    }
}
