using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 vect = new Vector2 (Input.GetAxisRaw("Horizontal") * force,Input.GetAxisRaw("Vertical") * force);
		if (vect.x != 0 || vect.y !=0){
			rigidbody2D.velocity = vect;

     	}
	}
}
