using UnityEngine;
using System.Collections;

public class TileMapDestroyScript : MonoBehaviour {


	public int layerCollider;
	public int layerColliderDecals;
	public int layerColliderEdges;
	public int layerColliderCorners;

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
			ContactPoint2D contactpoint = projectileCollision.contacts[0];
			//Debug.Log(contactpoint.point);
			//Debug.Log(contactpoint.collider.gameObject.name);
			//tilemap.ClearTile((int)contactpoint.point.x+21.5,(int)contactpoint.point.y+ 11.5,3);
			//Vector3 
			Vector3 tempVec = new Vector3 (contactpoint.point.x+(.5f*contactpoint.normal.x),contactpoint.point.y,5);

			tilemap.GetTileAtPosition(tempVec,out xPos, out yPos);


			int tileId = tilemap.Layers[layerCollider].GetTile(xPos,yPos);

			if (tileId >= 0) {
				tilemap.Layers[layerCollider].ClearTile(xPos,yPos);
				tilemap.Layers[layerColliderDecals].ClearTile(xPos,yPos);
				tilemap.Layers[layerColliderCorners].ClearTile(xPos,yPos);
				tilemap.Layers[layerColliderEdges].ClearTile(xPos,yPos);
				StartCoroutine(setNWESTiles(xPos,yPos)); 

				tilemap.Build();
			}

		}
		projectileCollision = null;
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

		int north = tilemap.Layers[layerColliderEdges].GetTile(xPos,yPos+1);
		int east = tilemap.Layers[layerColliderEdges].GetTile(xPos+1,yPos);
		int west = tilemap.Layers[layerColliderEdges].GetTile(xPos-1,yPos);
		int south = tilemap.Layers[layerColliderEdges].GetTile(xPos,yPos-1);
		//Get Tile ID's on layer 3 to check if tile is present, so cleared tiles can be skipped
		int _N = tilemap.Layers[layerCollider].GetTile(xPos,yPos+1);
		int _E = tilemap.Layers[layerCollider].GetTile(xPos+1,yPos);
		int _W = tilemap.Layers[layerCollider].GetTile(xPos-1,yPos);
		int _S = tilemap.Layers[layerCollider].GetTile(xPos,yPos-1);


		//get diagonal Tile ID's 
		int NW = tilemap.Layers[layerColliderCorners].GetTile(xPos-1,yPos+1)-16;
		int NE = tilemap.Layers[layerColliderCorners].GetTile(xPos+1,yPos+1)-16;
		int SE = tilemap.Layers[layerColliderCorners].GetTile(xPos+1,yPos-1)-16;
		int SW = tilemap.Layers[layerColliderCorners].GetTile(xPos-1,yPos-1)-16;

		//Debug.Log ("NW: " + NW + "NE: " + NE + "SE: " + SE + "SW: " + SE);
		//Get Tile ID's on layer 3 to check if tile is present, so cleared tiles can be skipped
		int _NW = tilemap.Layers[layerCollider].GetTile(xPos-1,yPos+1);
		int _NE = tilemap.Layers[layerCollider].GetTile(xPos+1,yPos+1);
		int _SE = tilemap.Layers[layerCollider].GetTile(xPos+1,yPos-1);
		int _SW = tilemap.Layers[layerCollider].GetTile(xPos-1,yPos-1);


		if(_N >= 0 ){
			if (north <0){
				north = 0;
			}
			tilemap.Layers[layerColliderEdges].SetTile(xPos,yPos+1,convertAddBinary(north,8));
		}

		if (_E >= 0) {
			if (east <0){
				east = 0;
			}
			tilemap.Layers[layerColliderEdges].SetTile(xPos+1,yPos,convertAddBinary(east,4));
		}

		if(_W >= 0 ){
			if (west <0){
				west = 0;
			}
			tilemap.Layers[layerColliderEdges].SetTile(xPos-1,yPos,convertAddBinary(west,2));
		}

		if(_S >= 0 ) {
			if (south <0){
				south = 0;
			}
			tilemap.Layers[layerColliderEdges].SetTile(xPos,yPos-1,convertAddBinary(south,1));
		}


		//diagonals
		if (_NW >= 0 ){
			if (NW <0){
				NW = 0;
			}
			tilemap.Layers[layerColliderCorners].SetTile(xPos-1,yPos+1, (convertAddBinary (NW,8) + 16 ));
		}
		if (_NE >= 0 ){
			if (NE <0){
				NE = 0;
			}
			tilemap.Layers[layerColliderCorners].SetTile(xPos+1,yPos+1, (convertAddBinary (NE,4) + 16 ));
		}
		if (_SE >= 0 ){
			if (SE <0){
				SE = 0;
			}
			tilemap.Layers[layerColliderCorners].SetTile(xPos+1,yPos-1, (convertAddBinary (SE,1) + 16 ));
		}
		if (_SW >= 0 ){
			if (SW <0){
				SW = 0;
			}
			tilemap.Layers[layerColliderCorners].SetTile(xPos-1,yPos-1, (convertAddBinary (SW,2) + 16 ));
		}


		yield return null;

	}

	int convertAddBinary(int tileID, int edgeDirection){

		string s1 = System.Convert.ToString(tileID, 2); 
		char[] charArray1 = s1.PadLeft(4, '0').ToCharArray();

		string s2 = System.Convert.ToString(edgeDirection, 2); 
		char[] charArray2 = s2.PadLeft(4, '0').ToCharArray();

		for(int i = 0; i < charArray1.Length; i++ ){
			if (charArray1[i] == '1' ){
				charArray2[i] = '1';
			}
		}
		string s = new string(charArray2);
		int tileIDNew = System.Convert.ToInt32(s, 2);
		return tileIDNew;
	}
	
}
