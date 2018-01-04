using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {
	
	public int pointsToAdd;

	public bool isEquipment;

	public bool isHpRegen;

	public bool isLifeUp;

	LifeManager life;

	InventoryMenu eq;
	OpenDoor door;

	void Start(){
		eq = FindObjectOfType<InventoryMenu> ();
		door = FindObjectOfType<OpenDoor> ();
		life = FindObjectOfType<LifeManager>();
	}
	
	public AudioSource pickedItemSoundEffect;
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerController> () != null) {
			if(!isEquipment && !isHpRegen){
				ScoreManager.AddPoints (pointsToAdd);
			}

			if(isEquipment){ 
				eq.AddItem();
				door.isOpened = true;
				door.Open();
			}

			if(isHpRegen){
				HealthManager.playerHp++;
			}

			if(isLifeUp){
				life.GiveLife();
			}

			pickedItemSoundEffect.Play ();
			Destroy (gameObject);
		}
	}
	
}
