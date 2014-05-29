using UnityEngine;
using System.Collections;

public class Boar_AI : MonoBehaviour {

		Animator anim;
		
		public float force;
		public float time;
		private float orientation;
		
		// Use this for initialization
		void Start (){
			
			anim = GetComponent<Animator> ();
			time = 1;
			InvokeRepeating("action", 0, time);	


					}
		
		// Update is called once per frame
		void Update () {
			
		}

		void action (){
				int choose = Random.Range (1, 3);
				if (choose == 1) {
						walk ();
				} else {
						flip ();
				}
		}
		void OnCollisionEnter2D(){

		}
		
		void walk (){
			orientation = transform.localScale.x;
			Vector2 forceVector = new Vector2 (force * orientation,0);
			rigidbody2D.velocity = forceVector;
			anim.SetBool("animIdle", false);
			anim.SetBool("animWalk", true);
			
		}

		void flip(){
			orientation = transform.localScale.x;
			orientation *= -1;
			Vector3 oriVector = new Vector3 (orientation, transform.localScale.y, transform.localScale.z);
			transform.localScale = oriVector;
			anim.SetBool("animIdle", true);
			anim.SetBool("animWalk", false);
		}
		
		public void walkEnd(){
			anim.SetBool("animIdle", true);
			anim.SetBool("animWalk", false);
			
		}
		
	}