using UnityEngine;
using System.Collections;

public class PortHoleVisual : MonoBehaviour {

	private float rnd_x;
	private float rnd_y;

	// Use this for initialization
	void Awake () {
	transform.localScale = new Vector3 (0, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine("Open");
		//StartCoroutine("Fade");
}

IEnumerator Fade() {
	for (float f = 1f; f >= 0f; f -= 0.1f) {
		Color c = renderer.material.color;
		c.a = f;
		renderer.material.color = c;
			yield return new WaitForSeconds(0.3f);
		}
	}

IEnumerator Open () {
	rnd_x = Random.Range(1f,3f)/10;
	rnd_y = Random.Range(1f,3f)/10;
	
	for (float f = 0f; f <= 1f; f += 0.1f) {
		transform.localScale = new Vector3(f,1,1);
		yield return new WaitForSeconds(0.1f);
		}
	}
}