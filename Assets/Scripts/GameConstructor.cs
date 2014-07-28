using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameConstructor : MonoBehaviour {

	private List<GameObject> spList = new List<GameObject>();

	private int charSelected;

	private GameObject[] playerSpawnPoints; 

	public bool singleScene;

	public int numberOfPlayers; 

	public int numberOfJoinedPlayers;

	public GameObject[] playersArray;

	public GameObject[] playersSelected;

	private bool isPlayerSpawned = false;

	void Awake(){
		//check if gameconstructor allready exists from previous scene

		if (singleScene) {
			playerSpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPointPlayer");
			Debug.Log ("number of playerSpawnPoints = " + playerSpawnPoints.Length);
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

	void Update () {
		if (singleScene && GameObject.FindGameObjectsWithTag("GameConstructor").Length > 1) {
			Destroy (gameObject);
		}
	}

	void OnLevelWasLoaded(int level){
		//find Player Spawnpoints


		if (level > 0){
			playerSpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPointPlayer");
			Debug.Log ("number of playerSpawnPoints = " + playerSpawnPoints.Length);
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
			//Debug.Log ("PlayerSpawn initiated | spawnpoint : " + pls.Length + " | " + playerSpawnPoints.Length);
			int _playerControlNr = 0;
			//make list
			foreach (GameObject sp in playerSpawnPoints) {
				spList.Add (sp);
			}
			foreach (GameObject pl in pls) {
				int spawnPointNr = Random.Range(0, spList.Count);
				GameObject spawnPoint = spList[spawnPointNr];
				Debug.Log ("spawnpoint nr :" + spawnPointNr);
				GameObject pPlayer = (GameObject) Instantiate (pl, spawnPoint.transform.position, Quaternion.identity);
				_playerControlNr ++;
				pPlayer.GetComponent<PlayerScript>().PlayerControlNr = _playerControlNr;

				//remove old spawnpoint from array;
				spList.RemoveAt(spawnPointNr);
				Debug.Log (spList.Count);
				/*
				GameObject[] _playerSpawnPoints = new GameObject[playerSpawnPoints.Length -1];
				for (int i=0; i < _playerSpawnPoints.Length; i++){
					if (playerSpawnPoints[i] != spawnPoint){
						_playerSpawnPoints[i] = playerSpawnPoints[i];
					}
				}
				playerSpawnPoints = _playerSpawnPoints;
				*/
			}
			// hide SpawnPoints
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("SpawnPointPlayer")){
				i.renderer.enabled = false;
			}
		} 
		
		else {
			Debug.Log ("No Player available to be spawned or too few spawnpoints");
		}
			isPlayerSpawned = true;
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