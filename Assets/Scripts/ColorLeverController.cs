using UnityEngine;
using System.Collections;

public class ColorLeverController : MonoBehaviour {
    bool fail;
    public AudioSource success;
    public AudioSource failure;
    public AudioSource doorSound;
    GameManager gameManager;
    public GameObject Zombie;

    // Use this for initialization
    void Start () {
        AudioSource[] audio = GameObject.Find("Levers").GetComponents<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        doorSound = GameObject.Find("Door1").GetComponent<AudioSource>();
        success = audio[0];
        failure = audio[1];
        fail = false;
      
	}
	

           
 

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //GBRY
        if (other.tag == "Player")
        {
            if (this.name == "Blue")
            {
                if (gameManager.leverColorCount != 1)
                {
                    fail = true;

                }
                else
                {
                    GameObject.Find("BlueActivated").transform.position = new Vector3(23.2f, -6.5f, 0);
                }
            }
            else if (this.name == "Green")
            {
                if (gameManager.leverColorCount != 0)
                {
                    fail = true;

                }
                else
                {
                    GameObject.Find("GreenActivated").transform.position = new Vector3(23.2f, -4.0f, 0);
                }
            }
            else if (this.name == "Red")
            {
                if (gameManager.leverColorCount != 2)
                {
                    fail = true;

                }
                else
                {
                    GameObject.Find("RedActivated").transform.position = new Vector3(23.2f, -1.6f, 0);
                }
            }
            else if (this.name == "Yellow")
            {
                if (gameManager.leverColorCount != 3)
                {
                    fail = true;

                }
                else
                {
                    GameObject.Find("YellowActivated").transform.position = new Vector3(23.2f, 0.8f, 0);
                }
            }
        }
        
        if(fail == true)
        {
            failure.Play();
            spawn();
            GameObject.Find("BlueActivated").transform.position = new Vector3(500, 500, 500);
            GameObject.Find("GreenActivated").transform.position = new Vector3(500, 500, 500);
            GameObject.Find("RedActivated").transform.position = new Vector3(500, 500, 500);
            GameObject.Find("YellowActivated").transform.position = new Vector3(500, 500, 500);
            gameManager.leverColorCount = 0;
            fail = false;
        }
        else
        {
            success.Play();
            gameManager.leverColorCount++;

            if (gameManager.leverColorCount == 4)
            {
                Debug.Log("Success!");
                GameObject.Find("Door1").transform.position = new Vector3(800, 800, 800);
                doorSound.Play();
            }
        }

       
    }
    void spawn()
    {
        GameObject newZombie = Instantiate(Zombie);
        Vector3 pos = new Vector3(-5F, 0, 0);
        newZombie.transform.position = this.transform.position+pos;
    }

}
