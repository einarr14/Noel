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
	private Vector3 playerPos;


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
		playerPos = player.transform.position;
		myPos = rb2d.position;
		//float thisDistFromPlayer = Mathf.Sqrt(Mathf.Pow((playerPos.x - myPos.x), 2F) + Mathf.Pow((playerPos.y - myPos.y), 2F));
        foreach (UnitController unit in boardManager.units)
        {
            if (this != unit)
            {
                monsterPoint =  unit.transform.position;
                float dif = Mathf.Sqrt(Mathf.Pow((monsterPoint.x - myPos.x), 2F) + Mathf.Pow((monsterPoint.y - myPos.y), 2F));
				float thisPlayerDist = Mathf.Sqrt(Mathf.Pow((playerPos.x - myPos.x), 2F) + Mathf.Pow((playerPos.y - myPos.y), 2F));
				float unitDistPlayer = Mathf.Sqrt(Mathf.Pow((playerPos.x - monsterPoint.x), 2F) + Mathf.Pow((playerPos.y - monsterPoint.y), 2F));
				if (unit.getRange() + sizeRange > dif && thisPlayerDist > unitDistPlayer)
                {
                    monsterVec.x = (monsterPoint.y - myPos.y) / dif; 
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
