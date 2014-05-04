using UnityEngine;
using System.Collections;

public class RotateGunScript : MonoBehaviour {

	private Vector3 mousePosition, screenPos;
	 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));

		// We need to modify something here so the gun turns correctly 
		// Something with transform.localscale.
		if (transform.parent.transform.localScale.x < 1) {
			transform.eulerAngles = new Vector3 (-180, -180, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
		}
	}
}
