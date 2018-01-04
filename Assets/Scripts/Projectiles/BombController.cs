using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

	private PlayerController player;
	private DmgSplash dmgSplash;
	public GameObject explosionEffect;
	public LevelManager levelManager;

	public float lifeTime;
	private bool isDamageDealt;
	public int damageToGive;




	void Start () {
		player = FindObjectOfType<PlayerController> ();
		levelManager = FindObjectOfType<LevelManager> ();
		dmgSplash = FindObjectOfType<DmgSplash> ();
		gameObject.GetComponent<Collider2D> ().enabled = false;
		isDamageDealt = false;
	}
	

	void Update () {
		lifeTime -= Time.deltaTime;
		
		if (lifeTime < 0.05) {
			gameObject.GetComponent<Collider2D> ().enabled = true;
		}
		if (lifeTime < 0) {
			Destroy(gameObject);
		}

	}

	void OnDestroy(){
		Instantiate(explosionEffect, transform.position, transform.rotation);
		player.isBombPlaced = false;
	}

	void OnTriggerStay2D(Collider2D other){

		if (other.tag == "Player" && !isDamageDealt) {
			isDamageDealt = true;
			other.GetComponent<AudioSource>().Play();

			var player = other.GetComponent<PlayerController>();

			if(player.isFireResActive){
				HealthManager.HurtPlayer(0);
			}
			else HealthManager.HurtPlayer(1);

			dmgSplash.toggleOn ();

			player.knockbackCount = player.knockbackLength+0.095f;//przedluzonie o 0.095 to i tak duzo 
			if(other.transform.position.x <transform.position.x){
				player.knockFromRight = true;
			}
			else{
				player.knockFromRight = false;
			}
		}

		if (other.tag == "Enemy" || other.tag == "boss1_minion") {
			other.GetComponent<EnemyHealthManager> ().giveDamage (damageToGive);
		}
	}
}
