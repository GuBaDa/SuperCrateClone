using UnityEngine;
using System.Collections;

public class ParticleDestroyScript : MonoBehaviour {

	private float awakeTime;
	// Use this for initialization
	void Awake() {
		awakeTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > ( gameObject.particleSystem.duration + awakeTime))
		{
			Destroy(gameObject);
		}
	}
}
