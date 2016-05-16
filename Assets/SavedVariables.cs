using UnityEngine;
using System.Collections;

public class SavedVariables : MonoBehaviour {

    private int difficulty;

	// Use this for initialization
    void Awake()
    {
        difficulty = 0;
    }

	void Start () {

	}

    public int getDifficulty()
    {
        return difficulty;
    }
	public void setDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
