using UnityEngine;
using System.Collections;

public class MovePlatformScript : MonoBehaviour {
	// This is a general script for moving platforms, Speed and Direction are public vars.
	private Vector3 startPosition;
	public float moveSpeed = .2f;
	public bool horizontal;
	private Vector2 moveForceUp;
	private Vector2 moveForceDown;
	private Vector2 moveForceLeft;
	private Vector2 moveForceRight;
	private bool goPos;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		moveForceUp = new Vector2(0f,moveSpeed);
		moveForceDown = new Vector2(0f,-moveSpeed);
		moveForceLeft = new Vector2(-moveSpeed,0f);
		moveForceRight = new Vector2(moveSpeed, 0f);
		goPos = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!horizontal) {
			if (transform.position.y > startPosition.y + .3)
					goPos = false;
			if (transform.position.y < startPosition.y - .3) 
					goPos = true;

			if (goPos) {
					transform.rigidbody2D.velocity = (moveForceUp);
					//transform.Translate (Vector3.up * Time.deltaTime, Space.World);
			} else {
					transform.rigidbody2D.velocity = (moveForceDown);
					//transform.Translate (Vector3.down * Time.deltaTime, Space.World);
			}
		} else {
			if (transform.position.x < startPosition.x - .3) {
				goPos = false;
			}
			
			if (transform.position.x > startPosition.x + .3) {
				goPos = true;	
			}
			if (goPos) {
				transform.rigidbody2D.velocity =(moveForceLeft);
				//transform.Translate (Vector3.left * Time.deltaTime, Space.World);
			} else {
				transform.rigidbody2D.velocity =(moveForceRight);
				//transform.Translate (Vector3.right * Time.deltaTime, Space.World);
			}
		}
	}
}
