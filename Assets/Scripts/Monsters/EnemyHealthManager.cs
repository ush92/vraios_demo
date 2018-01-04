using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
	
	public int enemyHealth;
	
	public GameObject deathEffect;
	
	public int pointsOnDeath;

	private float bossDeathEffectTime;
	
	
	void Start () {
		bossDeathEffectTime = 2;
	}
	
	
	void Update () {
		if (enemyHealth <= 0) {
			//ScoreManager.AddPoints(pointsOnDeath);

			if(gameObject.name == "boss_main"){
				if(bossDeathEffectTime>0){
					bossDeathEffectTime -= Time.deltaTime;
				}
				else{
					bossDeathEffectTime = 0;
				}
				GetComponent<SpriteRenderer> ().color = new Color(1,1,1,bossDeathEffectTime/2);
			}
			else{
				Instantiate(deathEffect, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		}
	}
	
	public void giveDamage(int damageToGive){
		enemyHealth -= damageToGive;
		GetComponent<AudioSource> ().Play ();
	}
}