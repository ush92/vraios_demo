using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	
	public float speed;
	public float rotationSpeed;
	
	public PlayerController player;
	
	public int pointsForKill;
	
	public GameObject enemyDeathEffect;
	public GameObject impactEffect;
	
	public int damageToGive;


	void Start () {
		player = FindObjectOfType<PlayerController>();

		if (player.transform.localScale.x < 0) {
			speed = -speed;
			rotationSpeed = -rotationSpeed;
		}
	}
	
	
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);
		GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;

		if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
			transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.localScale = new Vector3 (-0.5f, 0.5f, 1.0f);
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy" || other.tag =="boss1_minion") {
			other.GetComponent<EnemyHealthManager>().giveDamage (damageToGive);
		}
		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
		
	}
}
