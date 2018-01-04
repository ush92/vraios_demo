using UnityEngine;
using System.Collections;

public class boss1_DebuffManager : MonoBehaviour {
	
	public bool isBossActive; //czy encounter jest wlaczony
	public string activeDebuff; //aktywny debuff na graczu (plus/minus)
	public int debuffCounter; //ilosc eventu zmieniajacego buff
	private int eventDelay; //czas miedzy spawnem minionów i zmianą polaryzacji
	private boss1_blackOrb blackOrb; //czarna kula respująca miniony

	public GameObject minus; //czerwona kula
	public GameObject plus;  //niebieska kula
	public GameObject platform; //platforma po pokonaniu bosa

	private bool isBossBelow; //czy boss zszedl z platformy
	public GameObject deathEffect;
	private PlayerController player;
	private GameObject[] minions;
	public GameObject boss;
	AudioSource teleportAudio;

	void Start () {
		isBossActive = false;
		activeDebuff = null;
		isBossBelow = false;
		debuffCounter = 0;
		eventDelay = 2;
		blackOrb = FindObjectOfType<boss1_blackOrb> ();
		player = FindObjectOfType<PlayerController> ();
		teleportAudio = GetComponent<AudioSource> ();
		platform.gameObject.SetActive (false);

	}

	void Update () {
		if (activeDebuff == "plus") { //zmiana pozycji malych plusików i minusików
			plus.transform.position = new Vector3(player.transform.position.x, player.transform.position.y +0.8f, 0);
			minus.transform.position = new Vector3(43,7,0);
		} 
		else if (activeDebuff == "minus") {
			minus.transform.position = new Vector3(player.transform.position.x, player.transform.position.y +0.8f, 0);
			plus.transform.position = new Vector3(53,7,0);
		}

		if (HealthManager.playerHp <= 0 && isBossActive) { //reset jezeli gracz umrze
			ResetEncounter();

			boss.GetComponent<EnemyHealthManager>().enemyHealth = 50;
			minus.transform.position = new Vector3(43,7,0);
			plus.transform.position = new Vector3(53,7,0);
		}

		if (boss.GetComponent<EnemyHealthManager> ().enemyHealth <= 0 && isBossActive) {// jezeli bos umrze
			ResetEncounter();
			Instantiate(deathEffect, transform.position, transform.rotation);
			minus.SetActive(false);
			plus.SetActive(false);

			platform.gameObject.SetActive (true);
			GetComponent<Collider2D>().enabled = false;
		}
	}

	void GenerateDebuff(){
		if (Random.Range (0, 9) % 2 == 0) {
			activeDebuff = "plus";
		} else {
			activeDebuff = "minus";
		}
		teleportAudio.Play();
		debuffCounter++;
		//Debug.Log ("" + debuffCounter);

		if (debuffCounter == 5) { //w co piątym evencie teleportuj bosa na dol
			debuffCounter = 0;
			isBossBelow = !isBossBelow;
			if (isBossBelow) {
				boss.transform.localPosition = new Vector3(5f, 2f, 0);
			} else {
				boss.transform.localPosition = new Vector3(4.5f, 4.95f ,0);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(!isBossActive){
				InvokeRepeating("GenerateDebuff",3,eventDelay); //w 3 sekundzie, co 2 sekundy zmieniaj debuff
				blackOrb.InvokeRepeating ("Spawn",3,eventDelay);//w 3 sekundzie, co 2 sekundy resp miniona
			}
			isBossActive = true;
		}
	}

	void ResetEncounter(){
		isBossActive = false;
		isBossBelow = false;
		activeDebuff = null;
		debuffCounter = 0;
		boss.transform.localPosition = new Vector3(4.5f, 4.95f ,0);
		CancelInvoke("GenerateDebuff");
		blackOrb.CancelInvoke("Spawn");
		minions =  GameObject.FindGameObjectsWithTag ("boss1_minion");
		for(var i = 0 ; i < minions.Length ; i ++){
			Destroy(minions[i]);
		}
	}
}
