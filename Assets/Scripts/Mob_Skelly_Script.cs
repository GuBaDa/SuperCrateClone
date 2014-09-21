using UnityEngine;
using System.Collections;

public class Mob_Skelly_Script : MonoBehaviour {

	// Public vars
	public float moveSpeed;

	[HideInInspector]
	public bool goRight;

	//Private vars
	private bool grounded;
	private float castCooldown;



	// Use this for initialization
	void Awake () {
		castCooldown = 0;
		goRight = true;
	}

	void OnTriggerStay2D(){
		grounded = true;
	}

	void OnTriggerExit2D(){
		grounded = false;
	}

	// Update is called once per frame
	void Update () {

		if(grounded && castCooldown < Time.time) {
			doWalk();
		} else {
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		}
	}

	void doWalk(){
		rigidbody2D.velocity = new Vector2(moveSpeed*transform.localScale.x,0);
	}

	void castSpell(){
		castCooldown = Time.time + 2;
	}
}
