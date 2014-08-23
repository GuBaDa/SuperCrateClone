using UnityEngine;
using System.Collections;

public class DestructibleTimeMap : MonoBehaviour {
	
	private tk2dTileMap tilemap;
	private PlayerScript playerScript;
	//private GameObject player1;
	
	// Use this for initialization
	void Start () {
		tilemap = GetComponent<tk2dTileMap>();
		playerScript = GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
			
			Debug.Log(tilemap.GetTile(1,1,3));
			
			tilemap.ClearTile(1,1,3);
			tilemap.Build();
			//tilemap.ClearTile(tilemap.GetTilePosition(playerScript.transform.position.x,playerScript.transform.position.y));

	}
}
