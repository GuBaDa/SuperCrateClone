using UnityEngine;
using System.Collections;

public class Mob_Skelly_Ghost_Script : MonoBehaviour {

	public float moveSpeed;

	private float timeSinceFloat;
	private float floatCoolDown;
	private float castCooldown;

	Animator anim;
	// Use this for initialization
	void Awake () {
		castCooldown = 0;
		floatCoolDown = .2f;
		timeSinceFloat =0;

	}
	
	// Update is called once per frame
	void Update () {
				timeSinceFloat += Time.deltaTime;
		if (checkForAgro ()) {
						anim.SetBool ("animWalk", false);
						anim.SetBool ("animBack", false);
						anim.SetBool ("animCast", true);
				} else if (timeSinceFloat > floatCoolDown) {
						timeSinceFloat = 0;
						doFloat ();
						anim.SetBool ("animWalk", true);
						anim.SetBool ("animBack", false);
						anim.SetBool ("animCast", false);
					
						}
				}
		

	void doFloat(){
	rigidbody2D.AddForce(new Vector2(moveSpeed*transform.localScale.x,1f),ForceMode2D.Impulse);

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
