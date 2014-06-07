using UnityEngine;
using System.Collections;

public class AvatarSelector : MonoBehaviour {
	
	public float dimensionsAvatar;
	public float column1;
	public float column2;
	public float column3;
	public float column4;
	public float row1;
	public float row2;
	
	public Color colorSelected;
	public Color colorSweep1;
	public Color colorSweep2;
	
	public float durationSweep;
	
	public GameObject gameConstructor;
	
	public int playerNr;
	
	private Vector2 selectPos;
	private int selection;
	
	private float avat;
	private float marginX;
	private float startX;
	
	private GameConstructor constructor;
	
	// Controls
	
	private float axisHorizontal;
	private float axisVertical;
	private bool axisHorizontalDown;
	private bool axisVerticalDown;
	private bool fire1Btn;
	private bool fire2Btn;
	private bool fire3Btn;
	private bool jumpBtnDown;
	
	
	// Use this for initialization
	void Awake () {
		constructor = gameConstructor.GetComponent<GameConstructor> ();
		if (constructor.NumberOfPlayers < playerNr){
			Destroy (gameObject);
		}
		//Create key coordinates for grid
		avat = dimensionsAvatar / 2;
		
		marginX = column2 - column1;
		startX = column1 - marginX;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		getControls();
		ColorChange ();
		
		if (constructor.PlayersSelected [playerNr - 1] == null){
			if (axisHorizontalDown || axisVerticalDown){
				keySelection();
			}
			
			//check for mouse activity
			if (Input.GetAxisRaw ("Mouse X") != 0 || Input.GetAxisRaw ("Mouse Y") != 0){
				mouseSelection ();
			}
			transform.position = getPosition (selection);
			
			//selection
			if ( fire1Btn || Input.GetKeyDown( KeyCode.Return)){
				//constructor.CharSelected = selection;
				constructor.PlayersSelected [playerNr - 1] = constructor.playersArray[selection -1];
				Debug.Log (constructor.PlayersSelected[playerNr - 1]);
			}
		}
		else {
			//load next level
			if ( Input.GetKeyDown( KeyCode.L)){
				Application.LoadLevel("1P_gameTestEnvironment");
			}
			
			if ( fire1Btn || Input.GetKeyDown( KeyCode.Return)){
				//constructor.CharSelected = 0;
				constructor.PlayersSelected [playerNr - 1] = null;
			}
		}
	}
	
	
	void keySelection(){
		if (axisHorizontal > 0){
			if (selection < 8){
				selection ++;
			}
			else {
				selection = 1;
			}
		}
		if (axisHorizontal < 0){
			if (selection > 1){
				selection --;
			}
			else {
				selection = 8;
			}
		}
		
		if (axisVertical > 0){
			if (selection > 4){
				selection -= 4;
			}
			else {
				selection += 4;
			}
		}
		if (axisVertical < 0){
			if (selection < 5){
				selection += 4;
			}
			else {
				selection -= 4;
			}
		}
		
	}
	
	void mouseSelection() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if (mousePos.y >= row1 - avat && mousePos.y <= row1 + avat){
			if (mousePos.x >= column1 - avat && mousePos.x <= column1 + avat){
				//Player 1 selected
				selection = 1;
			}
			if (mousePos.x >= column2 - avat && mousePos.x <= column2 + avat){
				//Player 2 selected
				selection = 2;
			}
			if (mousePos.x >= column3 - avat && mousePos.x <= column3 + avat){
				//Player 1 selected
				selection = 3;
			}
			if (mousePos.x >= column4 - avat && mousePos.x <= column4 + avat){
				//Player 2 selected
				selection = 4;
			}
			
		}
		if (mousePos.y >= row2 - avat && mousePos.y <= row2 + avat){
			if (mousePos.x >= column1 - avat && mousePos.x <= column1 + avat){
				//Player 1 selected
				selection = 5;
			}
			if (mousePos.x >= column2 - avat && mousePos.x <= column2 + avat){
				//Player 2 selected
				selection = 6;
			}
			if (mousePos.x >= column3 - avat && mousePos.x <= column3 + avat){
				//Player 1 selected
				selection = 7;
			}
			if (mousePos.x >= column4 - avat && mousePos.x <= column4 + avat){
				//Player 2 selected
				selection = 8;
			}
		}
	}
	
	
	void ColorChange (){
		
		if (constructor.PlayersSelected [playerNr - 1] == null){
			float lerp = Mathf.PingPong (Time.time, durationSweep) / durationSweep;
			renderer.material.color = Color.Lerp (colorSweep1, colorSweep2, lerp);
		}
		else {
			renderer.material.color = colorSelected;
		}
		
	}
	
	
	private Vector2 getPosition(int selection) {
		
		if (selection <= 4){
			Vector2 selectPos =  new Vector2 (startX + (marginX * selection), row1);
			return selectPos;
		}
		else {
			Vector2 selectPos =  new Vector2 (startX + (marginX * (selection - 4)), row2);
			return selectPos;
		}
	}
	
	void getControls(){
		// Set control script to right player
		GetComponent<PlayerController>().PlayerControlNr = playerNr;
		// Get variables
		axisHorizontal = GetComponent<PlayerController>().AxisHorizontal;
		axisVertical = GetComponent<PlayerController>().AxisVertical;
		axisHorizontalDown = GetComponent<PlayerController>().AxisHorizontalDown;
		axisVerticalDown = GetComponent<PlayerController>().AxisVerticalDown;
		fire1Btn = GetComponent<PlayerController>().Fire1Btn;
		fire2Btn = GetComponent<PlayerController>().Fire2Btn;
		fire3Btn = GetComponent<PlayerController>().Fire3Btn;
		jumpBtnDown = GetComponent<PlayerController>().JumpBtnDown;
	}
}
