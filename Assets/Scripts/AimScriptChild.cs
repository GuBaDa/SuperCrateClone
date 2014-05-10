using UnityEngine;
using System.Collections;

public class AimScriptChild : MonoBehaviour {

	private Vector3 forceVector;

	public float aimDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 player = new Vector3 (transform.parent.position.x, transform.parent.position.y, transform.position.z);
		Vector3 aimPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 newAimPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, aimPos.z);
		Vector3 finalAimPos = Camera.main.ScreenToWorldPoint (newAimPos);

		forceVector = player - finalAimPos;
		if (forceVector.magnitude > aimDistance){
			forceVector = forceVector.normalized * aimDistance;
			transform.position = player - forceVector;

		}
		else{
			transform.position = finalAimPos;
		}

	}
}
