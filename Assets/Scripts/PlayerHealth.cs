using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// references: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
//				https://unity3d.com/learn/tutorials/modules/beginner/ui/ui-slider

public class PlayerHealth : MonoBehaviour {

	public Slider visualHealth;
	public float totalHealth;
	public float currentHealth;
	//bool attack = GameObject.Find("Player").
	private bool healing = false;
    public float regenTimeDelay;

	// Use this for initialization
	void Start () {
		//currentHealth = totalHealth;
		visualHealth.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (/*!player.underAttack && */currentHealth < totalHealth && healing == false) {
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

			yield return new WaitForSeconds (regenTimeDelay);
		}

		healing = false;
	}

	public void TakeDamage (int ammount) {
		currentHealth -= ammount;
		visualHealth.value = currentHealth;
	}}
