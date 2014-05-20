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


	private Vector2 selectPos;
	private int selection;

	private float avat;
	private float marginX;
	private float startX;
	



	// Use this for initialization
	void Start () {
		//Create key coordinates for grid
		avat = dimensionsAvatar / 2;

		marginX = column2 - column1;
		startX = column1 - marginX;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown  ("Horizontal") || Input.GetButtonDown  ("Vertical")){
			keySelection();
		}

		//check for mouse activity
		if (Input.GetAxisRaw ("Mouse X") != 0 || Input.GetAxisRaw ("Mouse Y") != 0){
			mouseSelection ();
		}
		transform.position = getPosition (selection);
		
		Debug.Log (getPosition (selection));


	}


	void keySelection(){
		if (Input.GetAxisRaw ("Horizontal") > 0){
			if (selection < 8){
				selection ++;
			}
			else {
				selection = 1;
			}
		}
		if (Input.GetAxisRaw ("Horizontal") < 0){
			if (selection > 1){
				selection --;
			}
			else {
				selection = 8;
			}
		}

		if (Input.GetAxisRaw ("Vertical") > 0){
			if (selection > 4){
				selection -= 4;
			}
			else {
				selection += 4;
			}
		}
		if (Input.GetAxisRaw ("Vertical") < 0){
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

}
