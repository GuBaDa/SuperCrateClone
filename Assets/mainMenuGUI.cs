using UnityEngine;
using System.Collections;

public class mainMenuGUI : MonoBehaviour {

	public GameObject[] buttonArray;

	public int cursor;

	// PlayerController variables
	private float axisVertical;
	private bool axisVerticalDown;
	private bool fire1BtnDown;
	private bool jumpBtnDown;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < buttonArray.Length; i++){
			if (cursor - 1 == i){
				buttonArray[i].GetComponent<GUIButtonScript>().Hover = true;
			}
			else {
				buttonArray[i].GetComponent<GUIButtonScript>().Hover = false;
			}
		}

		getControls ();

		if (axisVerticalDown){
			keySelection();
		}
		if (Input.GetAxisRaw ("Mouse X") != 0 || Input.GetAxisRaw ("Mouse Y") != 0){
			mouseSelection ();
		}


	}

	void keySelection(){
		if (axisVertical < 0){
			if (cursor < buttonArray.Length) {
				cursor ++;
			}
			else {
			cursor = 1;
			}
		}
		if (axisVertical > 0){
			Debug.Log (axisVertical);
			if (cursor > 1){
				cursor --;
			}
			else {
				cursor = buttonArray.Length;
			}
		}
	}

	void mouseSelection() {

		for (int i=0; i <buttonArray.Length; i++){
			if (buttonArray[i].GetComponent<GUIButtonScript>().MouseHover){
				cursor = i + 1;

				break;
			}
		}
	}
		
		
		

	void getControls(){

		// Get variables
		axisVertical = GetComponent<PlayerController>().AxisVertical;
		axisVerticalDown = GetComponent<PlayerController>().AxisVerticalDown;
		fire1BtnDown = GetComponent<PlayerController>().Fire1BtnDown;
		jumpBtnDown = GetComponent<PlayerController>().JumpBtnDown;
	}


	//////////////
	/// Properties
	//////////////

	public int Cursor{
		get {
			return cursor;
		}
		set {
			cursor = value;
		}
	}
}


