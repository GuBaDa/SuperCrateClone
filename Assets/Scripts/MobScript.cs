using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {

	// Public vars
	public float maxHealth;
	public ParticleSystem psOnDeath;
	public float moveSpeed;

	// Private vars
	private float health;
	private Vector3 tempScale;
	private float startTime;
	



	// Use this for initialization
	void Start () {
		health = maxHealth;
		tempScale = transform.localScale;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		OnDeath ();
		if( transform.localScale.x < 0 && ( rigidbody2D.velocity.x > 0) || transform.localScale.x > 0 && ( rigidbody2D.velocity.x  < 0)) 
		{	
			tempScale.x *= -1;
			transform.localScale = tempScale;
		}
		StartCoroutine(moveTowardsClosestPlayer());
	}

	void OnDeath(){
		if (Health == 0) {
			Destroy(gameObject);
			//Instantiate(psOnDeath,transform.position,Quaternion.identity);
		}
	}

	public float Health{
		get{return health;}
		set{health = Mathf.Clamp (value, 0f, maxHealth);}
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

	IEnumerator moveTowardsClosestPlayer(){
		GameObject target = findClosestPlayer ();

		rigidbody2D.AddForce (target.transform.position-transform.position);
		rigidbody2D.AddForce (new Vector2 (0f, 3f));
		
		yield return new WaitForSeconds (1f);

	}

}
