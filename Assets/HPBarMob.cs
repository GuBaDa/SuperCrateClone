using UnityEngine;
using System.Collections;

public class HPBarMob : MonoBehaviour {

	private MobScript mob;
	private LineRenderer lineRenderer;
	private float maxHealth;
	// Use this for initialization
	void Start () {
		mob = transform.parent.GetComponent<MobScript> ();
		maxHealth = mob.Health;
		
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer = GetComponent<LineRenderer>();
		
		Vector3 pos = new Vector3(mob.Health/maxHealth,0,0);
		lineRenderer.SetPosition(0, pos);
	}
}
