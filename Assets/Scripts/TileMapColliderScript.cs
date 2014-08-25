using UnityEngine;
using System.Collections;

public class TileMapColliderScript : MonoBehaviour {

	//[HideInInspector]

	TileMapDestroyScript tileMap;

	// Use this for initialization
	void Start () {
		tileMap = TileMapDestroyScript.FindObjectOfType<TileMapDestroyScript>();
	}
	
	// Update is called once per frame
	void Update () {

		string s = System.Convert.ToString(3, 2); 
		char[] bits = s.PadLeft(4, '0').ToCharArray();

//		char[] temp1 = {'0','0','1','1'};
//		char[] temp2 = {'0','1','0','0'};
//
//		Debug.Log(addBinaryStrings(temp1,temp2));


		//Debug.Log( bits[3] + " " + bits[2] + " " + bits[1] + " " + bits[0] );
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if(coll.collider.gameObject.tag == "Projectile"){
			tileMap.projectileCollision = coll;
		}
	}




}
