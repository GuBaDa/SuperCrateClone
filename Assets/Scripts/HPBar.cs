using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {

	private PlayerScript player;
	private LineRenderer lineRenderer;

	private Color c1;
	private Color c2;
	// Use this for initialization
	void Awake () {
		c1 = Color.red;
		c2 = Color.green;

		player = transform.parent.GetComponent<PlayerScript> ();
		//LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//lineRenderer.SetVertexCount( 1 );
	
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer = GetComponent<LineRenderer>();
		float health = player.Health / 100f;
		Vector3 pos = new Vector3(health,0,0);
		Color newColor = new Color(1-(c1.r * health), c2.g * health * 2, 0, 1); 
		lineRenderer.SetColors (newColor, newColor); 
		lineRenderer.SetPosition(0, pos);

	}
}
