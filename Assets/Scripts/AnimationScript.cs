using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	Animator anim;


	private bool grounded;
	private bool jumped;

	public float animSpeed;

	private float axisHorizontal;
	private float axisVertical;
	private bool axisHorizontalDown;
	private bool axisVerticalDown;
	private bool fire1Btn;
	private bool fire2Btn;
	private bool fire3Btn;
	private bool jumpBtnDown;

	//private float jumpVector = 0.5f; //factor needed in relation to vertical moving platforms

	void Awake (){
		anim = GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		getControls();
		//get boolean grounded from PlayerScript to check if grounded
		grounded = GetComponent<PlayerScript> ().grounded;

		if (grounded) {
			if (axisHorizontal!= 0){
			//check direction
				Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				float direction = (worldMousePos.x - transform.position.x) * rigidbody2D.velocity.x;
				anim.SetBool("animWalk", true);
				anim.SetBool("animIdle", false);
				anim.SetBool("animJump", false);
			}
			else {
				//Idle
				anim.SetBool("animIdle", true);
				anim.SetBool("animWalk", false);
				anim.SetBool("animWalkBackwards", false);
				anim.SetBool("animJump", false);
			}
		}
		else {
			//Jump
			anim.SetBool("animJump", true);
			anim.SetBool("animWalk", false);
			anim.SetBool("animWalkBackwards", false);
			anim.SetBool("animIdle", false);
		}
	}

	void FixedUpdate (){

		if (rigidbody2D.velocity.x == 0 && grounded) {
			anim.speed = animSpeed;
		}
		else {
			anim.speed = Mathf.Abs (rigidbody2D.velocity.x) * animSpeed / 10;
		}
	}

	void getControls(){
		// Set control script to right player
		GetComponent<PlayerController>().PlayerControlNr = GetComponent<PlayerScript>().PlayerControlNr;
		// Get variables
		axisHorizontal = GetComponent<PlayerController>().AxisHorizontal;
		axisVertical = GetComponent<PlayerController>().AxisVertical;
		axisHorizontalDown = GetComponent<PlayerController>().AxisHorizontalDown;
		axisVerticalDown = GetComponent<PlayerController>().AxisVerticalDown;
		fire1Btn = GetComponent<PlayerController>().Fire1Btn;
		fire2Btn = GetComponent<PlayerController>().Fire2Btn;
		fire3Btn = GetComponent<PlayerController>().Fire3Btn;
		jumpBtnDown = GetComponent<PlayerController>().JumpBtnDown;
	}
}
