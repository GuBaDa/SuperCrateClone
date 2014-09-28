using UnityEngine;
using System.Collections;

public class WeaponShoot : MonoBehaviour {

	public GameObject projectilePrefab;
	public float attackSpeed;
	public Vector2 projectileForce;
	public Vector2 projectileFirePos;
	public bool automatic;
	public bool arcWeapon;
	public bool randomizeHeight;
	public int maxRandomHeight;

	private float coolDown;
	private bool playerDied;
	private bool addedComponents = false;
	private bool fireBtn;
	private float axisVertical;


	void Update () {
		playerDied = GetComponentInParent<PlayerScript> ().dead;

		if (!playerDied){
			getControls ();
			if (fireBtn && Time.time > coolDown) {
				Fire();
			}
		} else if (!addedComponents ) {
			//CircleCollider2D gameObjectsCircleCollider =  gameObject.AddComponent<CircleCollider2D>();
			//gameObjectsCircleCollider.radius = .32f;
			//gameObject.AddComponent<PolygonCollider2D>();
			foreach(CircleCollider2D cc in GetComponentsInParent<CircleCollider2D>()) cc.enabled = true;			
			Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
			gameObjectsRigidBody.mass = 2; // Set the GO's mass to .5 via the Rigidbody.
			rigidbody2D.AddForce(new Vector2((float) Random.Range(-2000f,2000f),(float) Random.Range(-2000f,2000f)));
			gameObject.transform.parent = null;
			addedComponents = true;
		}
	}

	void Fire()
	{

		// Add a certain force to the projectile, the amount of force is given in the public var projectileForce and determined 

		coolDown = Time.time + attackSpeed;

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
		if (randomizeHeight) {
			projectileForce = new Vector2 (projectileForce.x, (float) Random.Range (-maxRandomHeight, maxRandomHeight));
			pPrefab.rigidbody2D.AddForce (new Vector2 ((projectileForce.x *(transform.parent.transform.localScale.x)),projectileForce.y));
		} else if (arcWeapon) {
			pPrefab.rigidbody2D.AddForce (new Vector2 ((projectileForce.x *(transform.parent.transform.localScale.x)),(projectileForce.y*axisVertical)+400));

		} else {
			pPrefab.rigidbody2D.AddForce (new Vector2 ((projectileForce.x *(transform.parent.transform.localScale.x)),projectileForce.y*axisVertical));
		}

		if (pPrefab.GetComponent<ProjectileBullet>() != null){
			pPrefab.GetComponent<ProjectileBullet>().owner = transform.parent.gameObject;
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
