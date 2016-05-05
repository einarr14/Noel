using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiderController : MonoBehaviour {
    public int strength;
	public float speed;
	private Rigidbody2D rb2d;
	public float maxRange;
	public float minRange;
	public float biteRange;
	private bool attacking = false;
	private GameObject player;
	public int damage;
	public float attackSpeed;
	PlayerHealth playerHealth;
    private Animator animator;
    private GUIText killText;
    public Text label;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		player = GameObject.Find ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
        animator = GetComponent<Animator>();
        
        

	}

	// Update is called once per frame
	void Update () {
		var playerPoint = player.transform.position;
		Vector2 currPoint = rb2d.position;
		float distance = Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x),2F) + Mathf.Pow((playerPoint.y - currPoint.y), 2F));
		if (distance < maxRange && distance > minRange) {
			float moveY = (playerPoint.y - currPoint.y) / distance;
			float moveX = (playerPoint.x - currPoint.x) / distance;
			Vector2 Movement = new Vector2 (moveX, moveY);
			Vector2 newPosition = currPoint + speed * Movement;
            animator.SetBool("SpiderWalk",true);
            //rb2d.MoveRotation (Mathf.Acos (moveX));
            if (moveY > 0) {
				rb2d.rotation = (180 + (Mathf.Acos (moveX) * 360 / 3.14F)) / 2;
			} else {
				rb2d.rotation = (180 - (Mathf.Acos (moveX) * 360 / 3.14F)) / 2;
			}
				
			rb2d.MovePosition (newPosition);
		}
        else if (distance > maxRange)
        {
            rb2d.velocity = new Vector2(0, 0);
            animator.SetBool("SpiderWalk", false);
        }
        else {
			rb2d.velocity = new Vector2(0, 0);
        }
		if (distance < biteRange && attacking == false) {
			attacking = true;
            

            BitePlayer ();
            
		}

	}
	private void BitePlayer() {
		StartCoroutine (DoBitePlayer());
	}

	private IEnumerator DoBitePlayer () {
		playerHealth.TakeDamage (damage);
        animator.SetTrigger("SpiderBite");
        yield return new WaitForSeconds (attackSpeed);
        
        attacking = false;
	}

	public bool inRange () {
		var playerPoint = player.transform.position;
		Vector2 currPoint = rb2d.position;
		float distance = Mathf.Sqrt (Mathf.Pow ((playerPoint.x - currPoint.x), 2F) + Mathf.Pow ((playerPoint.y - currPoint.y), 2F));
		if (distance < maxRange) {
            label.text = "Einsi Kaldi";
            return true;
            
        }
        label.text = "";
        return false;
	}
    public void eliminate ()
    {
		Vector3 curpos = this.transform.position;
		curpos.y += 1000F;
		this.transform.position = curpos;
    }
}

