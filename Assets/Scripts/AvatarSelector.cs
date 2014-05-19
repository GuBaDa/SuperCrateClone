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

	private Vector2 pl1;
	private Vector2 pl2;
	private Vector2 pl3;
	private Vector2 pl4;
	private Vector2 pl5;
	private Vector2 pl6;
	private Vector2 pl7;
	private Vector2 pl8;

	private float avat;

	private Vector2 selectPos;
	private int selection;

	private float marginX;
	private float startX;
	



	// Use this for initialization
	void Start () {
		//Create avatar grid coordinates
		pl1 = new Vector2 (column1, row1);
		pl2 = new Vector2 (column2, row1);
		pl3 = new Vector2 (column3, row1);
		pl4 = new Vector2 (column4, row1);
		pl5 = new Vector2 (column1, row2);
		pl6 = new Vector2 (column2, row2);
		pl7 = new Vector2 (column3, row2);
		pl8 = new Vector2 (column4, row2);
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

		Vector2 mousePos = Input.mousePosition;

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
			if (mousePos.x >= pl1.x - avat && mousePos.x <= pl1.x + avat){
				//Player 1 selected
				selection = 1;
			}
			if (mousePos.x >= pl2.x - avat && mousePos.x <= pl2.x + avat){
				//Player 2 selected
				selection = 2;
			}
			if (mousePos.x >= pl3.x - avat && mousePos.x <= pl3.x + avat){
				//Player 1 selected
				selection = 3;
			}
			if (mousePos.x >= pl4.x - avat && mousePos.x <= pl4.x + avat){
				//Player 2 selected
				selection = 4;
			}
			
		}
		if (mousePos.y >= row2 - avat && mousePos.y <= row2 + avat){
			if (mousePos.x >= pl5.x - avat && mousePos.x <= pl5.x + avat){
				//Player 1 selected
				selection = 5;
			}
			if (mousePos.x >= pl6.x - avat && mousePos.x <= pl6.x + avat){
				//Player 2 selected
				selection = 6;
			}
			if (mousePos.x >= pl7.x - avat && mousePos.x <= pl7.x + avat){
				//Player 1 selected
				selection = 7;
			}
			if (mousePos.x >= pl8.x - avat && mousePos.x <= pl8.x + avat){
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
