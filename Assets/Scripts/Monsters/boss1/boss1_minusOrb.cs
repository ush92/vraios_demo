using UnityEngine;
using System.Collections;

public class boss1_minusOrb : MonoBehaviour {

	private boss1_DebuffManager debuffManager;
	public GameObject sparks;

	private int dmgCounter;

	void Start () {
		debuffManager = FindObjectOfType<boss1_DebuffManager> ();
		dmgCounter = 0;
	}
	

	void Update () {

	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" && debuffManager.activeDebuff == "plus") {
			dmgCounter++;
			if (dmgCounter >95){

				HealthManager.HurtPlayer(10000);
				dmgCounter = 0;
			}
			Instantiate(sparks, transform.position, transform.rotation);
			other.GetComponent<AudioSource>().Play();
		}
	}	

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			dmgCounter = 0;
		}
	}
}
