using UnityEngine;
using System.Collections;

public class PlayerScript2 : MonoBehaviour {

	private Transform activePlatform;
	//private Vector3 moveDistance;

	private bool grounded;
	private bool jumped;
	//private float tempSpeed;
	private Vector3 tempScale;
	public float jumpHeight;
	public float doubleJumpHeight;
	public float MaxSpeed;
	public bool doubleJumpOn;
	
	/// Start this instance.
	void Start () {
		grounded = false;
		jumped = false;
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
		doubleJump ();
		jump ();
	}

	// If the player hits something, he is grounded and interacts with this object.
	void OnCollisionEnter2D(Collision2D collision2D){	
		// TODO: Only detects hits from below.
		grounded = true;
		if (collision2D.collider.gameObject.GetComponent<Rigidbody2D>()){
			activePlatform = collision2D.collider.transform;
		}
		else{activePlatform = null;}
	}

	// There is no active platform on collision exit
	void OnCollisionExit2D(Collision2D collision2D){
		Debug.Log("Exit Platform");
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
			if( transform.localScale.x < 0 && Input.GetAxisRaw ("Horizontal") > 0 ||
			   	transform.localScale.x > 0 && Input.GetAxisRaw ("Horizontal") < 0) 
			{	
				tempScale.x *= -1;
				transform.localScale = tempScale;
			}

			// Move player horizontally
			rigidbody2D.velocity = tempSpeed;
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
