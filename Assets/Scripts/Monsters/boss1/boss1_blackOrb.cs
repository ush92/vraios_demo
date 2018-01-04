using UnityEngine;
using System.Collections;

public class boss1_blackOrb : MonoBehaviour {

	public GameObject minion;
	public Transform respawnPoint;
	public GameObject sparks;
	

	void Start () {

	}
	

	void Update () {

	}

	public void Spawn(){
		Instantiate (minion, respawnPoint.position, respawnPoint.rotation);
		Instantiate(sparks, transform.position, transform.rotation);
	}
}
