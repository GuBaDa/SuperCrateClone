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
		//Instatiate the projectile at the position of the weapon.
		GameObject pPrefab = (GameObject) Instantiate (projectilePrefab, transform.position, Quaternion.identity);

		// Flip the projectile if looking the other way.
		Vector3 tempScale = new Vector3 (-1,pPrefab.transform.localScale.y,pPrefab.transform.localScale.z);
		if(transform.parent.transform.localScale.x == 1)
		{
			pPrefab.transform.localScale = tempScale;
		} 

		// Add a certain force to the projectile, the amount of force is given in the public var projectileForce
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
