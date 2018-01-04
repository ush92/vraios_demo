using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour {
	
	private Image icon;

	public void ChangeIcon(Sprite newSprite){
		if (icon == null) {
			icon = GetComponent<Image> ();
		}

		icon.sprite = newSprite;
	}
}
