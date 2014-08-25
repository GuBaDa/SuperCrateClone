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
				StartCoroutine (setNWESTiles(xPos,yPos));

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

	
	/// <summary>
	/// receives coordinates from destroyed tile,
	/// Checks tileIds from n,w,e,s directions and adds the appropriate binary string to them.
	/// Sets the four n,w,e,s tiles to their new tileIds.
	/// </summary>
	/// <param name="str1">Str1.</param>
	/// <param name="str2">Str2.</param>
	/// 
	IEnumerator setNWESTiles(int xPos, int yPos){
		
		// First remember the positions of the tiles N,W,E,S.

		int north = tilemap.Layers[3].GetTile(xPos,yPos+1);
		int west = tilemap.Layers[3].GetTile(xPos-1,yPos);
		int east = tilemap.Layers[3].GetTile(xPos+1,yPos);
		int south = tilemap.Layers[3].GetTile(xPos,yPos-1);


		if(north >= 0 ){
			tilemap.Layers[6].SetTile(xPos,yPos+1,convertAddBinary(north,1));
		}

		if (west >= 0 ) {
			tilemap.Layers[6].SetTile(xPos-1,yPos,convertAddBinary(west,2));
		}

		if (east >= 0 ) {
			tilemap.Layers[6].SetTile(xPos+1,yPos,convertAddBinary(east,4));
		}
		if (south >= 0 ) {
			tilemap.Layers[6].SetTile(xPos,yPos-1,convertAddBinary(south,8));
		}

		yield return null;

	}

	int convertAddBinary(int tileID, int edgeDirection){

		string s1 = System.Convert.ToString(tileID, 2); 
		char[] charArray1 = s1.PadLeft(4, '0').ToCharArray();

		string s2 = System.Convert.ToString(edgeDirection, 2); 
		char[] charArray2 = s2.PadLeft(4, '0').ToCharArray();

		for(int i = 0; i < charArray1.Length; i++ ){
			if (charArray2[i] == '1' ){
				charArray1[i] = '1';
			}
		}
		string s = new string(charArray2);
		int tileIDNew = System.Convert.ToInt32(s, 2);
		return tileIDNew;
	}
	
}
