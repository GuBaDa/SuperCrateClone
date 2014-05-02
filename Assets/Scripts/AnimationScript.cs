using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	Animator anim;

	public float animSpeed;

	private float jumpVector = 0.5f; //factor needed in relation to vertical moving platforms

	void Start (){
		anim = GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Horizontal") != 0 && rigidbody2D.velocity.y < jumpVector){
			anim.SetBool("animWalk", true);
			anim.SetBool("animIdle", false);
		}
		else{
			anim.SetBool("animIdle", true);
			anim.SetBool("animWalk", false);
		}
		if (rigidbody2D.velocity.y > jumpVector) {
			anim.SetBool("animWalk", false);
		}
	}

	void FixedUpdate (){

		anim.speed = Mathf.Abs (rigidbody2D.velocity.x) * animSpeed / 10;
	}
}
