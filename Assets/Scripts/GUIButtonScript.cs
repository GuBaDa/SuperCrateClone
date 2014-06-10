using UnityEngine;
using System.Collections;

public class GUIButtonScript : MonoBehaviour {

	public Color textColorNormal;
	public Color textColorHover;
	public Color textColorActive;

	public Sprite buttonNormal;
	public Sprite buttonHover;
	public Sprite buttonActive;

	private bool hover = false;
	private bool activ = false;

	private bool mouseHover = false;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hover && !activ){
			transform.FindChild("GUIButton").GetComponent<SpriteRenderer>().sprite = buttonHover;
			transform.FindChild("GUIText").GetComponent<SpriteRenderer>().material.color = textColorHover;
			
		}
		if (!hover && !activ) {
			transform.FindChild("GUIButton").GetComponent<SpriteRenderer>().sprite = buttonNormal;
			transform.FindChild("GUIText").GetComponent<SpriteRenderer>().material.color = textColorNormal;
		}
		if (activ) {
			hover = false;
			mouseHover = false;
			StartCoroutine("OnClick");
		}
	}


	public IEnumerator OnClick(){
		while (true) {
			transform.FindChild ("GUIButton").GetComponent<SpriteRenderer> ().sprite = buttonActive;
			transform.FindChild ("GUIText").GetComponent<SpriteRenderer> ().material.color = textColorActive;
			yield return new WaitForSeconds (0.15f);
			activ = false;
			break;
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

	public bool Activ {
		get {
			return activ;
		}
		set {
			activ = value;
		}
	}
	
}