using UnityEngine;
using System.Collections;

public class ProjectilePoisonGlobe : MonoBehaviour {

	private GameObject target;
	private bool targetFound;

	// Use this for initialization
	void Start () {
		targetFound = false;
		transform.localScale = new Vector3 (1,1,1);
	}
	
	// Update is called once per frame
	void Update () {
		if(targetFound) {
			Vector2 direction = (target.transform.position-transform.position).normalized*5;
			rigidbody2D.velocity = direction;
		} else {
			checkForAgro();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}

	void checkForAgro (){
		GameObject[] playersAvailable = GameObject.FindGameObjectsWithTag ("Player");
		if (playersAvailable.Length != 0){
			target = findClosestPlayer();
			if (Mathf.Abs(target.transform.position.x - transform.position.x) < 5 &&
			    Mathf.Abs(target.transform.position.y - transform.position.y) < 5)
			{
				transform.parent = null;
				//targetFound = true;
				//rigidbody2D.isKinematic = false;
			}
		}
	}


	GameObject findClosestPlayer(){
		//Find and return closest Player
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("Player");
		GameObject closestTarget = null;
		float distance = Mathf.Infinity;
		foreach (GameObject tar in targets) {
			Vector3 diff = tar.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closestTarget = tar;
				distance = curDistance;
			}
		}
		return closestTarget;
	}


}
