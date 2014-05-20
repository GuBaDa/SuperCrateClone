using UnityEngine;
using System.Collections;

public class ProjectileFireball : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		Destroy (gameObject);
		string name = coll.gameObject.name;
		GameObject thePlayer = GameObject.Find(name);
		PlayerScript playerScript = thePlayer.GetComponent<PlayerScript>();
		playerScript.Health -= 10.0f;

	}
}
