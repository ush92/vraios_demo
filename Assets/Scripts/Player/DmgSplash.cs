using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DmgSplash : MonoBehaviour {

	public bool isToggled;
	public float time;
	private float currentTime;

	void Start () {
		isToggled = false;
		currentTime = time;
	}
	

	void Update () {
		if (isToggled) {
			currentTime -= Time.deltaTime;
			gameObject.GetComponent<Image>().color = new Color(0.95f,0.05f,0.05f,currentTime/2);
			if(currentTime <=0){
				isToggled = false;
			}
		}
	}

	public void toggleOn(){
		isToggled = true;
		gameObject.GetComponent<Image>().color = new Color(1,0,0,0.5f);
		currentTime = time;
	}
}
