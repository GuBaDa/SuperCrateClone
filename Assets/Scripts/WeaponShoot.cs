using UnityEngine;
using System.Collections;

public class WeaponShoot : MonoBehaviour {

	public GameObject projectilePrefab;
	public float attackSpeed;
	public Vector2 projectileForce;
	public Vector2 projectileFirePos;
	public bool automatic;
	private float coolDown;

	private bool fireBtn;
	private float axisVertical;


	void Update () {
		getControls ();
		if (fireBtn && Time.time > coolDown) {
			Fire();
		}
	}

	void Fire()
	{
		//Calculate the relative FirePos of the projectile, which is different for every "Shoot" type weapon.
		Vector3 projectileInitPos = new Vector3 (projectileFirePos.x*transform.parent.transform.localScale.x,projectileFirePos.y,0); 

		//Calculate the direction, namely the direction indicated by the arrow keys/analog stick.
		// Something something Quaternion

		// Instantiate the projectile.

		GameObject pPrefab = (GameObject) Instantiate (projectilePrefab, (transform.position+projectileInitPos), Quaternion.identity);

		// Flip the projectile if looking the other way.
		Vector3 tempScale = new Vector3 (-1,pPrefab.transform.localScale.y,pPrefab.transform.localScale.z);
		if(transform.parent.transform.localScale.x == -1)
		{
			pPrefab.transform.localScale = tempScale;
		} 

		// Add a certain force to the projectile, the amount of force is given in the public var projectileForce and determined 
		pPrefab.rigidbody2D.AddForce (new Vector2 ((projectileForce.x *(transform.parent.transform.localScale.x)),projectileForce.y*axisVertical));
		if (pPrefab.GetComponent<ProjectileBullet>() != null){
			pPrefab.GetComponent<ProjectileBullet>().owner = transform.parent.gameObject;
			Debug.Log (pPrefab.GetComponent<ProjectileBullet>().owner);
		}

		coolDown = Time.time + attackSpeed;
	}

	void getControls(){
		// Set control script to right player
		GetComponentInParent<PlayerController>().PlayerControlNr = GetComponentInParent<PlayerScript> ().PlayerControlNr;

		//get input
		if(automatic){
			fireBtn = GetComponentInParent<PlayerController>().Fire1Btn;
		} else {
			fireBtn = GetComponentInParent<PlayerController>().Fire1BtnDown;
		}
		axisVertical = GetComponentInParent<PlayerController>().AxisVertical;

		
	}
}
