using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatText : MonoBehaviour {
    public int SentenceLength;
    public string Killtext;
    private Text TheText;

    // Use this for initialization
    void Start () {
        SentenceLength = 5;
        TheText.text = Killtext;
 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
