using UnityEngine;
using System.Collections;

public class hpBarPlayers : MonoBehaviour {

	public int healtForPlayerNr;

	private GameObject[] players;

	public GameObject player; 

	private float hp;
	private LineRenderer lineRenderer;
	public Vector3 posStart;

	public Color colorLow;
   	public Color colorFull;

	// Use this for initialization
	void Awake () {
		//temporary setup for single player testing
		//needs better way to figure out wich player to connect in future
		players = GameObject.FindGameObjectsWithTag("Player");
		if (players.Length >= healtForPlayerNr){
			player = players[healtForPlayerNr - 1];

		}
		else{
			Debug.Log("NO Player available");
			Destroy (gameObject);
		}
		lineRenderer = GetComponent<LineRenderer>();

		lineRenderer.material = new Material (Shader.Find ("Sprites/Default"));
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag("Player").Length > 0){
			hp = player.GetComponent<PlayerScript>().Health / 100;
			Debug.Log (hp);
			float hpLength = (hp * 2.8f) + posStart.x;
			Vector3 posEnd = new Vector3(hpLength,0,0.5f);
			float R = colorLow.r * (1-hp) + colorFull.r * hp *2;
			float G = colorLow.g * (1-hp) + colorFull.g * hp *2;
			float B = colorLow.b * (1-hp) + colorFull.b * hp *2;
			Color newColor = new Color(R,G,B, 1); 

			lineRenderer.SetPosition(0, posEnd);
			lineRenderer.SetColors (newColor, newColor); 
		}
	}
}
