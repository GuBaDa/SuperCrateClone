using UnityEngine;
using System.Collections;

public class MovePlatformScriptH : MonoBehaviour {
	
	private Vector3 startPosition;
	private bool goLeft;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		goLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < startPosition.x - .3)
			goLeft = false;
		if (transform.position.x > startPosition.x + .3) 
			goLeft = true;

		if (goLeft) {
			transform.Translate (Vector3.left * Time.deltaTime, Space.World);
		} else {
			transform.Translate (Vector3.right * Time.deltaTime, Space.World);
		}




	}
}
