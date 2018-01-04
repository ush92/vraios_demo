using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public GameObject currentCheckpoint;
	
	private PlayerController player;
	private float gravityStore;
	
	public HealthManager healthManager;
	public LifeManager lifeManager;
	
	public GameObject deathParticle;
	public GameObject respawnParticle;
	
	public int pointPenaltyOnDeath;
	
	public float respawnDelay;

	private CameraController levelCamera;
	
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		levelCamera = FindObjectOfType<CameraController> ();
		healthManager = FindObjectOfType<HealthManager> ();
		lifeManager = FindObjectOfType<LifeManager> ();
	}
	
	void Update () {
		
	}
	
	public void RespawnPlayer(){
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo(){
		player.enabled = false;
		player.GetComponent<Renderer>().enabled = false;
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		levelCamera.isFollowing = false;
		ScoreManager.AddPoints (-pointPenaltyOnDeath);

		yield return new WaitForSeconds (respawnDelay);
		if (lifeManager.lifeCounter > 0) {
			healthManager.FullHp ();
			healthManager.isDead = false;
			Debug.Log ("Player has been respawned.");
			player.knockbackCount = 0;
			player.transform.position = currentCheckpoint.transform.position;
			player.GetComponent<Renderer> ().enabled = true;
			player.enabled = true;
			levelCamera.isFollowing = true;
			Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		}
	}
}
