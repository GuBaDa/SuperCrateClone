using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour {

	private Vector3 forceVector;

	public float aimDistance;
	public GameObject player;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
		Vector3 aimPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 newAimPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, aimPos.z);
		Vector3 finalAimPos = Camera.main.ScreenToWorldPoint (newAimPos);

		forceVector = playerPos - finalAimPos;
		if (forceVector.magnitude > aimDistance){
			forceVector = forceVector.normalized * aimDistance;
			transform.position = playerPos - forceVector;
		}
		else{
			transform.position = finalAimPos;
		}

	}
}
