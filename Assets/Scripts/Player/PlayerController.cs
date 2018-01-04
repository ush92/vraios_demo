using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		
	public Rigidbody2D myRigidBody2D;
	private Animator anim;
	AudioSource jumpAudio;
	PauseMenu pause;
	InventoryMenu eq;

	//chodzenie/skakanie
	public float moveSpeed;
	private float moveVelocity;
	public float jumpHeight;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool grounded;
	private bool doubleJumped;

	//strzelanie
	public Transform firePoint;
	public GameObject bullet;
	public GameObject electrode;
	public float shotDelay;
	public float shotDelayCounter;

	//knockback
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	//drabina
	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	public float gravityStore;

	//bomba i torpeda
	public GameObject bomb;
	public GameObject torpedo;
	public bool isBombPlaced;
	AudioSource placeBombAudio;

	//fire res
	private Color redColor;
	private Color normalColor;
	public float fireResDelay;
	private float fireResCounter;
	public bool isFireResActive;
	private bool isFireResCooldown;
	private float fireResCooldownCounter;
	AudioSource fireResAudio;

	//minimalizator
	private float minimaliseMultipler;
	private bool isMini;
	AudioSource miniAudio;

	//gravity watch
	public bool isGravityWatchActive;
	private Color blueColor;
	AudioSource gravityWatchAudio;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		pause = FindObjectOfType<PauseMenu> ();
		eq = FindObjectOfType<InventoryMenu> ();
		myRigidBody2D = GetComponent<Rigidbody2D> ();

		AudioSource[] audios = GetComponents<AudioSource> ();
		jumpAudio = audios [1];
		placeBombAudio = audios [2];
		gravityWatchAudio = audios [3];
		fireResAudio = audios [4];
		miniAudio = audios [5];


		gravityStore = myRigidBody2D.gravityScale;
		isBombPlaced = false;
		isFireResActive = false;
		isMini = false;
		minimaliseMultipler = 1f;

		redColor = new Color (1, 0, 0);
		normalColor = new Color (1, 1, 1);
		blueColor = new Color (0.5f, 0.5f, 1f, 0.5f);

	}
	
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	void Update ()
	{
		if (!pause.isPaused && !eq.isToggled) {

			if (grounded) {
				doubleJumped = false;
			}

			anim.SetBool ("Grounded", grounded);

			//skakanie
			if (Input.GetButtonDown ("Jump") && grounded && !isGravityWatchActive) { //skok
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
			}//podwojny skok 
			if (Input.GetButtonDown ("Jump") && !grounded && !doubleJumped && !isGravityWatchActive) {
				if (eq.activeItem == (int)Item.GUMBOOTS) { //jezeli aktywny item to gumowe buty :D
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight + 2);
				} else {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight - 3);
				}
				doubleJumped = true;
				jumpAudio.Play ();
			
			}

			//chodzenie
			moveVelocity = 0f;
			moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");

			if (knockbackCount <= 0) { //knockback
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				if (knockFromRight) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockback, knockback);
				} else if (!knockFromRight) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockback, knockback);
				}
				knockbackCount -= Time.deltaTime;
			}

			anim.SetFloat ("Speed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x));

			//zmiana animacji lewo/prawo
			if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
				transform.localScale = new Vector3 (1.0f*minimaliseMultipler, 1.0f*minimaliseMultipler, 1.0f);
			} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
				transform.localScale = new Vector3 (-1.0f*minimaliseMultipler, 1.0f*minimaliseMultipler, 1.0f);
			}


			//strzelanie malymi bombami
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.GUN) { 
				Instantiate (bullet, firePoint.position, firePoint.rotation);
				shotDelayCounter = shotDelay;
			}
			if (Input.GetButton ("Fire1") && eq.activeItem == (int)Item.GUN) {
				shotDelayCounter -= Time.deltaTime;
				if (shotDelayCounter <= 0) {
					shotDelayCounter = shotDelay;
					Instantiate (bullet, firePoint.position, firePoint.rotation);
				}
			}


			//strzelanie elektrodami
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.ELECTRODE) { 
				Instantiate (electrode, firePoint.position, firePoint.rotation);
				shotDelayCounter = shotDelay;
			}
			if (Input.GetButton ("Fire1") && eq.activeItem == (int)Item.ELECTRODE) {
				shotDelayCounter -= Time.deltaTime;
				if (shotDelayCounter <= 0) {
					shotDelayCounter = shotDelay;
					Instantiate (electrode, firePoint.position, firePoint.rotation);
				}
			}


			//strzelanie torpedami 
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.TORPEDO) { 
				Instantiate (torpedo, firePoint.position, firePoint.rotation);
				shotDelayCounter = shotDelay;
			}
			if (Input.GetButton ("Fire1") && eq.activeItem == (int)Item.TORPEDO) {
				shotDelayCounter -= Time.deltaTime;
				if (shotDelayCounter <= 0) {
					shotDelayCounter = shotDelay;
					Instantiate (torpedo, firePoint.position, firePoint.rotation);
				}
			}

			//postawienie duzej bomby
			if (Input.GetButtonDown ("Fire1") && !isBombPlaced && !onLadder && grounded && eq.activeItem == (int)Item.BOMB) { 
				Instantiate (bomb, GetComponent<Rigidbody2D> ().position, firePoint.rotation);
				placeBombAudio.Play ();
				isBombPlaced = true;
			}

			//fire res
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.FIRERES && !isFireResActive && !isFireResCooldown) {
				isFireResActive = true;
				isFireResCooldown = true;
				GetComponent<SpriteRenderer> ().color = redColor;
				fireResCounter = fireResDelay;
				fireResCooldownCounter = fireResDelay;
				fireResAudio.Play();
			}
			if (Input.GetButton ("Fire1") && eq.activeItem == (int)Item.FIRERES && !isFireResActive && !isFireResCooldown) {
				isFireResActive = true;
				isFireResCooldown = true;
				if (fireResCounter <= 0) {
					GetComponent<SpriteRenderer> ().color = redColor;
				}
			}
			fireResCounter -= Time.deltaTime;
			if (fireResCounter <= 0) {
				isFireResActive = false;
				if (!isGravityWatchActive) {
					GetComponent<SpriteRenderer> ().color = normalColor;
				}
			}

			if(!isFireResActive)
			{
				fireResCooldownCounter -= Time.deltaTime;
				if (fireResCooldownCounter <= 0) {
					isFireResCooldown = false;
				}
			}

			//buty przyspieszenia 
			if (Input.GetButton ("Fire1") && eq.activeItem == (int)Item.GHOSTBOOTS) { 
				moveSpeed = 8f;
			}
			if (Input.GetButtonUp ("Fire1") && eq.activeItem == (int)Item.GHOSTBOOTS) {
				moveSpeed = 5f;
			}
			if (eq.activeItem != (int)Item.GHOSTBOOTS && eq.activeItem != (int)Item.GRAVITY_WATCH) {
				moveSpeed = 5f;
			}

			//magic watch 
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.GRAVITY_WATCH) { 
				if (isGravityWatchActive) {
					myRigidBody2D.gravityScale = gravityStore;
					jumpHeight = 15;
					moveSpeed = 5;
					GetComponent<SpriteRenderer> ().color = normalColor;
					isGravityWatchActive = !isGravityWatchActive;
				} else {
					myRigidBody2D.gravityScale = 0.05f;
					jumpHeight = 0;
					moveSpeed = 0;
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					GetComponent<SpriteRenderer> ().color = blueColor;
					isGravityWatchActive = !isGravityWatchActive;
					gravityWatchAudio.Play();
				}

				Debug.Log ("magic watch");
			}
			if (eq.activeItem != (int)Item.GRAVITY_WATCH) {
				myRigidBody2D.gravityScale = gravityStore;


				if(!isFireResActive){
					GetComponent<SpriteRenderer> ().color = normalColor;
				}

				isGravityWatchActive = false;

				if(eq.activeItem != (int)Item.GHOSTBOOTS){
					moveSpeed = 5;
				}

				if(eq.activeItem != (int)Item.MINI){
					jumpHeight = 15;
				}
			}


			//minimalizator
			if (Input.GetButtonDown ("Fire1") && eq.activeItem == (int)Item.MINI && !isMini) { 
				minimaliseMultipler = 0.5f;
				jumpHeight = 11f;
				isMini = true;
				miniAudio.Play ();
			}
			if (eq.activeItem != (int)Item.MINI) {
				minimaliseMultipler = 1f;
				jumpHeight = 15f;
				isMini = false;
			}


			//drabina
			if(!isGravityWatchActive){
				if (onLadder) {
					myRigidBody2D.gravityScale = 0f;
					climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");


					myRigidBody2D.velocity = new Vector2 (myRigidBody2D.velocity.x, climbVelocity);
				}
				if (!onLadder && !isGravityWatchActive) {
					myRigidBody2D.gravityScale = gravityStore;

				}
				anim.SetBool ("OnLadder", onLadder);
				anim.SetFloat ("LadderSpeed", Mathf.Abs (climbVelocity));
			}
		}

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.transform.tag == "MovingPlatform") {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.transform.tag == "MovingPlatform") {
			transform.parent = null;
		}
	}
}
