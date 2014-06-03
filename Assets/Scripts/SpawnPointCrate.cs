using UnityEngine;
using System.Collections;

public class SpawnPointCrate : MonoBehaviour {

	public bool isDebug;

	public bool isStatic;

	public GameObject[] cratesArray;
	
	public float spawnTimer;
	
	public Vector2 SpawnAreaBottomLeft;
	
	public Vector2 SpawnAreaUpperRight;
	
	// Use this for initialization
	void Awake () {
		//hide object
		if (!isDebug){
			renderer.enabled = false;
		}
		if (cratesArray.Length != 0 && spawnTimer > 0){
			//start spawn timer
			StartCoroutine ("SpawnCrateTimer", spawnTimer);
			
		}
		
		else {
			Debug.Log ("No gameobjects present in cratesArray");
		}
	}
	
	
	IEnumerator SpawnCrateTimer (){
		while (true){
			//wait for seconds, reposition spawner, spawn random object for cratesArray
			yield return new WaitForSeconds (spawnTimer); 
			GameObject crateToSpawn = cratesArray [Random.Range (0, cratesArray.Length)];
			SpawnCrate (crateToSpawn);
			yield return null;
		}
	}


	Vector2 NewPos (){ 
		return new Vector3 (Random.Range (SpawnAreaBottomLeft.x, SpawnAreaUpperRight.x),
		                    Random.Range (SpawnAreaBottomLeft.y, SpawnAreaUpperRight.y),
		                    transform.position.z);
	}
	
	void SpawnCrate (GameObject crate){
		Vector3 pos = transform.position;
		if (!isStatic){
			pos = NewPos ();
		}
		Instantiate (crate, pos, Quaternion.identity);
	}
	
	
}
