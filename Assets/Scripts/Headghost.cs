using UnityEngine;
using System.Collections;

public class Headghost : MonoBehaviour {
	
	Animator anim;
	
	public float force;
	public float swimTimeFactor;
	
	private float time;
	
	
	// Use this for initialization
	void Start (){
		
		anim = GetComponent<Animator> ();
		
		time = Random.Range (swimTimeFactor, swimTimeFactor * 2);
		InvokeRepeating("swim", 0, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(){
		float dir = 1;
		if (rigidbody2D.velocity.y > 0){
			dir = -1;
		}
		Vector2 forceVector = new Vector2 (Random.Range(force/-3, dir * force/3), force);
		rigidbody2D.AddForce (forceVector);
		anim.SetBool("animIdle", false);
		anim.SetBool("animSwim", true);
	}
	
	void swim (){
		Vector2 forceVector = new Vector2 (Random.Range(force/-3, force/3), force);
		rigidbody2D.AddForce (forceVector);
		time = Random.Range (swimTimeFactor, swimTimeFactor * 2);
		anim.SetBool("animIdle", false);
		anim.SetBool("animSwim", true);
		
	}
	
	public void swimEnd(){
		anim.SetBool("animIdle", true);
		anim.SetBool("animSwim", false);
		
	}
	
}
