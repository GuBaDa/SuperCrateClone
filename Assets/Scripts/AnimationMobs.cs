using UnityEngine;
using System.Collections;

public class AnimationMobs : MonoBehaviour {

	Animator anim;

	public float animSpeed;

	private float _speed;
	private float _agroSpeed;

	private Vector3 tempScale;
	//private float jumpVector = 0.5f; //factor needed in relation to vertical moving platforms

	void Awake (){
		anim = GetComponent<Animator> ();
		_speed = GetComponent<Boar_AI2> ().speed;
		_agroSpeed = GetComponent<Boar_AI2> ().agroSpeed;
		tempScale = new Vector3 (1, 1, 1);
	}
	// Update is called once per frame
	void Update () {

		float boarVelocityX = rigidbody2D.velocity.x;
		//direction
		if (boarVelocityX < 0){
			tempScale.x = 1;
		}
		if (boarVelocityX > 0){
			tempScale.x = -1;
		}
		transform.localScale = tempScale;

		//animations
		if (Mathf.Abs (boarVelocityX) == 0){
			//Idle
			anim.SetBool("animWalk", false);
			anim.SetBool("animIdle", true);
			anim.SetBool("animAttack", false);
		}
		else{
			if (Mathf.Abs (boarVelocityX) >= _agroSpeed){
				//Walk
				anim.SetBool("animWalk", false);
				anim.SetBool("animIdle", false);
				anim.SetBool("animAttack", true);
			}

			else {
				//Idle
				anim.SetBool("animWalk", true);
				anim.SetBool("animIdle", false);
				anim.SetBool("animAttack", false);
			}
		}
	}

	void FixedUpdate (){

		anim.speed = Mathf.Abs (rigidbody2D.velocity.x) * animSpeed / 10;

	}
}
