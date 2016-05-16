using UnityEngine;
using System.Collections;

public class GrubbController : MonsterController {

    private Animator animator;
    private AudioSource zombieAttack;
    private AudioSource zombieDie;
    private float moveY;
    private float moveX;
    public int textLength;
    private char[] letters;
    void Awake()
    {
        AudioSource[] source = GetComponents<AudioSource>();
        zombieAttack = source[0];
        zombieDie = source[1];
        type = "Grubb";
    }
    // Use this for initialization
    protected void Start()
    {
        base.Start();
        timeSlowed = false;
        attacking = false;
        
        animator = GetComponent<Animator>();
        initializeKillPhrases();
        initializeLetters();
        reset();
        
        
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
            Vector2 Movement = new Vector2(moveX, moveY);
            Vector2 newPosition = currPoint + speed * Movement;
            //animator.SetBool("SpiderWalk", true);
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
            //animator.SetBool("SpiderWalk", false);
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
            rb2d.rotation = (-180 + (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
        }
        else
        {
            rb2d.rotation = (-180 - (Mathf.Acos(moveX) * 360 / 3.14F)) / 2;
        }
    }

    private void BitePlayer()
    {
        StartCoroutine(DoBitePlayer());
    }

    private IEnumerator DoBitePlayer()
    {
        zombieAttack.Play();
        playerHealth.TakeDamage(damage);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackSpeed);

        attacking = false;
    }


    protected override void wordAction()
    {
        zombieDie.time = 0.5f;
        zombieDie.Play();
        Vector3 curpos = this.transform.position;
        curpos.y += 1000F;
        this.transform.position = curpos;
        wordDone = "";
        wordLeft = word;
        GameObject.Find("BoardManager").GetComponent<BoardManager>().removeUnit(this);
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
        word = "";
        makeWord();
        wordLeft = word;
        wordDone = "";
        timeSlowed = false;
    }
    void makeWord()
    {
        for (int i = 0; i < textLength;i++)
        {
            word = word + letters[Mathf.FloorToInt(Random.value * 26)];
            
        }
        word = word.ToUpper();
    }
    void initializeLetters()
    {
        letters = new char[26];
        letters[0] = 'q';
        letters[1] = 'w';
        letters[2] = 'e';
        letters[3] = 'r';
        letters[4] = 't';
        letters[5] = 'y';
        letters[6] = 'u';
        letters[7] = 'i';
        letters[8] = 'o';
        letters[9] = 'p';
        letters[10] = 'a';
        letters[11] = 's';
        letters[12] = 'd';
        letters[13] = 'f';
        letters[14] = 'g';
        letters[15] = 'h';
        letters[16] = 'j';
        letters[17] = 'k';
        letters[18] = 'l';
        letters[19] = 'z';
        letters[20] = 'x';
        letters[21] = 'c';
        letters[22] = 'v';
        letters[23] = 'b';
        letters[24] = 'n';
        letters[25] = 'm';
    }



}
