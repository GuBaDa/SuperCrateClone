using UnityEngine;
using System.Collections;

public class CharWheelSelect : MonoBehaviour {

	public int playerNr;

	public int nrOfCharacters;
	public float wheelSpeed;

	public Color textColorNormal;
	public Color textColorHover;
	public Color textColorActive;

	public Sprite buttonNormal;
	public Sprite buttonHover;
	public Sprite buttonActive;

	public Sprite textSelect;
	public Sprite textReady;
	public Sprite textIsReady;
	public Sprite textStart;

	public Sprite borderNormal;
	public Sprite borderJoined;

	public Sprite[] characterNames;

	public Color colorSweep1;
	public Color colorSweep2;
	public Color colorSweepSelecter1;
	public Color colorSweepSelecter2;

	public GameObject gameConstructor;

	private GameConstructor constructor;

	//selection state booleans
	private bool join = false;
	private bool selected = false;
	private bool ready = false;
	private bool allReady = false;

	private int selection = 1;  

	// PlayerController variables
	private float axisHorizontal;
	private float axisVertical;
	private bool axisHorizontalDown;
	private bool axisVerticalDown;
	private bool fire1Btn;
	private bool fire1BtnDown;
	private bool fire2Btn;
	private bool fire2BtnDown;
	private bool fire3Btn;
	private bool jumpBtnDown;


	//
	private Transform avatarWheel;
	private Transform selectionSprite;
	private Transform buttonUp;
	private Transform buttonDown;
	private Transform playerText;
	private Transform stateText;
	private Transform startText;
	private Transform characterName;
	private GameObject lerper; 
	private float avatarOffsetTarget;
	private float offsetDistance; 

	// Use this for initialization
	void Awake () {
		//define children and constructor
		selectionSprite = transform.FindChild ("SelectionSprite");
		avatarWheel = transform.FindChild ("charWheelAvatar");
		buttonUp = transform.FindChild ("charWheelArrowUp");
		buttonDown = transform.FindChild ("charWheelArrowDown");
		playerText = transform.FindChild ("charWheelPlayer");
		stateText = transform.FindChild ("charWheelState");
		startText = transform.FindChild ("charWheelStart");
		characterName = transform.FindChild ("charWheelName");

		constructor = gameConstructor.GetComponent<GameConstructor> ();

		lerper = GameObject.Find ("lerpTimer");
		if (lerper == null){
			Debug.Log ("No lerpTimer found");
		}


		avatarWheel.renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
		avatarOffsetTarget = avatarWheel.renderer.material.mainTextureOffset.y;
		//hide inactives
		avatarWheel.renderer.enabled = false;
		selectionSprite.renderer.enabled = false;
		characterName.renderer.enabled = false;
		stateText.renderer.enabled = false;
		playerText.GetComponent<SpriteRenderer> ().material.color = textColorNormal;
		stateText.GetComponent<SpriteRenderer> ().material.color = textColorNormal;
		//characterName.GetComponent<SpriteRenderer> ().material.color = textColorNormal;
		GetComponent<SpriteRenderer> ().sprite = borderNormal;


		offsetDistance = 1f / nrOfCharacters;
	}
	
	// Update is called once per frame
	void Update () {

		getControls ();
		AvatarWheel ();
		ColorChange ();
		//join player and enable/disable renderers
		if (fire1BtnDown && !join){
			fire1BtnDown = false;
			join = true;
			constructor.numberOfJoinedPlayers ++;
			playerText.GetComponent<SpriteRenderer> ().material.color = textColorHover;
			stateText.GetComponent<SpriteRenderer> ().material.color = textColorHover;
			GetComponent<SpriteRenderer> ().sprite = borderJoined;
			avatarWheel.renderer.enabled = true;
			selectionSprite.renderer.enabled = true;
			startText.renderer.enabled = false;
			characterName.renderer.enabled = true;
			stateText.renderer.enabled = true;
		}
		//if joined check for button presses, do debounce and actions needed.
		if (join) {
			//not selected, check for key presses and may later on mouse/joystick movement.
			if (!selected) {
				if (axisVerticalDown) { 
				keySelection ();
				}
				//if firebutton1 pressed, check for existing character in array and select character if possible
				if (fire1BtnDown) {
					fire1BtnDown = false;
					if (selection <= constructor.playersArray.Length){

						selected = true;
					}
				}
				// unselect selection
				if (fire2BtnDown || jumpBtnDown) {
					fire2BtnDown = false;
					jumpBtnDown = false;
					selected  = false;
				}
			}
			//if selection is made
			if (selected) {
				//if firebutton1 is pressed while selection, define and store right character in game constructor object
				if (fire1BtnDown) {
					fire1BtnDown = false;
					constructor.PlayersSelected [playerNr - 1] = constructor.playersArray[selection - 1];
					ready = true;
				}
				//unselect selection
				if (fire2BtnDown || jumpBtnDown) {
					fire2BtnDown = false;
					jumpBtnDown = false;
					constructor.PlayersSelected [playerNr - 1] = null;
					ready = false;
					selected = false;
				}
				//check if all are selected
				if (constructor.numberOfJoinedPlayers >1){
					allReady = true;
					for (int i = 0; i < constructor.numberOfJoinedPlayers; i++){
						if (constructor.PlayersSelected[i] == null) {
							allReady = false;
						}
					}
				}
			}
			//if all joined players are ready with selection
			if (allReady) {
				//press L to load test level
				if (Input.GetKeyDown( KeyCode.L)){
					//load next level
					Application.LoadLevel("1P_gameTestEnvironment");
				}
			}
		}

		//debug
		//Debug.Log ( constructor.PlayersSelected);
	}


