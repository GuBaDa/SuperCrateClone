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
	// Use this for initialization
	void Start () {
		if (!horizontal) {
			moveForce = new Vector2 (0f , moveSpeed);
		}
		else {
			moveForce = new Vector2 (moveSpeed, 0f);
		}
		startPosition = transform.position;
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
	}
}