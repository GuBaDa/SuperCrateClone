using UnityEngine;
using System.Collections;

public class Mob_Skelly_Mage_AnimationScript : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(checkForAgro()){
			anim.SetBool ("animWalk", false);
			anim.SetBool ("animIdle", false);
			anim.SetBool ("animCast", true);
		} else if(rigidbody2D.velocity.x != 0){
			anim.SetBool ("animWalk", true);
			anim.SetBool ("animIdle", false);
			anim.SetBool ("animCast", false);
		} else {
			anim.SetBool ("animWalk", false);
			anim.SetBool ("animIdle", true);
			anim.SetBool ("animCast", false);
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
