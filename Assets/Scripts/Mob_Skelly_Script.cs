using UnityEngine;
using System.Collections;

public class Mob_Skelly_Script : MonoBehaviour {

	// Public vars
	public float moveSpeed;

	//Private vars
	private bool grounded;
	private bool casting;



	// Use this for initialization
	void Awake () {
		casting = false;
	}

	void OnTriggerStay2D(){
		grounded = true;
	}

	void OnTriggerExit2D(){
		grounded = false;
	}

	// Update is called once per frame
	void Update () {
		if(grounded && !casting) {
			doWalk();
		} else if (!casting) {
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		}
	}

	void doWalk(){
		casting = false;
		rigidbody2D.velocity = new Vector2(moveSpeed,0);
	}

	void castSpell(){
		casting = true;
	}
}
