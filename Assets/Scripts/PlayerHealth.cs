using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// references: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
//				https://unity3d.com/learn/tutorials/modules/beginner/ui/ui-slider

public class PlayerHealth : MonoBehaviour {

	public Slider visualHealth;
	public float totalHealth;
	public float currentHealth;
	PlayerController player;

	// Use this for initialization
	void Start () {
		//currentHealth = totalHealth;
		visualHealth.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (/*!player.underAttack && */currentHealth < totalHealth) {
			RegenHealth ();
		}
	}

	public void TakeDamage (int ammount) {
		currentHealth -= ammount;
		visualHealth.value = currentHealth;
	}

	private void RegenHealth () {
		StartCoroutine (DoRegenHealth());
	}

	private IEnumerator DoRegenHealth () {
		float futureHealth = currentHealth += 1;
		while (currentHealth != futureHealth) {
			currentHealth += 1;
			visualHealth.value = currentHealth;
			yield return new WaitForSeconds(2F);
		}
	}
}
