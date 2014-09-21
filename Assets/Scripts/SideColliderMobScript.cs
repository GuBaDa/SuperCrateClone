using UnityEngine;
using System.Collections;

public class SideColliderMobScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	}

	void OnTriggerEnter2D(Collider2D coll){
		
		if(coll.gameObject.tag == "TileMap" ){
			transform.parent.transform.localScale = new Vector2(transform.parent.transform.localScale.x*-1,transform.parent.transform.localScale.y);
		}
	}
	
	

}