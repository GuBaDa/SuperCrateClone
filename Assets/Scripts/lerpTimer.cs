using UnityEngine;
using System.Collections;

public class lerpTimer : MonoBehaviour {

	public float lerpTime;
	public float lerpTime2;

	private float lerp;
	private float lerp2;

	// Update is called once per frame
	void Update () {
		lerp = Mathf.PingPong (Time.time, lerpTime) / lerpTime;
		lerp2 = Mathf.PingPong (Time.time, lerpTime2) / lerpTime2;
	}


	public float Lerp{
		get{
			return lerp;
		}
	}

	public float Lerp2{
		get{
			return lerp2;
		}
	}
}
