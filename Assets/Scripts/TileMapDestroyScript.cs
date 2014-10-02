using UnityEngine;
using System.Collections;

public class TileMapDestroyScript : MonoBehaviour {

	public bool proceduralBuildOnStart;

	public int layerBackground2;
	public int layerBackground2Corners;
	public int layerBackground2Edges;
	public int layerCollider;
	public int layerColliderDecals;
	public int layerColliderCorners;
	public int layerColliderEdges;


	private tk2dTileMap tilemap;

	//private PlayerScript playerScript;
	//private TileMapColliderScript tileMapCollider;

	public Collision2D projectileCollision;

	public float[,] tileArray; 

	public float health;
	public float damage = 0;
	// Use this for initialization
	void Start () {

		tilemap = GetComponent<tk2dTileMap>();

		tileArray = new float[tilemap.width,tilemap.height];


		
		if (proceduralBuildOnStart){
			ProceduralBuild();
		}
		Debug.Log (tileArray[10,1]);
		//tileMapCollider = GetComponent<TileMapColliderScript>();
		//tileMapCollider = TileMapColliderScript.FindObjectOfType<TileMapColliderScript>
	}
	
	// Update is called once per frame
	void Update () {
		if(projectileCollision != null){
			int xPos;
			int yPos;

			ContactPoint2D contactpoint = projectileCollision.contacts[0]; 
			Vector3 tempVec = new Vector3 (contactpoint.point.x+(.5f*contactpoint.normal.x),contactpoint.point.y,5);

			tilemap.GetTileAtPosition(tempVec,out xPos, out yPos);


			int tileId = tilemap.Layers[layerCollider].GetTile(xPos,yPos);
			if (tileId >= 0){
				if (tileArray[xPos,yPos] >0){
					tileArray[xPos,yPos] -= damage;
					int damageTile = (int) (3*(tileArray[xPos,yPos])/health) + 64;
					Debug.Log (damageTile);
					tilemap.Layers[layerColliderDecals].SetTile (xPos,yPos, damageTile);
				}
			    else {
					tilemap.Layers[layerCollider].ClearTile(xPos,yPos);
					tilemap.Layers[layerColliderDecals].ClearTile(xPos,yPos);
					tilemap.Layers[layerColliderCorners].ClearTile(xPos,yPos);
					tilemap.Layers[layerColliderEdges].ClearTile(xPos,yPos);
					setNWESTiles(xPos,yPos); 
				}
				tilemap.Build();
			}

		}
		projectileCollision = null;
		damage = 0;
	}


