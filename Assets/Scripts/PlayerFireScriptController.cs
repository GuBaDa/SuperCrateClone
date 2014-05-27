using UnityEngine;
using System.Collections;

public class PlayerFireScriptController: MonoBehaviour {

	public GameObject projectilePrefab;
	public float attackSpeed;
	private float coolDown;
	private AimScriptChild aimSight;

	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1") && Time.time > coolDown) {
			Fire();
		}
	}


	void Fire()
	{
		//Transform.position should be the position of the gun

		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint (pos);

		Quaternion q = Quaternion.FromToRotation(Vector3.up, pos-transform.position);
		GameObject pPrefab = (GameObject) Instantiate (projectilePrefab, transform.FindChild("AimSight").transform.position, q);
		Vector2 shootForce = new Vector2 (Input.GetAxisRaw ("Mouse X") * 10000, Input.GetAxisRaw ("Mouse Y") * 10000);
		pPrefab.rigidbody2D.AddForce (shootForce);

		coolDown = Time.time + attackSpeed;
	}
}
