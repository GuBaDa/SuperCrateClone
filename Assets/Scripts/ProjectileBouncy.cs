using UnityEngine;
using System.Collections;

public class ProjectileBouncy: MonoBehaviour {

	private int bounceCount;
	public int bounceCountAmount;

	// Use this for initialization
	void Start () {
		bounceCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// amount of bounces before being destroyed
				if (bounceCount == bounceCountAmount) {
						Destroy (gameObject);
				}
	}

		void OnCollisionEnter2D(Collision2D coll){
		bounceCount += 1;
		// For players
		GameObject enemy = coll.gameObject;
		if (enemy.tag == "Player") {
			PlayerScript playerScript = enemy.GetComponent<PlayerScript>();
			playerScript.Health -= 10.0f;
			Destroy (gameObject);
		} else if (enemy.tag == "Mob") {
			//MobScript mobScript = enemy.GetComponent<MobScript>();
			//mobScript.Health -= 10.0f;
		}
	}
}
