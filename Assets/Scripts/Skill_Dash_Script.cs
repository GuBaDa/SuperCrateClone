using UnityEngine;
using System.Collections;

public class Skill_Dash_Script : MonoBehaviour {

	private Vector2 boostSpeedRight = new Vector2(30,0);
	private Vector2 boostSpeedLeft = new Vector2(-30,0);

	private bool canBoost = true;
	private float boostCooldown = 2f;
	
	void Update(){
		if ((canBoost) && Input.GetButtonDown ("Fire1")) {
			StartCoroutine( Boost(.1f) ); //Start the Coroutine called "Boost", and feed it the time we want it to boost us
		}
	}
	
	IEnumerator Boost(float boostDur) { //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
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
		yield return new WaitForSeconds(boostCooldown); //Cooldown time for being able to boost again, if you'd like.
		canBoost = true; //set back to true so that we can boost again.

	}
}
