using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public static int playerHp;
	
	Text text;
	
	private LevelManager levelManager;
	
	public bool isDead;

	public LifeManager lifeSystem;

	void Start () {
		text = GetComponent<Text>();
		playerHp = PlayerPrefs.GetInt ("PlayerCurrentHealth");
		levelManager = FindObjectOfType<LevelManager> ();
		lifeSystem = FindObjectOfType<LifeManager> ();
		isDead = false;
	}
	
	void Update () {
		if (playerHp <= 0 && !isDead) {
			playerHp = 0;
			lifeSystem.TakeLife();
			levelManager.RespawnPlayer();
			isDead = true;
		}
		
		text.text = ""+playerHp;
	}
	
	public static void HurtPlayer(int damageToGive){
		playerHp -= damageToGive;

		if (playerHp < 0) {
			playerHp = 0;
		}
	}
	
	public void FullHp(){
		playerHp = PlayerPrefs.GetInt ("PlayerMaxHealth");
	}

	public void KillPlayer(){
		playerHp = 0;
	}
}
