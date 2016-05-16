using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatController : MonsterController {

    
    public int lifeSteal;
    private float moveY;
    private float moveX;
    private Animator animator;
    //private AudioSource spiderAttack;
    //private AudioSource spiderDie;

    void Awake()
    {
        //AudioSource[] source = GetComponents<AudioSource>();
        //spiderAttack = source[0];
        //spiderDie = source[1];
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

    protected override void move()
    {
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
			Vector2 monsterVec = new Vector2 (0F, 0F);
			if (monsterInRange (ref monsterVec)) {
				//rb2d.MovePosition (currPoint + monsterVec * speed);
			} else {
				Vector2 Movement = new Vector2 (moveX, moveY);
				Vector2 newPosition = currPoint + speed * Movement;
				//animator.SetBool("SpiderWalk", true)
				rb2d.MovePosition (newPosition);
			}
			// þetta er illa dapurt movement fix til að þeir ýti ekki hvor öðrum
			//Vector2 monsterVec = new Vector2 (0F, 0F);
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
        //spiderAttack.Play();
        playerHealth.TakeDamage(damage);
        backupLetters(lifeSteal);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackSpeed);

        attacking = false;
    }


    protected override void wordAction()
    {
        //spiderDie.Play();
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
