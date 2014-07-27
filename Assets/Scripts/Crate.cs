﻿using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

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
			player = collision.gameObject;
			Debug.Log (player);
			OnDestroy ();
		}
	}

	void OnDestroy (){
		//get old and new weapons, destroy old, initiate new weapon active and set as child to parent player
		weaponOld = player.GetComponent<PlayerScript>().weaponActive;
		weaponNew = items[Random.Range (0, items.Length)];
		Destroy(weaponOld);


		GameObject _weaponNew = (GameObject) Instantiate (weaponNew, player.transform.position, Quaternion.identity);
		_weaponNew.transform.parent = player.transform;
		_weaponNew.transform.position += new Vector3 (player.GetComponent<PlayerScript>().weaponPos.x*(player.transform.localScale.x),player.GetComponent<PlayerScript>().weaponPos.y,player.GetComponent<PlayerScript>().weaponPos.z);

		player.GetComponent<PlayerScript>().weaponActive = _weaponNew;

		Destroy (gameObject);

	}
}
		
