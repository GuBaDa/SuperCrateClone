using UnityEngine;
using System.Collections;

public class Mob_Skelly_Script : MonoBehaviour {

	// Public vars
	public float moveSpeed;
	public GameObject psPoisonGlobe;


	Animator anim;


	//Private vars
	private bool grounded;
	private float castCooldown;



	// Use this for initialization
	void Awake () {
		castCooldown = 0;
		anim = GetComponent<Animator> ();
	}

	void OnTriggerStay2D(){
		grounded = true;
	}

	void OnTriggerExit2D(){
		grounded = false;
	}

	// Update is called once per frame
	void Update () {
		if(checkForAgro() && transform.childCount == 1){
			castCooldown = Time.time + .8f;
			anim.SetBool ("animWalk", false);
			anim.SetBool ("animIdle", false);
			anim.SetBool ("animCast", true);
		} else if(grounded && castCooldown < Time.time){
			doWalk();
			anim.SetBool ("animWalk", true);
			anim.SetBool ("animIdle", false);
			anim.SetBool ("animCast", false);
		} else {
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
			anim.SetBool ("animWalk", false);
			anim.SetBool ("animIdle", true);
			anim.SetBool ("animCast", false);
		}
	}

	void doWalk(){
		rigidbody2D.velocity = new Vector2(moveSpeed*transform.localScale.x,0);
	}

	void castSpell(){
		if (transform.childCount == 1){
			GameObject projectile = (GameObject) Instantiate(psPoisonGlobe,new Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
			projectile.transform.parent = transform;
		}
	}

	private bool checkForAgro (){
		//Debug.Log ("CHECK foragro");
		GameObject[] playersAvailable = GameObject.FindGameObjectsWithTag ("Player");
		if (playersAvailable.Length != 0){
			GameObject target = findClosestTarget ();
			if (Mathf.Abs(target.transform.position.x - transform.position.x) < 3 &&
			    Mathf.Abs(target.transform.position.y - transform.position.y) < 3)
			{
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}

	GameObject findClosestTarget(){
		//Find and return closest Player
		
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("Player");
		GameObject closestTarget = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject tar in targets) {
			Vector3 diff = tar.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closestTarget = tar;
				distance = curDistance;
			}
		}
		return closestTarget;
	}


}