	void ColorChange (){
		float lerp = lerper.GetComponent<lerpTimer> ().Lerp ; //Mathf.PingPong (Time.time, durationSweep) / durationSweep;
		float lerp2 = lerper.GetComponent<lerpTimer> ().Lerp2; //Mathf.PingPong (Time.time, 5 * durationSweep) / (5 * durationSweep);
		//if not joined yet
		if (!join && !allReady) {
			startText.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp);
		}
		if (!join && allReady) {
			startText.GetComponent<SpriteRenderer> ().material.color = textColorNormal;
		}
		//if joined, then check if selected or ready..
		else {
			if (!selected) {
				//waiting for selection

				stateText.GetComponent<SpriteRenderer> ().sprite = textSelect;
				selectionSprite.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp2);
				stateText.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp2);
				characterName.GetComponent<SpriteRenderer> ().material.color = textColorNormal;
				if (playerNr <= constructor.numberOfJoinedPlayers) {
					GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweepSelecter1, colorSweepSelecter2, lerp);
				}
			}
			else {
				//when selected
				if (ready) {
				//selection made: ready!
					if (!allReady){
					// not all ready
						stateText.GetComponent<SpriteRenderer> ().sprite = textIsReady;
						GetComponent<SpriteRenderer> ().material.color = colorSweepSelecter2;
						selectionSprite.GetComponent<SpriteRenderer> ().material.color = colorSweepSelecter2;
						stateText.GetComponent<SpriteRenderer> ().material.color = textColorActive;
						characterName.GetComponent<SpriteRenderer> ().material.color = colorSweepSelecter2;
					}
					else {
					//if all are ready
						stateText.GetComponent<SpriteRenderer> ().sprite = textStart; 
						stateText.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweepSelecter1, colorSweepSelecter2, lerp);
						selectionSprite.GetComponent<SpriteRenderer> ().material.color = colorSweepSelecter2;
						characterName.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweepSelecter1, colorSweepSelecter2, lerp);

					}


				}
				else {
				//selection made: is ready?
					stateText.GetComponent<SpriteRenderer> ().sprite = textReady;
					GetComponent<SpriteRenderer> ().material.color = colorSweepSelecter2;
					selectionSprite.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp2);
					stateText.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp2);
					characterName.GetComponent<SpriteRenderer> ().material.color = Color.Lerp (colorSweep1, colorSweep2, lerp2);
				}
			}
		}
	}


	 void keySelection (){
		if (axisVertical > 0){ //up
			selection --;
			if (selection == 0) {selection = nrOfCharacters;}

			avatarOffsetTarget += offsetDistance;
		}
		if (axisVertical < 0){ //down
			selection ++;
			if (selection == nrOfCharacters + 1) {selection = 1;}

			avatarOffsetTarget -= offsetDistance;
		}
	}

	void AvatarWheel (){
		if (avatarOffsetTarget * 100 > avatarWheel.renderer.material.mainTextureOffset.y * 100){
			avatarWheel.renderer.material.mainTextureOffset += new Vector2 (0, wheelSpeed / 100);
			characterName.renderer.enabled = false;
		}
		if (avatarOffsetTarget * 100 < avatarWheel.renderer.material.mainTextureOffset.y * 100){
			avatarWheel.renderer.material.mainTextureOffset -= new Vector2 (0, wheelSpeed / 100);
			characterName.renderer.enabled = false;
		}
		//snap to place (otherwise float not precise)
		if (Mathf.Abs (avatarOffsetTarget - avatarWheel.renderer.material.mainTextureOffset.y) < 0.03){
			avatarWheel.renderer.material.mainTextureOffset = new Vector2 (0, avatarOffsetTarget);
			characterName.GetComponent<SpriteRenderer> ().sprite = characterNames [selection - 1];
			if (join){
				characterName.renderer.enabled = true;
			}
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
		fire1BtnDown = GetComponent<PlayerController>().Fire1BtnDown;
		fire2Btn = GetComponent<PlayerController>().Fire2Btn;
		fire2BtnDown = GetComponent<PlayerController>().Fire2BtnDown;
		fire3Btn = GetComponent<PlayerController>().Fire3Btn;
		jumpBtnDown = GetComponent<PlayerController>().JumpBtnDown;
	}
}
