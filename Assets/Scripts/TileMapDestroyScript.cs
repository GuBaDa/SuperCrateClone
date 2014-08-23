using UnityEngine;
using System.Collections;

public class TileMapDestroyScript : MonoBehaviour {
	
	private tk2dTileMap tilemap;
	//private PlayerScript playerScript;
	//private TileMapColliderScript tileMapCollider;

	public Collision2D projectileCollision;
	
	// Use this for initialization
	void Start () {
		tilemap = GetComponent<tk2dTileMap>();
		//tileMapCollider = GetComponent<TileMapColliderScript>();
		//tileMapCollider = TileMapColliderScript.FindObjectOfType<TileMapColliderScript>
	}
	
	// Update is called once per frame
	void Update () {
		if(projectileCollision != null){
			Debug.Log("Projectile hit the wall");
			foreach ( ContactPoint2D contactpoint in projectileCollision.contacts){
				Debug.Log(contactpoint.point);
				tilemap.ClearTile((int)contactpoint.point.x,(int)contactpoint.point.y,3);
			}
			projectileCollision = null;
			tilemap.Build();
		}
	}
}
