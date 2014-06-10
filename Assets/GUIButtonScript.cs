using UnityEngine;
using System.Collections;

public class GUIButtonScript : MonoBehaviour {

	public Color textColorNormal;
	public Color textColorHover;
	public Color textColorActive;

	public Sprite buttonNormal;
	public Sprite buttonHover;
	public Sprite buttonActive;

	public string loadSceneonClick;
	
	private bool hover = false;

	private bool mouseHover = false;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hover){
			transform.FindChild("GUIButton").GetComponent<SpriteRenderer>().sprite = buttonHover;
			transform.FindChild("GUIText").GetComponent<SpriteRenderer>().material.color = textColorHover;
			
		}
		else {
			transform.FindChild("GUIButton").GetComponent<SpriteRenderer>().sprite = buttonNormal;
			transform.FindChild("GUIText").GetComponent<SpriteRenderer>().material.color = textColorNormal;
		}
	}

	void OnMouseEnter () {
		mouseHover = true;
	}

	void OnMouseExit () {
		mouseHover = false;
	}



	public bool MouseHover {
		get {
			return mouseHover;
		}
	}

	public bool Hover {
		get {
			return hover;
		}
		set {
			hover = value;
		}
	}
	
}