using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private Transform activePlatform;
	private Vector3 moveDistance;

	private bool grounded;
	private bool jumped;
	private float tempSpeed;
	private float jumpHeight = 5;
	private float doubleJumpHeight = 5;
	public float MaxSpeed = 2;
	public bool doubleJumpOn;
	
	/// Start this instance.
	void Start () {
		grounded = false;
		jumped = false;
	}
	

	
	// FixedUpdate is called on fixed Times, use this for physics movements.
	void FixedUpdate () {
		move ();
		OnDeath ();
	}

	// Update is called on each frame, this way the character immediatly reacts to jumps.
	void Update(){

		if (activePlatform == null) {
			transform.parent = null;

		}
		doubleJump ();
		jump ();
	}
	
	void OnCollisionEnter2D(Collision2D collision2D){	
		if (collision2D.gameObject.name =="Ground" || collision2D.gameObject.name =="Platform" ) {
			//Debug.Log("Hit ground");
			transform.parent = null;
			grounded = true;
			if(collision2D.gameObject.name =="Platform") {
				Debug.Log("Hit Platform");
				activePlatform = collision2D.collider.transform;
				transform.parent = activePlatform;

			}
		}
	}

	void OnCollisionExit2D(Collision2D collision2D){
		Debug.Log("Exit Platform");
		activePlatform = null;
	}


	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void move(){
		if (Input.GetButton  ("Horizontal")) {
			tempSpeed = Input.GetAxisRaw ("Horizontal") * MaxSpeed;
			rigidbody2D.velocity = new Vector2 (tempSpeed, rigidbody2D.velocity.y);
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
		if (transform.position.y < -10) {
			Application.LoadLevel(Application.loadedLevel);
			grounded = false;
		
		}
	}
	
}
