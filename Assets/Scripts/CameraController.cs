using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	// Code borrowed from https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial/following-player-camera?playlist=25844
	public GameObject player;
	private Vector3 offset;

	void Start () {
		offset.z = transform.position.z - player.transform.position.z;
		transform.position = player.transform.position + offset;
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
