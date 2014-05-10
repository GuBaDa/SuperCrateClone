using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	Animator anim;


	private bool grounded;
	private bool jumped;

	public float animSpeed;

	//private float jumpVector = 0.5f; //factor needed in relation to vertical moving platforms

	void Start (){
		anim = GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		//get boolean grounded from PlayerScript to check if grounded
		grounded = GetComponent<PlayerScript> ().grounded;

		if (grounded) {
			if (Input.GetAxisRaw ("Horizontal") != 0){
			//check direction
				Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				float direction = (worldMousePos.x - transform.position.x) * rigidbody2D.velocity.x;

				if (direction > 0){
					//walk forward
					anim.SetBool("animWalk", true);
					anim.SetBool("animWalkBackwards", false);
				}
				else {
					//walk backwards
					anim.SetBool("animWalkBackwards", true);
					anim.SetBool("animWalk", false);
				}
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
}
