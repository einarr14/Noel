using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class LightController : MonoBehaviour {

	private bool darker;
	public float intensity;
	bool busy;
	Light light;

	// Use this for initialization
	void Start () {
		darker = false;
		busy = false;
		intensity = 0;
		light = GameObject.Find ("Point light").GetComponent<Light> ();
		fadeIn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!busy) {
			busy = true;
			StartCoroutine (waitForBusy (0.7F));
			if (darker) {
				HOTween.To (this, 0.7F, "intensity", 7.6);
				darker = false;
			} else {
				HOTween.To (this, 0.7F, "intensity", 8);
				darker = true;
			}
		}
		light.intensity = intensity;
		light.range = intensity -1F;
	}

	public void fadeIn() {
		busy = true;
		HOTween.To (this, 2F, "intensity", 7.8);
		StartCoroutine (waitForBusy (2F));
	}

	private IEnumerator waitForBusy(float duration) {
		yield return new WaitForSeconds (duration);
		busy = false;
	}

	public void fadeOut() {
		busy = true;
		HOTween.To (this, 1F, "intensity", 0);
		StartCoroutine (waitForBusy (1F));
	}
}
