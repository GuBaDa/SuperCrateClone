using UnityEngine;
using System.Collections;

public class SideColliderMobScript : MonoBehaviour {

	
	private bool goRight;
	
	
	// Use this for initialization
	void Awake () {
		goRight = true;
	}
	
	
	void OnTriggerStay2D(Collider2D coll){
		
		if(coll.gameObject.tag == "TileMap" ){
			goRight = false;
		}
	}
	
	
	void OnTriggerExit2D(Collider2D coll){
		goRight = true;
	}
	
	
	void Update(){
		GetComponentInParent<Mob_Skelly_Script>().goRight = goRight;
	}
}