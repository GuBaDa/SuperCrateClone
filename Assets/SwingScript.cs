using UnityEngine;
using System.Collections;

public class SwingScript : MonoBehaviour {

	public float attackSpeed;
	private float coolDown;
	private bool Slashing;
	private bool Recovering;
	private float totalRotation;
	private float rotationAmt;
	public float rotateSpeed;


	// Use this for initialization
	void Start () {
		transform.Rotate (0, 0, 75);
		Vector3 newLocalPos = new Vector3 (.4f, .1f, 0f);
		transform.localPosition = newLocalPos;

		totalRotation = 0; 
	}
	
	// Update is called once per frame
	void Update () {

		
		if (Input.GetButton("Fire1") && !Slashing) {
			Slashing = true;
		}

		if (Slashing) {
			Slash ();
		} else if (Recovering) {
			Recover();	
		}
	}

	void Slash(){
		rotationAmt = rotateSpeed * Time.deltaTime + (.2f * totalRotation);
		transform.Rotate(0, 0, -rotationAmt);
		totalRotation += rotationAmt; 	
		if (totalRotation >80) {
			Slashing = false;
			Recovering = true;
			totalRotation = 0;
		}
	}
	
	void Recover(){
		rotationAmt = rotateSpeed * Time.deltaTime + (.1f * totalRotation);
		transform.Rotate(0, 0, rotationAmt);
		totalRotation += rotationAmt; 	
		if (totalRotation > 80) {

			Recovering = false;
			totalRotation = 0;
		}
	}


}
