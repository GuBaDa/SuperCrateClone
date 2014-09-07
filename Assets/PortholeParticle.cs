using UnityEngine;
using System.Collections;

public class PortholeParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
			// Set the sorting layer of the particle system.
			//particleSystem.renderer.sortingLayerName = "foreground";
			particleSystem.renderer.sortingOrder = 2;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
