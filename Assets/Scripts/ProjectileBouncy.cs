using UnityEngine;
using System.Collections;

public class ProjectileBouncy: MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
				if (transform.rigidbody2D.velocity.x == 0) {
						Destroy (gameObject);
				}
	}

	void OnCollisionEnter2D(Collision2D coll){
		// For players
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
