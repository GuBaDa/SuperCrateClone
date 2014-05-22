using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {
	private PlayerScript player;
	private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<PlayerScript> ();
		//LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//lineRenderer.SetVertexCount( 1 );

	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer = GetComponent<LineRenderer>();

		Vector3 pos = new Vector3(player.Health/100f,0,0);
		lineRenderer.SetPosition(0, pos);
	}
}
