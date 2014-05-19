using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] // Hides var below from inspector
	public bool grounded;
	[HideInInspector] // Hides var below from inspector
	public bool jumped;

	private bool wallLeft, wallRight;
	private Transform activePlatform;
	private Vector3 tempScale;
	public float jumpHeight;
	private float doubleJumpHeight;
	public float MaxSpeed;
	public bool doubleJumpOn;

	public GameObject dust;
	/// Start this instance.
	/// 
	void Start () {
		grounded = false;
		jumped = false;
		wallLeft = false;
		wallRight = false;
		doubleJumpHeight = jumpHeight * .75f;
		tempScale = transform.localScale;
	}

	
	// FixedUpdate is called on fixed Times, use this for physics movements.
	void FixedUpdate () {


		move ();
		OnDeath ();
	}

	// Update is called on each frame, this way the character immediately reacts to jumps.
	void Update(){
		// Get all horizontal velocity properties from the object you're touching.

		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 1f);
		if (hit.collider != null ) {
			wallLeft = false;
			wallRight = false;
			grounded = true;
		}

		if (activePlatform != null) {
			Vector2 tempVelocity;
			tempVelocity.x = activePlatform.rigidbody2D.velocity.x;
			tempVelocity.y = transform.rigidbody2D.velocity.y;
			transform.rigidbody2D.velocity = tempVelocity;
		}

	


		// Flip sprite if mouse player is not facing the mouse
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if( transform.localScale.x < 0 && ( worldMousePos.x > transform.position.x) ||
			   	transform.localScale.x > 0 && ( worldMousePos.x  < transform.position.x)) 
		{	
			tempScale.x *= -1;
			transform.localScale = tempScale;
		}

		doubleJump ();
		jump ();
	}

	// If the player hits something, he is grounded and interacts with this object.
	void OnCollisionEnter2D(Collision2D collision2D){	
		foreach (ContactPoint2D contact in collision2D.contacts) {
			if (contact.normal == Vector2.up) {
				if (contact.collider.rigidbody2D != null) {
					activePlatform = contact.collider.transform;
				} else {
					activePlatform = null;
				}
				grounded = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts) {
			if( (contact.normal == Vector2.right) ){
				Vector2 playerSpeed = rigidbody2D.velocity;
				playerSpeed.x = 0;
				wallLeft = true;
				rigidbody2D.velocity = playerSpeed;
			} else if (contact.normal == (Vector2.right*-1)){
				Vector2 playerSpeed = rigidbody2D.velocity;
				playerSpeed.x = 0;
				wallRight = true;
				rigidbody2D.velocity = playerSpeed;
			}
		}
	}

	// There is no active platform on collision exit
	void OnCollisionExit2D(Collision2D collision2D){
		wallLeft = false;
		wallRight = false;
		activePlatform = null;
	}


	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void move(){
		if (Input.GetButton  ("Horizontal")) {
			// Get speed in correct direction
			Vector2 tempSpeed =  new Vector2 (Input.GetAxisRaw ("Horizontal") * MaxSpeed, rigidbody2D.velocity.y);

			// Move player horizontally only if it is not blocked by a wall 
			//RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector3.right, .5f);
			//RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector3.left, .5f);

			if(!wallRight && !wallLeft){
				rigidbody2D.velocity = tempSpeed;
			}

		}
	}
	void jump(){
		if (Input.GetButtonDown  ("Jump") && grounded) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
			grounded = false;
			jumped = true;
			dustCast ();
		}
	}
	void doubleJump(){
		if (doubleJumpOn) {
			if(Input.GetButtonDown("Jump") && jumped ){
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, doubleJumpHeight);
				jumped = false;
			}
		}
	}
	
	/// Misc functions, OnDeath, LoseHP(int), etc.. 
	/// 
	/// 
	void OnDeath(){
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel(Application.loadedLevel);
			grounded = false;
		
		}
	}

	void dustCast(){
		GameObject pDust = (GameObject) Instantiate (dust);
		pDust.transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
	}
	
}
