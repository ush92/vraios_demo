using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {
	
	private PlayerController player;

	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			player.onLadder = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			player.onLadder = false;
		}
	}
}
