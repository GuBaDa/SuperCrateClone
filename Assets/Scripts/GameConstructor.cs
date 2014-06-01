using UnityEngine;
using System.Collections;

public class GameConstructor : MonoBehaviour {


	private int charSelected;

	private GameObject[] playerSpawnPoints; 

	public GameObject[] playersArray;

	private bool isPlayerSpawned;

	void Start(){
		Debug.Log ("GameConstructor Available");

		DontDestroyOnLoad (transform.gameObject);



	}

	void OnLevelWasLoaded(int level){
		//find Player Spawnpoints
		playerSpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPointPlayer");
		Debug.Log ("number of playerSpawnPoints = " + playerSpawnPoints.Length);
		if (level > 0){
			Debug.Log ("level is playable");
			if (CharSelected > 0 && CharSelected <= playersArray.Length ){
				Debug.Log ("CharSelected is present");
				if (playerSpawnPoints.Length != 0){
					PlayerSpawn (playersArray[CharSelected - 1]);
				}
				else {
					Debug.Log ("No SpawnPointPlayer present in scene");
				}
			}
		}
		else {
			Debug.Log ("Level is Start Level");
		}
	}

	void PlayerSpawn (GameObject pl){
		if (pl != null){
			Debug.Log ("PlayerSpawn initiated");
			GameObject spawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)];
			Instantiate (pl, spawnPoint.transform.position, Quaternion.identity);
			foreach(GameObject i in playerSpawnPoints){
				i.renderer.enabled = false;
			}
		} 
		else {
			Debug.Log ("No Player available to be spawned");
		}
	}



/////////////////////////////////////////////////////
//		GLOBAL PROPERTIES   
/////////////////////////////////////////////////////

	public int CharSelected {
		get {
			return charSelected; 
		}
		set {
			charSelected = value; 
		}
	}
}