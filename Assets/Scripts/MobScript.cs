using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {
	
	private float health = 20f;
	
	public float Health{
		get{return health;}
		set{health = value;}
	}


	// Use this for initialization
	void Start () {
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
