using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour {


	public float appearTime;
	public float disappearTime;
	public float startingTime;

	void Start () {
		InvokeRepeating("Appear",startingTime,appearTime);
		InvokeRepeating("Disappear",startingTime+disappearTime,appearTime);
	}


	void Disappear(){
		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);

	}

	void Appear(){
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
	}
	
}
