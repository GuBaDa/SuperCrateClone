using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] // Hides var below from inspector
	public bool grounded;
	public bool dead;

	private bool doubleJump;
	private bool wallLeft, wallRight;
	private Transform activePlatform;
	private Vector3 tempScale;
	private float doubleJumpHeight;
	public bool goLeft;
	public bool goRight;
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


	/// Start this instance.
	/// 
	void Awake () {
		health = 100f;
		grounded = false;
		doubleJump = true;
		wallLeft = false;
		wallRight = false;
		goRight = true;
		doubleJumpHeight = jumpHeight * .75f;
		tempScale = transform.localScale;

		plControllerScript = GetComponent<PlayerController>(); 
	}

	
	// FixedUpdate is called on fixed Times, use this for physics movements.
	void FixedUpdate () {
		doMove ();
		resetGame ();
	}

	void OnTriggerStay2D(Collider2D coll){
		if(!(coll.transform.IsChildOf(transform))){
			grounded = true;
			doubleJump = true;
		}
	}

	void OnTriggerExit2D(){
		grounded = false;
	}

	// Update is called on each frame, this way the character immediately reacts to jumps.
	void Update(){
		getControls();

		if(rigidbody2D.velocity.y < 0){
			Physics2D.IgnoreLayerCollision(2,11,false);
		}

		OnDeath ();
		doDoubleJump();
		if(grounded)doJump ();
	}




	/// Move, Jump and DoubleJump //////
	/// 
	/// 
	void doMove(){
		if (axisHorizontal != 0) {
			// Get speed in correct direction
			Vector2 tempSpeed =  new Vector2 (axisHorizontal * maxSpeed, rigidbody2D.velocity.y);

			// Move player horizontally only if it is not blocked by a wall 


			if( transform.localScale.x < 0 && ( axisHorizontal > 0) || transform.localScale.x > 0 && ( axisHorizontal  < 0)) 
	   		{	
	   			tempScale.x *= -1;
	   			transform.localScale = tempScale;
	   		}

			if(tempSpeed.x < 0 && goRight){
				rigidbody2D.velocity = tempSpeed;
			}
			if(tempSpeed.x > 0 && goRight){
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
			dustCast ();
		}
	}


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





