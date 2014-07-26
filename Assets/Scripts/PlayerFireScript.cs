using UnityEngine;
using System.Collections;

public class PlayerFireScript : MonoBehaviour {

	public GameObject projectilePrefab;
	public float attackSpeed;
	public Vector2 projectileForce;
	private float coolDown;
	private AimScriptChild aimSight;

	private bool fire1Btn;
	private bool fire1BtnDown;

	// Update is called once per frame
	void Update () {
		getControls ();
		if (fire1BtnDown && Time.time > coolDown) {
			Fire();
		}
	}


	void Fire()
	{
		//Transform.position should be the position of the gun

		//Vector3 pos = Input.mousePosition;
		//pos.z = transform.position.z - Camera.main.transform.position.z;
		//pos = Camera.main.ScreenToWorldPoint (pos);
		// 

		//Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.position);
		GameObject pPrefab = (GameObject) Instantiate (projectilePrefab, transform.position, Quaternion.identity);
		if(transform.parent.transform.localScale.x == 1){
			pPrefab.transform.Rotate(0,180,0);
		}
		pPrefab.rigidbody2D.AddForce (new Vector2 ((projectileForce.x *(transform.parent.transform.localScale.x)),projectileForce.y));

		coolDown = Time.time + attackSpeed;
	}

	void getControls(){
		// Set control script to right player
		GetComponentInParent<PlayerController>().PlayerControlNr = GetComponentInParent<PlayerScript> ().PlayerControlNr;

		//get input
		fire1Btn = GetComponentInParent<PlayerController>().Fire1Btn;
		fire1BtnDown = GetComponentInParent<PlayerController>().Fire1BtnDown;

		
	}
}
