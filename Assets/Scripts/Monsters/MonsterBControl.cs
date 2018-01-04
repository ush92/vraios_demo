using UnityEngine;
using System.Collections;

public class MonsterBControl : MonoBehaviour {

	public float jumpHeight;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool grounded;
	private bool doubleJumped;

	void Start () {
	
	}

	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	void Update () {
	
		if (grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
		}
	}
}
