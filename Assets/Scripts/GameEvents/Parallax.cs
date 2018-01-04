using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	private float x;
	public int offset;
	public bool followCamera;

	void Start () {
		x = Camera.main.transform.position.x;
	}
	

	void Update () {
		if (followCamera) {
			transform.localPosition = new Vector3 (Camera.main.transform.position.x - x, -6.5f, 5) / offset;
		} else {
			transform.localPosition = new Vector3 (x - Camera.main.transform.position.x, -6.5f, 5) / offset;
		}
	}
}
