using UnityEngine;
using System.Collections;

public class MonsterController : UnitController {

	protected PlayerHealth playerHealth;
    protected string[] killPhrases;
    public float speed;
	public int damage;
    public float biteRange;
    protected bool attacking;
    public float attackSpeed;


    void Awake () {
	}

	// Use this for initialization
	protected void Start () {
		base.Start ();
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	protected virtual void move () {
		//source.Play ();
		float moveY = (playerPoint.y - currPoint.y) / distance;
		float moveX = (playerPoint.x - currPoint.x) / distance;
		Vector2 Movement = new Vector2(moveX, moveY);
		Vector2 newPosition = currPoint + speed * Movement;
		rb2d.MovePosition(newPosition);
	}


	public override void damagePlayer()
	{
		playerHealth.TakeDamage(damage);
	}
    protected void initializeKillPhrases()
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
    protected bool collision;
    protected Vector3 collisionVector;
    void OnCollisionStay2D(Collision2D coll)
    {
        collisionVector = rb2d.transform.position - coll.transform.position;
        collision = true;

    }
    void OnCollisionExit2D(Collision2D coll)
    {
        collision = false;

    }
}
