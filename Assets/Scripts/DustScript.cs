using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {

	public int nrOfDustParticles;
	public float maxSize;
	public float maxSpeed;

	private ParticleSystem particles;

	// Use this for initialization
	void Awake () {
		particles = GetComponentInChildren<ParticleSystem>();

		for (int i=0; i<nrOfDustParticles; i++){
			particles.startSize = Random.Range (1, maxSize)/(maxSize*2.5f);
			particles.startSpeed = Random.Range (1,maxSpeed);
			particles.Emit(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!particles.IsAlive()){
			Destroy (gameObject);
		}
	}

}
