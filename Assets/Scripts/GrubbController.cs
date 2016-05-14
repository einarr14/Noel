using UnityEngine;
using System.Collections;

public class GrubbController : MonsterController {

    //private Animator animator;
    //private AudioSource spiderAttack;
    //private AudioSource spiderDie;
    private float moveY;
    private float moveX;
    void Awake()
    {
        //AudioSource[] source = GetComponents<AudioSource>();
        //spiderAttack = source[0];
        //spiderDie = source[1];
        type = "Grubb";
    }
    // Use this for initialization
    protected void Start()
    {
        base.Start();
        timeSlowed = false;
        attacking = false;
        
        //animator = GetComponent<Animator>();
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
        GameObject.Find("BoardManager").GetComponent<BoardManager>().removeUnit(this);
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
        word = killPhrases[Mathf.FloorToInt(Random.value * 9)];
        wordLeft = word;
        wordDone = "";
        timeSlowed = false;
    }


}
