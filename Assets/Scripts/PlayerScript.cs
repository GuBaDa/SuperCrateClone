﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] // Hides var below from inspector
	public bool grounded;

	private bool jumped;
	private bool wallLeft, wallRight;
	private Transform activePlatform;
	private Vector3 tempScale;
	public float jumpHeight;
	private float doubleJumpHeight;
	public bool doubleJumpOn;

	// Property variables
	private float health;
	private int experience;
	public float maxSpeed;

	// 
	public float Health{
		get{return health;}
		set{health = value;}
	}

	public int Experience{
		get{return experience;}
		set{experience = value;}
	}

	public float MaxSpeed{
		get{return maxSpeed;}
		set	{maxSpeed = value;}
	}
	
	public GameObject dust;

	// PlayerController variables
	public int PlayerControlNr;
	private float axisHorizontal;
	private float axisVertical;
	private bool fire1Btn;
	private bool fire2Btn;
	private bool fire3Btn;
	private bool jumpBtnDown;

	/// Start this instance.
	/// 
	void Awake () {
		health = 100f;
		grounded = false;
		jumped = false;
		wallLeft = false;
		wallRight = false;
		doubleJumpHeight = jumpHeight * .75f;
		tempScale = transform.localScale;
	}

	
	// FixedUpdate is called on fixed Times, use this for physics movements.
	void FixedUpdate () {
		doMove ();
	}

	// Update is called on each frame, this way the character immediately reacts to jumps.
	void Update(){
		getControls();
		// If the player hits the ground, make sure he can move normally again.
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 1f);
		if (hit.collider != null) {

			wallLeft = false;
			wallRight = false;
			grounded = true;
		}
		
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

		OnDeath ();
		doDoubleJump ();
		doJump ();
	}

	void OnCollisionEnter2D(Collision2D collision2D){	
		foreach (ContactPoint2D contact in collision2D.contacts) {
			// If the player hits something below him, he is grounded and interacts with this object.
			if (contact.normal == Vector2.up) {
				if (contact.collider.rigidbody2D != null) {
					activePlatform = contact.collider.transform;
					//Debug.Log("New Collision: I just hit the ground below me!");
					grounded = true;
				} else {
					activePlatform = null;
				}
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
		grounded = false;
		activePlatform = null;
	}


	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void doMove(){
		if (axisHorizontal != 0) {
			// Get speed in correct direction
			Vector2 tempSpeed =  new Vector2 (axisHorizontal * maxSpeed, rigidbody2D.velocity.y);

			// Move player horizontally only if it is not blocked by a wall 
			if(tempSpeed.x < 0 && !wallLeft){
				rigidbody2D.velocity = tempSpeed;
			}
			if(tempSpeed.x > 0 && !wallRight){
				rigidbody2D.velocity = tempSpeed;
			}

		}
	}

	// Jump is possible when grounded.
	void doJump(){
		if (jumpBtnDown && grounded) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
			grounded = false;
			jumped = true;
			dustCast ();
		}
	}


	void doDoubleJump(){
		if (doubleJumpOn) {
			if(jumpBtnDown && jumped ){
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, doubleJumpHeight);
				jumped = false;
			}
		}
	}
	
	/// Misc functions, OnDeath, LoseHP(int), etc.. 
	/// 
	/// 
	void OnDeath(){
		if (Health == 0) {
			Destroy(gameObject);
		}
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel(Application.loadedLevel);
			grounded = false;
		}
	}

	void dustCast(){
		GameObject pDust = (GameObject) Instantiate (dust);
		pDust.transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
	}

	void getControls(){
		// Set control script to right player
		GetComponent<PlayerController>().PlayerControlNr = PlayerControlNr;
		// Get variables
		axisHorizontal = GetComponent<PlayerController>().AxisHorizontal;
		axisVertical = GetComponent<PlayerController>().AxisVertical;
		fire1Btn = GetComponent<PlayerController>().Fire1Btn;
		fire2Btn = GetComponent<PlayerController>().Fire2Btn;
		fire3Btn = GetComponent<PlayerController>().Fire3Btn;
		jumpBtnDown = GetComponent<PlayerController>().JumpBtnDown;

	}
}






