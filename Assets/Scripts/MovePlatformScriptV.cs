using UnityEngine;
using System.Collections;

public class MovePlatformScriptV : MonoBehaviour {
	
	private Vector3 startPosition;
	private bool goUp;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		goUp = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > startPosition.y + .3)
			goUp = false;
		if (transform.position.y < startPosition.y - .3) 
			goUp = true;

		if (goUp) {
			transform.Translate (Vector3.up * Time.deltaTime, Space.World);
		} else {
			transform.Translate (Vector3.down * Time.deltaTime, Space.World);
		}




	}
}
