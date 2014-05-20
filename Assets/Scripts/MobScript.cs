using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {
	
	private float health;
	
	public float Health{
		get{return health;}
		set{health = value;}
	}


	// Use this for initialization
	void Start () {
		health = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		OnDeath ();
	}

	void OnDeath(){
		if (Health == 0) {
			Destroy(gameObject);
		}
	}
}
