using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void Update () {
		if (player != null){
			transform.position = player.transform.position;
		} else {
			Destroy(gameObject);
		}
	}
}
