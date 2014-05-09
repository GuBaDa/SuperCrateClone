using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] // Hides var below from inspector
	public bool grounded;
	[HideInInspector] // Hides var below from inspector
	public bool jumped;

	private Transform activePlatform;
	private Vector3 tempScale;
	public float jumpHeight;
	private float doubleJumpHeight;
	public float MaxSpeed;
	public bool doubleJumpOn;
	
	/// Start this instance.
	void Start () {
		grounded = false;
		jumped = false;
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
		// TODO: Only detects hits from below.
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 1f);
		if (hit.collider != null) {
			if (hit.collider.rigidbody2D != null) {
				activePlatform = hit.collider.transform;
			} else {
				activePlatform = null;
			}
			//Debug.Log (hit.collider.gameObject.name);
			grounded = true;
		}
	}

	// There is no active platform on collision exit
	void OnCollisionExit2D(Collision2D collision2D){
		activePlatform = null;
	}


	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void move(){
		if (Input.GetButton  ("Horizontal")) {
			// Get speed in correct direction
			Vector2 tempSpeed =  new Vector2 (Input.GetAxisRaw ("Horizontal") * MaxSpeed, rigidbody2D.velocity.y);

			// Flip sprite if direction changes
			/*
			if( transform.localScale.x < 0 && Input.GetAxisRaw ("Horizontal") > 0 ||
			   	transform.localScale.x > 0 && Input.GetAxisRaw ("Horizontal") < 0) 
			{	
				tempScale.x *= -1;
				transform.localScale = tempScale;
			}
			*/

			// Move player horizontally if it is not blocked by a wall
			RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector3.right, .5f);
			RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector3.left, .5f);
			if(hitRight.collider == null && tempSpeed.x > 0){
				rigidbody2D.velocity = tempSpeed;
			}
			if(hitLeft.collider == null && tempSpeed.x < 0){
				rigidbody2D.velocity = tempSpeed;
			}
		}
	}
	void jump(){
		if (Input.GetButtonDown  ("Jump") && grounded) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
			grounded = false;
			jumped = true;
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
		if (transform.position.y < -15) {
			Application.LoadLevel(Application.loadedLevel);
			grounded = false;
		
		}
	}
	
}
