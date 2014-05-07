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
			//check direction
			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float direction = (worldMousePos.x - transform.position.x) * rigidbody2D.velocity.x;

			if (direction > 0){
			anim.SetBool("animWalk", true);
			anim.SetBool("animWalkBackwards", false);
			}
			else {
			anim.SetBool("animWalkBackwards", true);
				anim.SetBool("animWalk", false);
			}
			anim.SetBool("animIdle", false);
		}
		else{
			anim.SetBool("animIdle", true);
			anim.SetBool("animWalk", false);
			anim.SetBool("animWalkBackwards", false);
		}
		if (rigidbody2D.velocity.y > jumpVector) {
			anim.SetBool("animWalk", false);
			anim.SetBool("animWalkBackwards", false);
		}
	}

	void FixedUpdate (){

		anim.speed = Mathf.Abs (rigidbody2D.velocity.x) * animSpeed / 10;
	}
}
