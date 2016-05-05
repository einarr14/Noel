using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private GameObject player;
	public float speed;
	public float minRange;
	public float maxRange;
    private BoardManager boardmanager;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		player = GameObject.Find ("Player");
        boardmanager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.ghostpause)
        {
            var playerPoint = player.transform.position;
            Vector2 currPoint = rb2d.position;
            float distance = Mathf.Sqrt(Mathf.Pow((playerPoint.x - currPoint.x), 2F) + Mathf.Pow((playerPoint.y - currPoint.y), 2F));
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


            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
        else
        {

        }
	}
}
