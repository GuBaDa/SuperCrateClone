using UnityEngine;
using System.Collections;

public class ProjectileBullet : MonoBehaviour {

	public ParticleSystem psPlayerHit;
	public ParticleSystem psBulletSparks;
	public float damageOutput;


	public GameObject owner;

	private Quaternion q;
	private tk2dTileMap tilemap;
	
	// Update is called once per frame
	
	void Start(){
		//tilemap = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
	}



	// Update is called once per frame

//	void LateUpdate(){
//		float xPos = transform.position.x + 21.5f;
//		float yPos = transform.position.y + 12;
//		if (tilemap.GetTile ((int)xPos, (int)yPos, 3) != -1){
//			Destroy (gameObject);
//		}
//	}

	void OnCollisionEnter2D(Collision2D coll){
		if(owner.transform.localScale.x == 1)
		{
			q = psBulletSparks.transform.rotation;
		} else {
			q = psBulletSparks.transform.rotation * Quaternion.Euler(0,180,0);
		}
		Instantiate(psBulletSparks, transform.position, q);
		Destroy (gameObject);
		// For players
		    GameObject enemy = coll.gameObject;
		if (enemy.tag == "Player") {
			PlayerScript playerScript = enemy.GetComponent<PlayerScript>();
			playerScript.Health -= damageOutput;
			psPlayerHit.maxParticles = (int) damageOutput;
			Instantiate(psPlayerHit, transform.position, Quaternion.identity);
		} else if (enemy.tag == "Mob") {
			MobScript mobScript = enemy.GetComponent<MobScript>();
			mobScript.Health -= damageOutput;
		}
	}
}
