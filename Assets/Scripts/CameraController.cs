using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	// Code borrowed from https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial/following-player-camera?playlist=25844
	public GameObject player;
	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
