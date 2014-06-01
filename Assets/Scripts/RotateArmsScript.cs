using UnityEngine;
using System.Collections;

public class RotateArmsScript : MonoBehaviour {

	private Vector3 mousePosition, screenPos;
	public float maxAngle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));

		// Move along in the angle of the mouse position and flip the angles if the sprite is also flipped.
		// You can fix the maximum angle the sprite can turn with maxAngle. 
		if (transform.parent.transform.localScale.x < 1) {
			float newAngle = 180-Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg;
			if ((newAngle > maxAngle && newAngle < 210) ||  (210 < newAngle && newAngle < (360-maxAngle)) ) {			
				if (newAngle < 210) newAngle = maxAngle; 
				if (newAngle > 210 ) newAngle = 360 - maxAngle;
			}
			transform.eulerAngles = new Vector3 (0, 0, newAngle);
		} else {
			float newAngle = Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg;
			Debug.Log("The angle is: " + newAngle);
			if ((newAngle > maxAngle && newAngle < 210) ||  (-210 < newAngle && newAngle < maxAngle*-1) ) {			
				if (newAngle < 210 && newAngle > -maxAngle) newAngle = maxAngle; else newAngle = maxAngle*-1;
			}
			transform.eulerAngles = new Vector3 (0, 0, newAngle);
		}
	}
}