	void ProceduralBuild(){
		int mapHeight = tilemap.height;
		int mapWidth = tilemap.width;
		for (int x = 1; x <= mapWidth; x++){
			for (int y = 1; y <= mapHeight; y++){

				bool edgeN = false;
				bool edgeE = false;
				bool edgeW = false;
				bool edgeS = false;

				//target tile
				int XY = tilemap.Layers[layerCollider].GetTile(x,y);

				//Get Tile ID's related to target tile
				int _N = tilemap.Layers[layerCollider].GetTile(x,y+1);
				int _E = tilemap.Layers[layerCollider].GetTile(x+1,y);
				int _W = tilemap.Layers[layerCollider].GetTile(x-1,y);
				int _S = tilemap.Layers[layerCollider].GetTile(x,y-1);

				int _edgeN = tilemap.Layers[layerColliderEdges].GetTile(x,y+1);
				if (_edgeN >0){
					edgeN = getBinary(_edgeN, 1);
				}
				int _edgeE = tilemap.Layers[layerColliderEdges].GetTile(x+1,y);
				if (_edgeE >0){
					edgeE = getBinary(_edgeE, 2);
				}
				int _edgeW = tilemap.Layers[layerColliderEdges].GetTile(x-1,y);
				if (_edgeW >0){
					edgeW = getBinary(_edgeW, 3);
				}
				int _edgeS = tilemap.Layers[layerColliderEdges].GetTile(x,y-1);
				if (_edgeS >0){
					 edgeS = getBinary(_edgeS, 4);
				}


				int _NW = tilemap.Layers[layerCollider].GetTile(x-1,y+1);
				int _NE = tilemap.Layers[layerCollider].GetTile(x+1,y+1);
				int _SE = tilemap.Layers[layerCollider].GetTile(x+1,y-1);
				int _SW = tilemap.Layers[layerCollider].GetTile(x-1,y-1);

				//get tileID edge layer
				int _XY = tilemap.Layers[layerColliderEdges].GetTile(x,y);
				int __XY = tilemap.Layers[layerBackground2Edges].GetTile(x,y)-32;
				int XY_Corners = tilemap.Layers[layerColliderCorners].GetTile(x,y)-16;


				if (XY > 0){


					if (_XY < 0){
						_XY = 0;
					}
					if (XY_Corners < 0){
						XY_Corners = 0;
					}


					//ARRAY
					tileArray[x,y] = health;


					// EDGES
					if(_N < 0 || edgeN){
						tilemap.Layers[layerColliderEdges].SetTile(x,y,convertAddBinary(_XY,1));
						_XY = tilemap.Layers[layerColliderEdges].GetTile(x,y);
					}
					
					if (_E < 0 || edgeE) {
						tilemap.Layers[layerColliderEdges].SetTile(x,y,convertAddBinary(_XY,2));
						_XY = tilemap.Layers[layerColliderEdges].GetTile(x,y);
					}
					
					if(_W < 0 || edgeW){
						tilemap.Layers[layerColliderEdges].SetTile(x,y,convertAddBinary(_XY,4));
						_XY = tilemap.Layers[layerColliderEdges].GetTile(x,y);
					}
					
					if(_S < 0 || edgeS) {
						tilemap.Layers[layerColliderEdges].SetTile(x,y,convertAddBinary(_XY,8));
						_XY = tilemap.Layers[layerColliderEdges].GetTile(x,y);
					}


					// CORNERS
					if (_NW < 0 ){
						tilemap.Layers[layerColliderCorners].SetTile(x,y, (convertAddBinary (XY_Corners,1) + 16 ));
						XY_Corners = tilemap.Layers[layerColliderCorners].GetTile(x,y)-16;;
					}
					if (_NE < 0 ){
						tilemap.Layers[layerColliderCorners].SetTile(x,y, (convertAddBinary (XY_Corners,2) + 16 ));
						XY_Corners = tilemap.Layers[layerColliderCorners].GetTile(x,y)-16;;
					}
					if (_SE < 0 ){
						tilemap.Layers[layerColliderCorners].SetTile(x,y, (convertAddBinary (XY_Corners,8) + 16 ));
						XY_Corners = tilemap.Layers[layerColliderCorners].GetTile(x,y)-16;;
					}
					if (_SW < 0 ){
						tilemap.Layers[layerColliderCorners].SetTile(x,y, (convertAddBinary (XY_Corners,4) + 16 ));
						XY_Corners = tilemap.Layers[layerColliderCorners].GetTile(x,y)-16;;
					}



					// BACKGROUND

					tilemap.Layers[layerBackground2].SetTile(x,y, 72);

					int backgroundID = _XY + 32;
					if (backgroundID < 32) backgroundID = 32;
					tilemap.Layers[layerBackground2Edges].SetTile(x,y, backgroundID);
				
					int backgroundCornersID = XY_Corners +48;
					if (backgroundCornersID < 48) backgroundCornersID = 48;
					tilemap.Layers[layerBackground2Corners].SetTile(x,y, backgroundCornersID);
				}
			}
		}
		tilemap.Build ();

	}


	
	/// <summary>
	/// receives coordinates from destroyed tile,
	/// Checks tileIds from n,w,e,s directions and adds the appropriate binary string to them.
	/// Sets the four n,w,e,s tiles to their new tileIds.
	/// </summary>
	/// <param name="str1">Str1.</param>
	/// <param name="str2">Str2.</param>
	/// 
	void setNWESTiles(int xPos, int yPos){
		
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
			if (xPos >= 1){
				tilemap.Layers[layerColliderEdges].SetTile(xPos-1,yPos,convertAddBinary(west,2));
			}
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
			if (xPos >= 1){
				tilemap.Layers[layerColliderCorners].SetTile(xPos-1,yPos+1, (convertAddBinary (NW,8) + 16 ));
			}
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
			if (xPos >= 1){
				tilemap.Layers[layerColliderCorners].SetTile(xPos-1,yPos-1, (convertAddBinary (SW,2) + 16 ));
			}
		}


		//yield return null;

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

	bool getBinary(int tileID, int edgeDirection){

		string s1 = System.Convert.ToString(tileID, 2);
		char[] charArray1 = s1.PadLeft(4, '0').ToCharArray();

		if ( charArray1[edgeDirection-1] == '1'){
			return true;}
		else {
			return false;}
		//Debug.Log (tileID + " : " + edgeDirection + " : " + charArray1 [edgeDirection - 1]);
	}



}
