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
	private bool takingDAmage = false;
	private Color normalColor;
	private Color flashColor = new Color(1F,0F,0F,0.1F);
	private float blendColor;
	public Light playerLight;
    public AudioSource charHit;
    public AudioSource charDeath;

	// Use this for initialization
	void Start () {
        AudioSource[] audio = GetComponents<AudioSource>();
        charHit = audio[0];
        charDeath = audio[1];

		//currentHealth = totalHealth;
		visualHealth.value = currentHealth;
		gameManager = GameObject.Find ("GameManager");
		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();
		boardManager = gameManager.GetComponent<BoardManager> ();
		normalColor = playerLight.color;
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
			if (blendColor > 0F) {
				blendColor -= 0.04F;	
			}
			playerLight.color = Color.Lerp (normalColor, flashColor, blendColor);

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
		if(ammount >= currentHealth)
        {
            charDeath.Play();
        }
        else
        {
            charHit.Play();
        }
		currentHealth -= ammount;
		visualHealth.value = currentHealth;
		blendColor = 1;
	}
    public void IncreaseHealth (int ammount)
    {
        totalHealth += ammount;
        visualHealth.maxValue = totalHealth;
        currentHealth += ammount;
        visualHealth.value = currentHealth;
    }
    public void setHealth()
    {
        totalHealth = 100;
        visualHealth.maxValue = totalHealth;
    }
	public void fillHealth() {
		currentHealth = 100;
		visualHealth.maxValue = totalHealth;
		visualHealth.value = totalHealth;
	}
}
