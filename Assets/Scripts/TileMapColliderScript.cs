using UnityEngine;
using System.Collections;

public class TileMapColliderScript : MonoBehaviour {

	//[HideInInspector]

	TileMapDestroyScript tileMap;


	// Use this for initialization
	void Start () {
		tileMap = TileMapDestroyScript.FindObjectOfType<TileMapDestroyScript>();

		//set manually to right layer, because unity changes this back to default when playing game
		gameObject.layer = LayerMask.NameToLayer ("TileMapCollision");
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnCollisionEnter2D (Collision2D coll) {
		if(coll.collider.gameObject.tag == "Projectile"){
			tileMap.damage = coll.collider.GetComponent<ProjectileBullet>().damageOutput;
			tileMap.projectileCollision = coll;
		}
	}




}
