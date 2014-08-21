using UnityEngine;
using System.Collections;

public class RespawnCircle : MonoBehaviour {
	public int playerNr;
	public Color[] players;
	
	public float flickerTime;

	public float timeTillDestroy;
	
	private float timer = 0;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		renderer.material.SetColor ("_TintColor", players [playerNr - 1]);
		GetComponent<SpriteRenderer>().color = players [playerNr - 1];
		StartCoroutine ("Flicker");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	IEnumerator Flicker (){
		while (true){
			yield return new WaitForSeconds(flickerTime);
			renderer.enabled = !renderer.enabled;

			if (timer > timeTillDestroy){
				Destroy (gameObject);
			}
		}
	}
	
}
