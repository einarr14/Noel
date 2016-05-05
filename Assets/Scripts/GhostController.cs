using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private GameObject player;
	public float speed;
	public float minRange;
	public float maxRange;
    private BoardManager boardmanager;
    public Text label;
    public Text answer;
    private float distance;
    PlayerHealth playerHealth;
    public int damage;
    private string riddle;
    public string type;

    // Use this for initialization
    void Start () {
        riddle = label.text;
        label.text = "";
		rb2d = GetComponent<Rigidbody2D>();
		player = GameObject.Find ("Player");
        boardmanager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        playerHealth = player.GetComponent<PlayerHealth>(); 
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.ghostpause)
        {
            var playerPoint = player.transform.position;
            Vector2 currPoint = rb2d.position;
            distance = Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y), 2F));
            if (type != "block")
            {
                if (distance < maxRange && distance > minRange)
                {
                    float moveY = (playerPoint.y - currPoint.y) / distance;
                    float moveX = (playerPoint.x - currPoint.x) / distance;
                    Vector2 Movement = new Vector2(moveX, moveY);
                    Vector2 newPosition = currPoint + speed * Movement;
                    rb2d.MovePosition(newPosition);
                }
                else if (distance <= minRange)
                {
                    GameManager.instance.ghostscreen();
                    label.text = riddle;

                }
                else
                {
                    rb2d.velocity = new Vector2(0, 0);
                }
            }
            else
            {
                if (distance <= minRange)
                {
                    label.text = riddle;
                }
                else
                {
                    label.text = "";
                }
            }
        }
        else
        {
            
        }
	}
    public bool inRange()
    {
        
        if (distance <= minRange)
        {
            return true;
        }
        return false;
    }
    public void eliminate()
    {
        Vector3 curpos = this.transform.position;
        curpos.y += 1000F;
        this.transform.position = curpos;
    }
    public void increaseHealth()
    {
        playerHealth.IncreaseHealth(damage);
    }
    public void damagePlayer()
    {
        playerHealth.TakeDamage(damage);
    }
}
