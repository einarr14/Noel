﻿using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GameObject.Find("TeleporterIn").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        string destination;
        if(other.name == "Player")
        {
            switch (this.name)
            {
                case "TeleporterIn":
                    destination = "TeleporterOut";
                    break;
                case "TeleporterIn2":
                    destination = "TeleporterOut3";
                    break;
                case "TeleporterIn3":
                    destination = "TeleporterOut6";
                    break;
                case "TeleporterIn4":
                    destination = "TeleporterOut5";
                    break;
                case "TeleporterIn5":
                    destination = "TeleporterOut2";
                    break;
                case "TeleporterIn6":
                    destination = "TeleporterOut4";
                    break;
                default:
                    destination = "TeleporterOut7";
                    break;
            }

            other.transform.position = GameObject.Find(destination).transform.position;
            source.Play();
        }
        
    }
    
}
