using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public string mainMenu;
	private Button[] buttons;
	public bool isPaused;

	public GameObject pauseMenuCanvas;

	InventoryMenu inventoryMenu;

	void Start(){
		inventoryMenu = FindObjectOfType<InventoryMenu> ();
		buttons = GetComponentsInChildren<Button> ();
		


	}

	void Update(){
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);

			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive(false);
			if(!inventoryMenu.isToggled){
				Time.timeScale = 1f;
			}
		}
		if (Input.GetButtonDown ("Cancel") && !inventoryMenu.isToggled) {//esc
			isPaused = !isPaused;
		}
		buttons [0].Select ();
	}
	public void Resume(){
		isPaused = false;
	}
	
	public void QuitGame(){
		Application.LoadLevel (mainMenu);
	}
}
