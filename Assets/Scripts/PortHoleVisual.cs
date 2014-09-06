using UnityEngine;
using System.Collections;

public class PortHoleVisual : MonoBehaviour {

	private float rnd_x;
	private float rnd_y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine("ChangeScale");
		StartCoroutine("Fade");
}

IEnumerator Fade() {
	for (float f = 1f; f >= 0f; f -= 0.1f) {
		Color c = renderer.material.color;
		c.a = f;
		renderer.material.color = c;
			yield return new WaitForSeconds(0.3f);
		}
	}

IEnumerator ChangeScale() {
	rnd_x = Random.Range(1f,3f)/10;
	rnd_y = Random.Range(1f,3f)/10;
	
	for (float f = 1f; f >= 0; f -= 0.1f) {
		transform.localScale = new Vector3(f+rnd_x,f+rnd_y,1);
		yield return new WaitForSeconds(0.3f);
		}
	}
}