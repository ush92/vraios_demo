using UnityEngine;
using System.Collections;

public class DestroyBlock : MonoBehaviour {

	public GameObject impactEffect;

	void Start () {
	
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Block") {
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(other.gameObject);
		}	
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Block") {
			Destroy (other.gameObject);
		}
	}
}
