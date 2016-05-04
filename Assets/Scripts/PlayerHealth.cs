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
		if (!playerController.getUnderAttack() && currentHealth < totalHealth && healing == false) {
			healing = true;
			RegenHealth ();
		}
	}

	private void RegenHealth () {
		StartCoroutine (DoRegenHealth());
	}

	private IEnumerator DoRegenHealth () {
		while (currentHealth < totalHealth) {
			visualHealth.value = currentHealth;
			currentHealth++;

			yield return new WaitForSeconds (0.05f);
		}

		healing = false;
	}

	public void TakeDamage (int ammount) {
		currentHealth -= ammount;
		visualHealth.value = currentHealth;
	}}
