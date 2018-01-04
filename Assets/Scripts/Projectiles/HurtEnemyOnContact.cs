using UnityEngine;
using System.Collections;

public class HurtEnemyOnContact : MonoBehaviour {
	
	public int damageToGive;

	public float bounceOnEnemy;

	private Rigidbody2D myRigidBody2D;
	
	void Start () {
		myRigidBody2D = transform.parent.GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "MonsterA" || other.name == "minion(Clone)" || other.name == "boss_main"
		    || other.name == "MonsterB" || other.name == "MonsterC") {
			other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
			myRigidBody2D.velocity = new Vector2 (myRigidBody2D.velocity.x, bounceOnEnemy);
		}

	}
}
