using UnityEngine;
using System.Collections;

public class ProjectileJelly : MonoBehaviour {

	private bool hitGround;

	// Use this for initialization
	void Start () {
		hitGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.rigidbody2D.velocity.x) < .5f && hitGround) {
			Destroy(gameObject);		
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		// For players
		hitGround = true;
		GameObject enemy = coll.gameObject;
		if (enemy.tag == "Player") {
			PlayerScript playerScript = enemy.GetComponent<PlayerScript>();
			playerScript.Health -= 10.0f;
		} else if (enemy.tag == "Mob") {
			MobScript mobScript = enemy.GetComponent<MobScript>();
			mobScript.Health -= 10.0f;
		}
	}
}
