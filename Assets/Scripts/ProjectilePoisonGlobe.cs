using UnityEngine;
using System.Collections;

public class ProjectilePoisonGlobe : MonoBehaviour {

	private GameObject target;


	// Use this for initialization
	void Start () {
	

		target = findClosestPlayer ();
		transform.parent = null;
		transform.localScale = new Vector3 (1,1,0);


	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = (target.transform.position-transform.position).normalized*5;
		rigidbody2D.velocity = direction;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Destroy(gameObject);
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
