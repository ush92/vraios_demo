using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	private bool playerInZone;

	public string levelToLoad;

	public OpenDoor door;
	
	InventoryMenu eq;
	LifeManager lifes;

	void Start () {
		eq = FindObjectOfType<InventoryMenu> ();
		lifes = FindObjectOfType<LifeManager> ();
		door = FindObjectOfType<OpenDoor> ();
		playerInZone = false;
	}
	

	void Update () {
		if(Input.GetAxisRaw("Vertical")>0 && playerInZone && door.isOpened){

			PlayerPrefs.SetInt ("ItemsCount", eq.itemsCount);
			PlayerPrefs.SetInt ("CurrentItem", eq.activeItem);
			PlayerPrefs.SetInt ("PlayerCurrentLives", lifes.lifeCounter);
			PlayerPrefs.SetInt ("PlayerMaxHealth", HealthManager.playerHp > 5 ? HealthManager.playerHp : 5);
			PlayerPrefs.SetInt ("PlayerCurrentHealth", HealthManager.playerHp);
			PlayerPrefs.SetInt ("PlayerCurrentScore", ScoreManager.GetPoints());
			PlayerPrefs.SetInt ("CurrentLevel",PlayerPrefs.GetInt("CurrentLevel")+1);

			PlayerPrefs.Save();
			Application.LoadLevel(levelToLoad);
		}
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.name == "Player") {
			playerInZone = false;
		}
	}
}
