using UnityEngine;
using System.Collections;

public class AimScriptChildController: MonoBehaviour {

	private Vector3 forceVector;
	private float stickSensitivity;

	public float aimDistance;

	// Use this for initialization
	void Start () {
		stickSensitivity = 20f;
	}
	
	// Update is called once per frame
	void Update () {

		//Vector3 player = new Vector3 (transform.parent.position.x, transform.parent.position.y, transform.position.z);
		//Vector3 aimPos = Camera.main.WorldToScreenPoint (transform.position);
		//Vector3 newAimPos = new Vector3 (Input.GetAxisRaw("Mouse X")* stickSensitivity, Input.GetAxisRaw("Mouse Y")* stickSensitivity, aimPos.z);
		//Vector3 finalAimPos = Camera.main.ScreenToWorldPoint (newAimPos);
		/*
		forceVector = player - finalAimPos;
		if (forceVector.magnitude > aimDistance){
			forceVector = forceVector.normalized * aimDistance;
			transform.position = player - forceVector;

		}
		else{
			transform.position = finalAimPos;
		}
		*/

		float xPos = (Input.GetAxisRaw ("Mouse X") * stickSensitivity) + transform.parent.transform.position.x;
		float yPos = (Input.GetAxisRaw ("Mouse Y") * stickSensitivity) + transform.parent.transform.position.y;
		transform.position = new Vector3 (xPos,yPos, 0);

	}
}
