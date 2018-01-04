using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	private Button[] buttons;

	public GameObject about;

	void Start(){
		buttons = GetComponentsInChildren<Button> ();
	
		buttons [0].Select ();

		if (PlayerPrefs.GetInt ("CurrentLevel") == 0) { //jezeli gracz jest w pierwszej rundzie to nie mozna kliknac continue
			buttons [0].interactable = false;
		} else {
			buttons [0].interactable = true;
		}

	}

	public void NewGame(){

		PlayerPrefs.SetInt ("PlayerCurrentLives", 2);
		PlayerPrefs.SetInt ("PlayerCurrentScore", 0);
		PlayerPrefs.SetInt ("PlayerCurrentHealth", 5);
		PlayerPrefs.SetInt ("PlayerMaxHealth", 5);
		PlayerPrefs.SetInt("ItemsCount", 1);
		PlayerPrefs.SetInt ("CurrentItem", 0);
		PlayerPrefs.SetInt ("CurrentLevel", 0);

		Application.LoadLevel ("level"+PlayerPrefs.GetInt("CurrentLevel").ToString());
	}

	public void Continue(){
		Application.LoadLevel ("level"+PlayerPrefs.GetInt("CurrentLevel").ToString());
	}

	public void About(){
		about.SetActive (true);
		about.GetComponentInChildren<Button> ().Select ();
	}

	public void Return(){
		about.SetActive (false);
		buttons [0].Select ();
	}


	public void QuitGame(){
		Application.Quit ();
	}
}
