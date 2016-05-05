using UnityEngine;
using System.Collections;

public class CrateCorner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Crate")
        {
            GameObject.Find("Crate").transform.position = new Vector3(11.3f, -9.83f, 0);
        }
    }
}
