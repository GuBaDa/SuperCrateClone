﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] // Hides var below from inspector
	public bool grounded;
	public bool dead;

	private bool doubleJump;
	private Transform activePlatform;
	private Vector3 tempScale;
	private float doubleJumpHeight;
	public bool canBoost;
	private bool isBoosting = false;
	public Vector3 weaponPos;
	public float jumpHeight;
	public bool doubleJumpOn;

	// Property variables
	private float health;
	private int experience;
	public float maxSpeed;



	//Weapon
	public GameObject weaponActive;


	
	public GameObject dust;

	// PlayerController variables
	private PlayerController plControllerScript; 

	public int PlayerControlNr;
	private float axisHorizontal;
	private float axisVertical;
	private bool fire1Btn;
	private bool fire2Btn;
	private bool fire3Btn;
	private bool jumpBtnDown;


	//TESTING
	public Transform GroundCheck;
	public LayerMask collisionLayer;
	public Transform SideCheck;
	bool sideClear;



	/// Start this instance.
	/// 
	void Awake () {
		health = 100f;
		grounded = false;
		doubleJump = true;
		doubleJumpHeight = jumpHeight * .75f;
		sideClear = true;
		tempScale = transform.localScale;

		plControllerScript = GetComponent<PlayerController>(); 
	}

	
	// FixedUpdate is called on fixed Times, use this for physics movements.
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle(GroundCheck.position, .45f, collisionLayer);
		sideClear = !Physics2D.OverlapArea(SideCheck.position,new Vector3(SideCheck.position.x+(.1f*transform.localScale.x),SideCheck.position.y-.88f,SideCheck.position.z),collisionLayer);

		if (!dead && !isBoosting) {
			doMove ();
		}
		resetGame ();
	}


	// Update is called on each frame, this way the character immediately reacts to jumps.
	void Update(){
		getControls ();

		// This code is necessary for one-way platforms
		if(rigidbody2D.velocity.y < 0){
			Physics2D.IgnoreLayerCollision(2,11,false);
		}
		
		if (!dead) {
			if ((canBoost) && Input.GetButtonDown ("Fire2")) {
				StartCoroutine( Boost(.15f) ); //Start the Coroutine called "Boost", and feed it the time we want it to boost us
			} 
			OnDeath ();
			doDoubleJump();
			if(grounded)doJump ();
		}
	}




	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void doMove(){
		if (axisHorizontal != 0) {
			// Get speed in correct direction
			Vector2 tempSpeed =  new Vector2 (axisHorizontal * maxSpeed, rigidbody2D.velocity.y);

			// Move player horizontally only if it is not blocked by a wall 


			// Determines if the character has to flip to before moving.
			if( transform.localScale.x < 0 && ( axisHorizontal > 0) || transform.localScale.x > 0 && ( axisHorizontal  < 0)) 
	   		{	
	   			tempScale.x *= -1;
	   			transform.localScale = tempScale;
	   		}

			// The character can only move if there is not an obstacle in the way. (Prevents sticking on walls)
			if(tempSpeed.x < 0 && sideClear){
				rigidbody2D.velocity = tempSpeed;
			}
			if(tempSpeed.x > 0 && sideClear){
				rigidbody2D.velocity = tempSpeed;
			}

		}
	}

	// Jump is possible when grounded.
	void doJump(){
		if (jumpBtnDown && grounded) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
			Physics2D.IgnoreLayerCollision(2,11,true);
			grounded = false;
			doubleJump = true;
			dustCast ();
		}
	}

	// Doublejump is possible once after jumping.
	void doDoubleJump(){
		if (doubleJumpOn) {
			if(jumpBtnDown && !grounded && doubleJump ){
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, doubleJumpHeight);
				Physics2D.IgnoreLayerCollision(2,11,true);
				doubleJump = false;
			}
		}
	}
	
	/// Misc functions, OnDeath, etc.. 
	/// 
	/// 
	void OnDeath(){
		if (Health == 0) {
			dead = true;
			BoxCollider2D[] bc = GetComponents<BoxCollider2D>();

			bc[0].size = new Vector2(bc[0].size.y,.3f);
			bc[0].center = new Vector2(.02f,-.5f);


		}
	}
	void destroyPlayer(){
		Destroy(gameObject);
	}

	void resetGame(){
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
		plControllerScript.PlayerControlNr = PlayerControlNr;
		// Get variables
		axisHorizontal = plControllerScript.AxisHorizontal;
		axisVertical = plControllerScript.AxisVertical;
		fire1Btn = plControllerScript.Fire1Btn;
		fire2Btn = plControllerScript.Fire2Btn;
		fire3Btn = plControllerScript.Fire3Btn;
		jumpBtnDown = plControllerScript.JumpBtnDown;

	}
	// SKILS //

	IEnumerator Boost(float boostDur) { //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
		Vector2 boostSpeedRight = new Vector2(30,0);
		Vector2 boostSpeedLeft = new Vector2(-30,0);
		float boostCooldown = 2f;
		isBoosting = true;
		Vector2 initialSpeed = rigidbody2D.velocity;
		float time = 0; //create float to store the time this coroutine is operating
		canBoost = false; //set canBoost to false so that we can't keep boosting while boosting
		
		while(boostDur > time) { //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
			time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
			if(transform.localScale.x == 1){
				rigidbody2D.velocity = boostSpeedRight; //set our rigidbody velocity to a custom velocity every frame, so that we get a steady boost direction like in Megaman
			} else {
				rigidbody2D.velocity = boostSpeedLeft;
			}
			yield return 0; //go to next frame
		}
		rigidbody2D.velocity = initialSpeed;
		isBoosting = false;
		yield return new WaitForSeconds(boostCooldown); //Cooldown time for being able to boost again, if you'd like.
		canBoost = true; //set back to true so that we can boost again.
		
	}



	//  Properties /// 
	public float Health{
		get{return health;}
		set{health = Mathf.Clamp (value, 0f, 100f);}
	}
	
	public int Experience{
		get{return experience;}
		set{experience = value;}
	}
	
	public float MaxSpeed{
		get{return maxSpeed;}
		set	{maxSpeed = value;}
	}
	

}





