using UnityEngine;
using System.Collections;

public class ProjectileBlast : MonoBehaviour {

	public float flashTime;
	
	// Update is called once per frame
	void Awake () {
		Invoke("DestroySelf", flashTime);
	}


	void OnCollisionEnter2D(Collision2D coll){
		// For players
		GameObject hit = coll.gameObject;
		if (hit.tag == "Player") {
			PlayerScript playerScript = hit.GetComponent<PlayerScript>();
			playerScript.Health -= 10.0f;
			Destroy (gameObject);
		} else if (hit.tag == "Mob") {
			MobScript mobScript = hit.GetComponent<MobScript>();
			mobScript.Health -= 10.0f;
		}
	}

	void DestroySelf (){
		Destroy (gameObject);
	}
}