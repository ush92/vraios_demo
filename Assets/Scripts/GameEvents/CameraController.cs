using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	
	public PlayerController player;
	public bool isFollowing;
	public float xOffset;
	public float yOffset;
	private float currentXOffset;
	private float currentYOffset;
	
	void Start ()
	{
		player = FindObjectOfType<PlayerController> ();
		
		isFollowing = true;

		currentXOffset = xOffset;
		currentYOffset = yOffset;
	}
	
	void Update ()
	{
		if (isFollowing) {
			transform.position = new Vector3 (player.transform.position.x + currentXOffset, player.transform.position.y + currentYOffset, transform.position.z);

			if (player.grounded && !player.onLadder && Input.GetAxisRaw ("Vertical") !=0) {
				if (Mathf.Abs (currentYOffset - yOffset) < 2) {
					currentYOffset += Time.deltaTime * 2 * Mathf.Sign (Input.GetAxisRaw ("Vertical"));
				}
			} else if (Mathf.Round (currentYOffset) == yOffset) {
				currentYOffset = 2.0f;
			} else if (currentYOffset < yOffset) {
				currentYOffset += 10 * Time.deltaTime;
			} else if (currentYOffset > yOffset) {
				currentYOffset -= 10 * Time.deltaTime;
			}
		}

	}
}
