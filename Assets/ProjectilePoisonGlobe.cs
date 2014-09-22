using UnityEngine;
using System.Collections;

public class ProjectilePoisonGlobe : MonoBehaviour {

	private GameObject target;

	// Use this for initialization
	void Start () {
		target = findClosestPlayer ();

		//Dit zorgt ervoor dat de particle system niet meer zichtbaar is. Ik snap niet waarom.
		//transform.parent = null;

		
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce (target.transform.position-transform.position);
		rigidbody2D.AddForce (new Vector2 (0f, 2f));
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
