using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if anim.
	}
	
	public float destruct(float a){
		Destroy (gameObject);
		return a;
	}
}
