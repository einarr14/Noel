using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthbarInputPosController : MonoBehaviour {

    GameObject cam;
    Vector3 posHealth;
    Vector3 posUserInput;
    Vector3 zero;
    public Slider health;
    public InputField userInput;
	// Use this for initialization
	void Start () {
        zero = new Vector3(0F, 0F, 0F);
        cam = GameObject.Find("Main Camera");
       
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (!GameManager.instance.ghostpause)
        {
            posHealth = new Vector3(cam.GetComponent<Camera>().pixelWidth / 2, 10F, 0F);
            posUserInput = new Vector3(cam.GetComponent<Camera>().pixelWidth / 2, (cam.GetComponent<Camera>().pixelHeight + 1000), 0F);
            health.transform.position = posHealth;
            userInput.transform.position = posUserInput;
        }
        else
        {
            posHealth = new Vector3(cam.GetComponent<Camera>().pixelWidth / 2, 10F, 0F);
            posUserInput = new Vector3(cam.GetComponent<Camera>().pixelWidth / 2, 40F, 0F);
            health.transform.position = posHealth;
            userInput.transform.position = posUserInput;
        }
            
        transform.position = cam.GetComponent<Camera>().ScreenToViewportPoint(zero);
    }
    void LateUpdate()
    {
      
        transform.position = cam.GetComponent<Camera>().ScreenToViewportPoint(zero);
    }
}
   
