using UnityEngine;
using System.Collections;

public class TileMapDestroyScript : MonoBehaviour {
	
	private tk2dTileMap tilemap;
	private int xPos;
	private int yPos;

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
			foreach ( ContactPoint2D contactpoint in projectileCollision.contacts){
				//Debug.Log(contactpoint.point);
				//Debug.Log(contactpoint.collider.gameObject.name);
				//tilemap.ClearTile((int)contactpoint.point.x+21.5,(int)contactpoint.point.y+ 11.5,3);
				//Vector3 
				Vector3 tempVec = new Vector3 (contactpoint.point.x+(.5f*contactpoint.normal.x),contactpoint.point.y,5);
				Debug.Log (contactpoint.normal);

				//Debug.Log (tilemap.GetTilePosition(2,3));
				//Debug.Log (tempVec);
				//Debug.Log(tempVec);
				//int tileId2 = tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),4);


				tilemap.GetTileAtPosition(tempVec,out xPos, out yPos);
				int tileId = tilemap.Layers[3].GetTile(xPos,yPos);
				Debug.Log(tileId);

				tilemap.Layers[3].ClearTile(xPos,yPos);
				tilemap.Build();

				//Debug.Log(tileId2);
				//tilemap.Layers[3].ClearTile(1,1);


//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),0));
//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),1));
//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),2));
//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),3));
//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),4));
//				Debug.Log (tilemap.GetTileIdAtPosition(tilemap.GetTilePosition(2,3),5));



			}
			projectileCollision = null;
			//tilemap.Build();
		}
	}
}
