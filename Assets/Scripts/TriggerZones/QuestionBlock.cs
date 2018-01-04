using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionBlock : MonoBehaviour {

	public GameObject text;

	void Start () {
		text.GetComponent<Canvas> ().enabled = false;
	}

	void Update(){

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerController> () != null) {
			text.GetComponent<Canvas>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<PlayerController> () != null) {
			text.GetComponent<Canvas> ().enabled = false;
		}
	}
}
