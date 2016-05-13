using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockGhostController : MonsterController {

    public Text answer;
    public Text theRiddle;
    private string riddle;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        type = "BlockGhost";
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
        }
    }

    protected override void wordAction()
    {
		privateText.text = "";
		word = "";
		theRiddle.text = riddle;
		wordLeft = word;
		wordDone = "";
        GameManager.instance.ghostscreen();
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
		GameObject.Find ("BoardManager").GetComponent<BoardManager> ().removeUnit (this);
    }

    public override void reset()
    {
        rb2d.transform.position = originalPosition;
        word = "TALK";
        wordLeft = word;
        wordDone = "";
    }
    public override string getAnswer()
    {
        return answer.text;
    }
}
