using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChaseGhostController : MonsterController {

    public Text theRiddle;
    public Text answer;
    private string riddle;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        type = "ChaseGhost";
    }

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        riddle = theRiddle.text;
        reset();
        hostile = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        if (!GameManager.instance.ghostpause)
        {
            theRiddle.text = "";
                if (distance < maxRange && distance > minRange)
                {
                    source.time = 0.9f;
                    source.Play();
                    move();
                }
                else if (distance <= minRange)
                {
                    GameManager.instance.ghostscreen();
                    theRiddle.text = riddle;
                }
                else
                {
                    rb2d.velocity = new Vector2(0, 0);
                }
        }
    }
    public override void increaseHealth()
    {
        playerHealth.IncreaseHealth(damage);
    }

    public override void eliminate()
    {
        Vector3 curpos = this.transform.position;
        curpos.y += 1000F;
        this.transform.position = curpos;
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
            word = "";
            wordLeft = "";
            wordDone = "";
    }
    public override string getAnswer()
    {
        return answer.text;
    }
}
