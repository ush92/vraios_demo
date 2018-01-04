using UnityEngine;
using System.Collections;

public class DestroyFinishedParticle : MonoBehaviour {
	
	private ParticleSystem thisParticleSystem;
	
	void Start () {
		thisParticleSystem = GetComponent<ParticleSystem>();	
	}
	
	void Update () {
		if (thisParticleSystem.isPlaying)
			return;
		
		Destroy (gameObject);
	}
	
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
