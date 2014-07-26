using UnityEngine;
using System.Collections;

public class GameConstructor : MonoBehaviour {


	private int charSelected;

	private GameObject[] playerSpawnPoints; 

	public bool singleScene;

	public int numberOfPlayers; 

	public int numberOfJoinedPlayers;

	public GameObject[] playersArray;

	public GameObject[] playersSelected;

	private bool isPlayerSpawned;

	void Awake(){
		//check if gameconstructor allready exists from previous scene
		if (singleScene && GameObject.FindGameObjectsWithTag("GameConstructor").Length > 1) {
			Destroy (gameObject);
		}
		if (singleScene) {
			playerSpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPointPlayer");
			if (playersSelected.Length == numberOfPlayers){
				Debug.Log ("CharSelected is present");
				if (playerSpawnPoints.Length != 0){
					PlayersSpawn (playersSelected);
				}
				else {
					Debug.Log ("No SpawnPointPlayer present in scene");
				}
			}
		}

		playersSelected = new GameObject[numberOfPlayers];

		Debug.Log ("GameConstructor Available");

		DontDestroyOnLoad (transform.gameObject);



	}

	void OnLevelWasLoaded(int level){
		//find Player Spawnpoints
		playerSpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPointPlayer");
		Debug.Log ("number of playerSpawnPoints = " + playerSpawnPoints.Length);

		if (level > 0){
			Debug.Log ("level is playable");
			if (playersSelected.Length == numberOfPlayers){
				Debug.Log ("CharSelected is present");
				if (playerSpawnPoints.Length != 0){
					PlayersSpawn (playersSelected);
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

	void PlayersSpawn (GameObject[] pls){
		if (pls != null && pls.Length <= playerSpawnPoints.Length){
			Debug.Log ("PlayerSpawn initiated");
			int _playerControlNr = 0;
			foreach (GameObject pl in pls){
				GameObject spawnPoint = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)];
				GameObject pPlayer = (GameObject) Instantiate (pl, spawnPoint.transform.position, Quaternion.identity);
				_playerControlNr ++;
				pPlayer.GetComponent<PlayerScript>().PlayerControlNr = _playerControlNr;

				//remove old spawnpoint fro array;
				GameObject[] _playerSpawnPoints = new GameObject[playerSpawnPoints.Length -1];
				for (int i=0; i < _playerSpawnPoints.Length; i++){
					if (playerSpawnPoints[i] != spawnPoint){
						_playerSpawnPoints[i] = playerSpawnPoints[i];
					}
				}
				playerSpawnPoints = _playerSpawnPoints;
			}
			// hide SpawnPoints
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("SpawnPointPlayer")){
				i.renderer.enabled = false;
			}
		} 
		else {
			Debug.Log ("No Player available to be spawned or too few spawnpoints");
		}
	}



/////////////////////////////////////////////////////
//		GLOBAL PROPERTIES   
/////////////////////////////////////////////////////

	public int NumberOfPlayers {
		get {
			return numberOfPlayers;
		}
		set {
			numberOfPlayers = value;
		}
	}
	
	public GameObject[] PlayersSelected {
		get {
			return playersSelected;
		}
		set {
			playersSelected = value;
		}
	}



	public int CharSelected {
		get {
			return charSelected; 
		}
		set {
			charSelected = value; 
		}
	}
}