using UnityEngine;
using System.Collections;

public class MovePlatforms : MonoBehaviour {
	// This is a general script for moving platforms, Speed and Direction are public vars.
	
	public float moveSpeed;
	public bool horizontal;
	public float tilesToMove;

	private float delay = 20;
	private Vector3 startPosition;
	private Vector2 moveForce;
	
	//private Vector2 moveForceDown;
	//private Vector2 moveForceLeft;
	//private Vector2 moveForceRight;
	//private int goPos = 1;
	
	// Use this for initialization
	void Start () {
		if (!horizontal) {
			moveForce = new Vector2 (0f , moveSpeed);
		}
		else {
			moveForce = new Vector2 (moveSpeed, 0f);
		}
		startPosition = transform.position;
		//moveForceUp = new Vector2(0f,moveSpeed);
		//moveForceDown = new Vector2(0f,-moveSpeed);
		//moveForceLeft = new Vector2(-moveSpeed,0f);
		//moveForceRight = new Vector2(moveSpeed, 0f);
		//goPos = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (delay > 20){
			if (!horizontal) {
				if (transform.position.y > startPosition.y + tilesToMove || 
				    transform.position.y < startPosition.y - tilesToMove){
					moveForce.y *= -1;
					delay = 0;
				}
			}
			else {
				if (transform.position.x > startPosition.x + tilesToMove || 
				    transform.position.x < startPosition.x - tilesToMove){
					moveForce.x *= -1;
					delay = 0;
				}
			}
		}
		transform.rigidbody2D.velocity = Time.deltaTime * moveForce;
		delay++;
		/*

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
	*/}
}
