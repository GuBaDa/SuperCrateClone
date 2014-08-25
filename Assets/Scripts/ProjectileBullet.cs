using UnityEngine;
using System.Collections;

public class ProjectileBullet : MonoBehaviour {

	public ParticleSystem psPlayerHit;
	public float damageOutput;

	public GameObject owner;

	private tk2dTileMap tilemap;
	
	// Update is called once per frame
	
	void Awake(){
		tilemap = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
	}
	
	// Update is called once per frame

	void LateUpdate(){
		float xPos = transform.position.x + 21.5f;
		float yPos = transform.position.y + 12;
		if (tilemap.GetTile ((int)xPos, (int)yPos, 3) != -1){
			Destroy (gameObject);
			Debug.Log ("Destroyed inside colliders");
		}
	}

	void OnCollisionEnter2D(Collision2D coll){

		Debug.DrawLine(coll.contacts[0].point, coll.contacts[1].point);
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
