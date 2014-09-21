using UnityEngine;
using System.Collections;

public class DestructibleTimeMap : MonoBehaviour {
	
	private tk2dTileMap tilemap;
	//private GameObject player1;
	
	// Use this for initialization
	void Start () {
		tilemap = GetComponent<tk2dTileMap>();
	}
	
	// Update is called once per frame
	void Update () {
			
			Debug.Log(tilemap.GetTile(1,1,3));
			
			tilemap.ClearTile(1,1,3);
			tilemap.Build();

	}
}
