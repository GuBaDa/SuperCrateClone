using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {
	
	public float scrollSpeed;

	// Use this for initialization
	void Start () {

		//Set material to wrap texture UVs around
		renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = (new Vector2(offset, 0));
		
	}
}