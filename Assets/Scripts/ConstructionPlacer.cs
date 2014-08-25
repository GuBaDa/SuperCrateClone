using UnityEngine;
using System.Collections;

public class ConstructionPlacer : MonoBehaviour {

	public Color possible1;
	public Color possible2;

	public Color impossible1;
	public Color impossible2;
	public float lerpTime;

	private GameObject item;
	public GameObject Item{
		get { return item;}
		set { item = value;}
	}

	private GameObject player;

	private bool istrigger; 


	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null){
			player = transform.parent.gameObject;
		}

		//snap to grid
		//Debug.Log ((int) player.transform.localScale.x);
		//if (player.GetComponent<PlayerScript>().grounded) {

			int playerPosX = (int) player.transform.position.x;
			int playerPosY = (int) player.transform.position.y;
			int snapTileX = playerPosX + 1 + (1 * ((int) player.transform.localScale.x));
			int snapTileY = playerPosY;
			int playerLookY;
			int xFactor;
			if (player.GetComponent<PlayerController>().AxisVertical > 0) {
				playerLookY = 1;
				xFactor = 0;
			}
			else if (player.GetComponent<PlayerController>().AxisVertical < 0) {
				playerLookY = -1;
				xFactor = 0;
			}
			else {
				playerLookY = 0;
				xFactor = 1;
			}
			Vector2 offset = new Vector2 ( Mathf.Abs (snapTileX - player.transform.position.x), snapTileY - player.transform.position.y + playerLookY) ;
			if (player.transform.localScale.x == -1){ offset.x ++;}
			offset.x *= xFactor;
			transform.localScale = new Vector2 (1,1);
			transform.localPosition = offset;
		//}

		//set color
		float lerp = Mathf.PingPong (Time.time, lerpTime) / lerpTime;
		Color newColor = Color.Lerp (possible1, possible2, lerp);
		if (istrigger){
			renderer.material.SetColor("_TintColor",Color.Lerp(impossible1,impossible2, lerp)); 
		}
		else {
			renderer.material.SetColor("_TintColor",Color.Lerp(possible1,possible2, lerp)); 
		}

		//
	}

	void OnTriggerStay2D (Collider2D coll) {
		istrigger = true;
	}

	void OnTriggerExit2D (Collider2D coll) {
		istrigger = false;
	}
}
