using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	//define what type of crate
	public bool weaponsCrate;
	public bool healthCrate;
	public bool constructionCrate;

	public float health;
	public float specialBoost;


	public GameObject[] items;

	private GameObject player;

	private GameObject weaponOld;
	private GameObject weaponNew;
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "Player"){
			if (player == null){
				player = collision.gameObject;
				Debug.Log (player);

				if (weaponsCrate){
					//get old and new weapons, destroy old, initiate new weapon active and set as child to parent player
					if (player.GetComponent<PlayerScript>().weaponActive != null){
						weaponOld = player.GetComponent<PlayerScript>().weaponActive;
					}
					weaponNew = items[Random.Range (0, items.Length)];
					Destroy(weaponOld);
					
					
					GameObject _weaponNew = (GameObject) Instantiate (weaponNew, player.transform.position, Quaternion.identity);
					_weaponNew.transform.parent = player.transform;
					_weaponNew.transform.position += new Vector3 (player.GetComponent<PlayerScript>().weaponPos.x*(player.transform.localScale.x),player.GetComponent<PlayerScript>().weaponPos.y,player.GetComponent<PlayerScript>().weaponPos.z);
					_weaponNew.transform.localScale = new Vector2 (1, 1);
					
					player.GetComponent<PlayerScript>().weaponActive = _weaponNew;
				}
				else if (healthCrate){
					if (player != null){
						Debug.Log(player.GetComponent<PlayerScript>().Health);
						player.GetComponent<PlayerScript>().Health += health;
						Debug.Log("new health:" + player.GetComponent<PlayerScript>().Health);
					}
				}
				
				else if (constructionCrate){
					//do something
				}
				
				else {
					Debug.Log("No boolean set for type cratebox");
				}
			}

				Destroy (gameObject);
		}
	}
}
		
