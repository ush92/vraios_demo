using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
	public bool isToggled;
	public GameObject inventoryMenuCanvas;
	public int activeItem;
	PauseMenu pause;
	//PlayerController player;
	private int activeButton;
	public int itemsCount;
	private Button[] buttons;
	InventoryIcon icon;
	Sprite[] sprite;
	AudioSource[] audios;

	private bool pressed;

	public void Start ()
	{
		pause = FindObjectOfType<PauseMenu> ();
		buttons = GetComponentsInChildren<Button> ();
		//player = FindObjectOfType<PlayerController> ();
		icon = FindObjectOfType<InventoryIcon> ();
		audios = GetComponents<AudioSource> ();
		activeButton = 0;

		activeItem = PlayerPrefs.GetInt ("CurrentItem");
		itemsCount = PlayerPrefs.GetInt ("ItemsCount");
		//itemsCount = 9;
		for (int i=8; i >= itemsCount; i--) {
			buttons [i].interactable = false;
		}

		sprite = Resources.LoadAll<Sprite> ("Arts/items");

		icon.ChangeIcon (sprite [activeItem]);
	}

	public void Update ()
	{
		if (isToggled) {
			inventoryMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			inventoryMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
		}

		if (Input.GetButtonDown ("Fire2") && !pause.isPaused) {//y
			isToggled = !isToggled;
			Cursor.visible = !Cursor.visible;
			activeButton = activeItem;
		}	

		if (Input.GetButtonDown ("Fire3") && !pause.isPaused && isToggled) {//space
			ChooseItem ();
		}

		if (!pressed && Input.GetAxisRaw ("Horizontal") < 0 && !pause.isPaused && isToggled) {
			pressed = true;
			if (activeButton != 0) {
				activeButton--;
				audios[0].Play();
			}
		}

		if (!pressed && Input.GetAxisRaw ("Horizontal") > 0 && !pause.isPaused && isToggled) {
			pressed = true;
			if (activeButton != 8 && activeButton+1<itemsCount) {
				activeButton++;
				audios[0].Play();
			}
		}

		if (Input.GetAxisRaw("Horizontal") == 0 && !pause.isPaused && isToggled) {
			pressed = false;
		}

		buttons [activeButton].Select ();

		QuickChange ();//metoda do zmiany eq za pomocą cyfr
	}

	public void ChooseItem ()
	{

		Cursor.visible = !Cursor.visible;
		activeItem = activeButton;
		sprite = Resources.LoadAll<Sprite> ("Arts/items");


		icon.ChangeIcon (sprite [activeItem]);

		audios [activeItem + 9 - (2 * activeItem)].Play (); //unity skomplikowało mi sortowanie audio w tablicy, stad wzor na odp index

		if (isToggled) {
			isToggled = !isToggled;
		}

	}

	public void AddItem ()
	{
		buttons [itemsCount].interactable = true;
		itemsCount++;
	}

	public void QuickChange(){
		
		if (!pause.isPaused && !isToggled) {

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if (itemsCount >= 1 && activeItem != 0) {
					activeButton = 0;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				if (itemsCount >= 2 && activeItem != 1) {
					activeButton = 1;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				if (itemsCount >= 3 && activeItem != 2) {
					activeButton = 2;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				if (itemsCount >= 4 && activeItem != 3) {
					activeButton = 3;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				if (itemsCount >= 5 && activeItem != 4) {
					activeButton = 4;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha6)) {
				if (itemsCount >= 6 && activeItem != 5) {
					activeButton = 5;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha7)) {
				if (itemsCount >= 7 && activeItem != 6) {
					activeButton = 6;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha8)) {
				if (itemsCount >= 8 && activeItem != 7) {
					activeButton = 7;
					ChooseItem ();
				}
			}

		
			if (Input.GetKeyDown (KeyCode.Alpha9)) {
				if (itemsCount >= 9 && activeItem != 8) {
					activeButton = 8;
					ChooseItem ();
				}
			}
		}
	}
}
	

	public enum Item
	{
		BOMB,
		GUN,
		GUMBOOTS,
		ELECTRODE,
		FIRERES,
		GHOSTBOOTS,
		TORPEDO,
		MINI,
		GRAVITY_WATCH
	}

		