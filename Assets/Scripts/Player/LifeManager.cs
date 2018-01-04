using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

	public int lifeCounter;

	private Text lifeText;

	public GameObject gameOverScreen;
	public PlayerController player;
	public MusicController music;

	public string mainMenu;

	public float waitAfterGameOver;

	void Start () {
		lifeText = GetComponent<Text> ();

		lifeCounter = PlayerPrefs.GetInt("PlayerCurrentLives");

		player = FindObjectOfType<PlayerController> ();
		music = FindObjectOfType<MusicController> ();
	}
	

	void Update () {
		if (lifeCounter == 0) {
			player.gameObject.SetActive(false);
			music.TurnOffMusic();
			gameOverScreen.SetActive(true);
			PlayerPrefs.SetInt("PlayerCurrentLives",2);
		}
		lifeText.text = "" + lifeCounter;

		if (gameOverScreen.activeSelf) {
			waitAfterGameOver -= Time.deltaTime;
		}

		if (waitAfterGameOver < 0) {
			Application.LoadLevel(mainMenu);
		}
	}

	public void GiveLife(){
		lifeCounter++;
	}

	public void TakeLife(){
		lifeCounter--;
	}
}
