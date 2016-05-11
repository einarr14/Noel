using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CrateCorner : MonoBehaviour {

    public int level;

	// Use this for initialization
	void Start () {
        level = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Crate")
        {
            if(level == 1)
            {
                GameObject.Find("Crate").transform.position = new Vector3(11.3f, -9.83f, 0);
            }
            if(level == 3)
            {
                GameObject.Find("Crate").transform.position = new Vector3(3.67f, -14.37f, 0);
            }
        }
    }
}
