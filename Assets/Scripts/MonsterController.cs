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
    private Vector3 monsterPoint;
    private Vector2 myPos;


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

        killPhrases = new string[30];


        killPhrases[0] = "SAVAGE GARDEN";
        killPhrases[1] = "MURDERTRAIN";
        killPhrases[2] = "DEATH STAR";
        killPhrases[3] = "MASTER OF PUPPETS";
        killPhrases[4] = "DESTRUCTION";
        killPhrases[5] = "ELDERLY BINGONIGHT";
        killPhrases[6] = "HOUNTED HOUSE";
        killPhrases[7] = "MOLTEN LAVA";
        killPhrases[8] = "ASTROID BELT";
        killPhrases[9] = "NASTY CREATURE";
        killPhrases[10] = "THE BOOGEYMAN";
        killPhrases[11] = "MASSIVE EXPLOSION";
        killPhrases[12] = "CREEPY STALKER";
        killPhrases[13] = "OVERRIPE AVACADO";
        killPhrases[14] = "SQUISHY WORM";
        killPhrases[15] = "SHARK ATTACK";
        killPhrases[16] = "KILLER WHALE";
        killPhrases[17] = "DEATH FROM ABOVE";
        killPhrases[18] = "RAPID HELLHOUNDS";
        killPhrases[19] = "BANANA BREAD";
        killPhrases[20] = "WICKED WITCH";
        killPhrases[21] = "TASMANIAN DEVIL";
        killPhrases[22] = "GRUMPY BADGER";
        killPhrases[23] = "CRAZY CATWOMAN";
        killPhrases[24] = "EXPLODING DIARRHEA";
        killPhrases[25] = "EATER OF SOULS";
        killPhrases[26] = "GINGERBREAD MAN";
        killPhrases[27] = "HORDES OF RATS";
        killPhrases[28] = "INNOCENT BLOOD";
        killPhrases[29] = "FERMENTED SHARK";






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
    protected bool monsterInRange(ref Vector2 monsterVec)
    {
        int count = 0;
        foreach (UnitController unit in boardManager.units)
        {
            count++;
            if (this != unit)
            {
				monsterPoint =  unit.transform.position;
				myPos = rb2d.position;
				float dif = Mathf.Sqrt(Mathf.Pow((monsterPoint.x - myPos.x), 2F) + Mathf.Pow((monsterPoint.y - myPos.y), 2F));
                if (unit.getRange() + sizeRange > dif)
                {
                    monsterVec.x = -(monsterPoint.y - myPos.y) / dif; 
                    monsterVec.y = -(monsterPoint.x - myPos.x) / dif;
                    return true;
                }
            }
            else
            {
            }
        }
        
        return false;
    }
}
