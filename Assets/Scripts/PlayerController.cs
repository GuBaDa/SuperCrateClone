using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int PlayerControlNr;

	//properties

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
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (PlayerControlNr){
			case 0 :
				Debug.Log("no PlayerNr defined in PlayerController");
				break;
			case 1 :
				axisHorizontal = Input.GetAxisRaw ("Horizontal");
				axisVertical = Input.GetAxisRaw ("Vertical");
				axisHorizontalDown = Input.GetButtonDown ("Horizontal");
				axisVerticalDown = Input.GetButtonDown ("Vertical");
				fire1Btn = Input.GetButton("Fire1");
				fire2Btn = Input.GetButton("Fire2");
				fire3Btn = Input.GetButton("Fire3");
				jumpBtnDown = Input.GetButtonDown("Jump");
				break;
			case 2 :
				axisHorizontal = Input.GetAxisRaw ("Horizontal_2");
				axisVertical = Input.GetAxisRaw ("Vertical_2");
				axisHorizontalDown = Input.GetButtonDown ("Horizontal_2");
				axisVerticalDown = Input.GetButtonDown ("Vertical_2");
				fire1Btn = Input.GetButton("Fire1_2");
				fire2Btn = Input.GetButton("Fire2_2");
				fire3Btn = Input.GetButton("Fire3_2");
				jumpBtnDown = Input.GetButtonDown("Jump_2");
				break;
			default :
				Debug.Log ("no PlayerNr defined in PlayerController");
				break;
			}


	}



///////////////////////////////////////////////////////////////////////////////
///  PROPERTIES
///////////////////////////////////////////////////////////////////////////////

	public float AxisHorizontal{
		get{
			return axisHorizontal;
		}
	}

	public float AxisVertical{
		get{
			return axisVertical;
		}
	}

	public bool AxisHorizontalDown{
		get{
			return axisHorizontalDown;
		}
	}
	
	public bool AxisVerticalDown{
		get{
			return axisVerticalDown;
		}
	}

	public bool Fire1Btn{
		get{
			return fire1Btn;
		}
	}

	public bool Fire2Btn{
		get{
			return fire2Btn;
		}
	}
	public bool Fire3Btn{
		get{
			return fire3Btn;
		}

	}
	public bool JumpBtnDown{
		get{
			return jumpBtnDown;
		}

	}
}