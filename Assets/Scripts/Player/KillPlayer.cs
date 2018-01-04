using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
	
	public LevelManager levelManager;
	private HealthManager health;
	
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		health = FindObjectOfType<HealthManager> ();
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			health.KillPlayer();
			levelManager.RespawnPlayer();
		}
	}
}