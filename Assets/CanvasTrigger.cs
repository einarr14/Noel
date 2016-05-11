using UnityEngine;
using System.Collections;

public class CanvasTrigger : MonoBehaviour {

    public Collider2D collider1;

	// Use this for initialization
	void Start () {
        Collider2D[] colliders = GetComponents<Collider2D>();

        collider1 = colliders[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.gameObject == collider1)
        {
            Debug.Log("Hi");
        }
    }
}
