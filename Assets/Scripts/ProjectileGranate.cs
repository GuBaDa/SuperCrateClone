using UnityEngine;
using System.Collections;

public class ProjectileGranate : MonoBehaviour {

	public float detonateTime;
	public GameObject blastPrefab;

	public GameObject particleObject;

	public float blastRange;
	public int numberOfBlasts;

	private bool loop; 
	private ParticleSystem particles;

	void Awake(){
		//StartCoroutine (BlastSpawner(0.1f));
		loop = false;
		particles = particleObject.particleSystem;
	}

	void Update(){
		detonateTime -= Time.deltaTime;

		if (detonateTime < 0 && !loop){
			particles.Play();
			renderer.enabled = false;
			StartCoroutine (BlastSpawner(0.01f));
		}
	}

	IEnumerator BlastSpawner(float time) { 
		loop = true;
		Debug.Log ("IENUMERATOR");


		for(int i=0; i<numberOfBlasts; i++) {
			SpawnBlast(); 
			if (i == numberOfBlasts -1){
				particles.Stop();
				yield return new WaitForSeconds(2f);
				Destroy (gameObject);
			}
			yield return new WaitForSeconds(time);
		}
	}


	void SpawnBlast(){
		float randomX = Random.Range (0, blastRange *2) - blastRange; 
		float randomY = Random.Range (0, blastRange *2) - blastRange;
		Vector2 pos = new Vector2 (transform.position.x + randomX, transform.position.y + randomY);
		GameObject pPrefab = (GameObject) Instantiate (blastPrefab, pos, Quaternion.identity);
	}
}