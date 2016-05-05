using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// references: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
//				https://unity3d.com/learn/tutorials/modules/beginner/ui/ui-slider

public class PlayerHealth : MonoBehaviour {

	public Slider visualHealth;
	public float totalHealth;
	public float currentHealth;
	private bool healing = false;
    public float regenTimeDelay;
	private GameObject gameManager;
	private GameObject player;
	BoardManager boardManager;
	PlayerController playerController;

	// Use this for initialization
	void Start () {
		//currentHealth = totalHealth;
		visualHealth.value = currentHealth;
		gameManager = GameObject.Find ("GameManager");
		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();
		boardManager = gameManager.GetComponent<BoardManager> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.ghostpause)
        {
            GetComponent<Renderer>().enabled = player.GetComponent<Renderer>().enabled;
            if (!playerController.getUnderAttack() && currentHealth < totalHealth && healing == false)
            {
                healing = true;
                RegenHealth();
            }
        }
	}

	private void RegenHealth () {
		StartCoroutine (DoRegenHealth());
	}

	private IEnumerator DoRegenHealth () {
		//while (currentHealth < totalHealth) {
			visualHealth.value = currentHealth;
			currentHealth++;

			yield return new WaitForSeconds (regenTimeDelay);
		//}

		healing = false;
	}

	public void TakeDamage (int ammount) {
		currentHealth -= ammount;
		visualHealth.value = currentHealth;
	}
    public void IncreaseHealth (int ammount)
    {
        totalHealth += ammount;
        visualHealth.maxValue = totalHealth;
    }
    public void setHealth()
    {
        totalHealth = 100;
        visualHealth.maxValue = totalHealth;
    }
	public void fillHealth() {
		currentHealth = 100;
        visualHealth.value = currentHealth;
        visualHealth.maxValue = totalHealth;
	}
}
