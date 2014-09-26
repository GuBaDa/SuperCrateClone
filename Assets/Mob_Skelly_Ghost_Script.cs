using UnityEngine;
using System.Collections;

public class Mob_Skelly_Ghost_Script : MonoBehaviour {

	private float timeSinceFloat;
	private float coolDown;

	// Use this for initialization
	void Awake () {
		coolDown = .2f;
		timeSinceFloat =0;

	}
	
	// Update is called once per frame
	void Update () {
		timeSinceFloat += Time.deltaTime;
		if(timeSinceFloat > coolDown){
			timeSinceFloat = 0;
			doFloat();
		}
	}




	void doFloat(){
		rigidbody2D.AddForce(new Vector2(2f,1f),ForceMode2D.Impulse);

	}
}
