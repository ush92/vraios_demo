using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	private Animator anim;
	public bool isOpened;

	void Start () {
		anim = GetComponent<Animator> ();
		isOpened = false;
		anim.SetBool ("isOpen", false);

	}

	public void Open(){
		anim.SetBool ("isOpen", true);
	}

}
