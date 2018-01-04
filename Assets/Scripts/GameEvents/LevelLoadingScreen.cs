using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoadingScreen : MonoBehaviour {

	public bool isToggled;
	public float time;

	private Image bck;
	private Text txt;
	private string lvName;

	void Start () {
		isToggled = true;
		bck = GetComponentInChildren<Image> ();
		txt = GetComponentInChildren<Text> ();

		switch (PlayerPrefs.GetInt ("CurrentLevel")) {
		case 0:
			lvName = "\n\nRegardez City";
			break;
		case 1:
			lvName = "\n\nRegardez Port";
			break;

		case 2:
			lvName = "\n\nRegardez Power Station";
			break;
		case 3:
			lvName = "\n\nVRAIOS DEMO";
			break;
		}
	}
	

	void Update () {
		if (isToggled) {
			time -= Time.deltaTime;
			bck.color = new Color(0,0,0,time/4);
			txt.color = new Color(1,1,1,time/4);
			txt.text = "Level "+(PlayerPrefs.GetInt("CurrentLevel")/3 + 1).ToString()+"-"+(PlayerPrefs.GetInt("CurrentLevel")%3+1).ToString();
			txt.text += lvName;
			if(time <=0){
				gameObject.SetActive(false);
				isToggled = false;
			}
		}
	}
}
