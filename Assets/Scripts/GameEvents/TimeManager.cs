using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public float startingTime;
	public float countingTime;
	private Text timeText;

	private PauseMenu pauseMenu;
	private HealthManager health;

	void Start () {
		countingTime = startingTime;
		timeText = GetComponent<Text> ();
		pauseMenu = FindObjectOfType<PauseMenu> ();
		health = FindObjectOfType<HealthManager> ();
	}

	void Update () {
		if (pauseMenu.isPaused) {
			return;
		}
		countingTime -= Time.deltaTime;

		if (countingTime <= 0) {
			health.KillPlayer();
		}

		timeText.text = "" + Mathf.Round(countingTime);
	}

	public void ResetTime(){
		countingTime = startingTime;
	}
}
