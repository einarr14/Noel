using UnityEngine;
using System.Collections;

public class BatController : MonsterController {

    public float biteRange;
    private bool attacking;
    public float attackSpeed;
    public int lifeSteal;
    //private Animator animator;
    public bool timeSlowed = false;
    private string[] killPhrases;
    //private AudioSource spiderAttack;
    //private AudioSource spiderDie;

    void Awake()
    {
        //AudioSource[] source = GetComponents<AudioSource>();
        //spiderAttack = source[0];
        //spiderDie = source[1];
    }

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        attacking = false;
        // animator = GetComponent<Animator>();
        initializeKillPhrases();
        reset();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        if (!GameManager.instance.ghostpause)
        {
            move();
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
            float moveY = (playerPoint.y - currPoint.y) / distance;
            float moveX = (playerPoint.x - currPoint.x) / distance;
            Vector2 Movement = new Vector2(moveX, moveY);
            Vector2 newPosition = currPoint + speed * Movement;
            //animator.SetBool("SpiderWalk", true);
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
           // animator.SetBool("SpiderWalk", false);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
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
        //backupLetters(lifeSteal);
        //animator.SetTrigger("SpiderBite");
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
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
        word = killPhrases[Mathf.FloorToInt(Random.value * 9)];
        wordLeft = word;
        wordDone = "";
        timeSlowed = false;
    }

    private void initializeKillPhrases()
    {
        killPhrases = new string[9];

        killPhrases[0] = "SAVAGE GARDEN";
        killPhrases[1] = "MURDERTRAIN";
        killPhrases[2] = "EINAR SAVAGE";
        killPhrases[3] = "MASTER OF INHERITANCE";
        killPhrases[4] = "YO MAMA";
        killPhrases[5] = "ELDERLY BINGONIGHT";
        killPhrases[6] = "ZOOLANDER 2";
        killPhrases[7] = "HOT MAMA BITCH";
        killPhrases[8] = "COOL GUYS DONT LOOK AT EXPLOTIONS";
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
