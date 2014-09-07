using UnityEngine;
using System.Collections;

public class PortHoleVisual : MonoBehaviour {

	private float rnd_x;
	private float rnd_y;

	private bool opened;

	public GameObject psMob;

	// Use this for initialization
	void Awake () {
		transform.localScale = new Vector3 (0, 1, 1);
		opened = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x <= 1) {
			StartCoroutine ("Open");
		}

		if (transform.localScale.x >= 1 && opened == false ) {
			opened = true;
			Instantiate(psMob, transform.position, Quaternion.identity);
		}

//		if (transform.localScale.x >= 1 || opened == true) {
//			opened = true;
//			StartCoroutine ("Close");
//		}

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
		float f = transform.localScale.x;
		f = f + 0.01f;
		transform.localScale = new Vector3(f,1,1);
		yield return new WaitForSeconds(0.1f);

	}

	IEnumerator Close () {
		float f = transform.localScale.x;
		f = f - 0.01f;
		transform.localScale = new Vector3(f,1,1);
		yield return new WaitForSeconds(0.1f);
		
	}


}