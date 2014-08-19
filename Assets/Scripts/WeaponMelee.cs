using UnityEngine;
using System.Collections;

public class WeaponMelee : MonoBehaviour {

	Animator anim;


	public float damage; 
	public ParticleSystem psPlayerHit;
	public bool randomFlipY;

	private bool fire1Btn;
	private bool fire1BtnDown;
	private Quaternion q;

	private bool attack = false;
	private GameObject player;

	// Use this for initialization
	void Awake () {
		// Set control script to right player

		//define animator
		anim = GetComponent<Animator> ();
		//define parent plaer

	}


	// Update is called once per frame
	void Update () {
		getControls();
		if (player == null){
			player = transform.parent.gameObject;
		}
		if (fire1BtnDown) {
			anim.SetBool("animSwing", true);
			anim.SetBool("animIdle", false);
			if (randomFlipY){
				transform.localScale = new Vector2 (1,Random.Range(0, 2) * 2 -1);
			}
		}
		/*
		//flip if needed
		if (player.transform.localScale.x < 0) {
			transform.localRotation.Set (0f, 180f, 0f, 0f);
		}
		else {
			transform.localRotation.Set (0f, 0f, 0f, 0f);
			}
*/
	}

	public void SetAttackOn (){
		attack = true;

	}
	public void SetAttackOff (){
		attack = false;
		anim.SetBool("animSwing", false);
		anim.SetBool("animIdle", true);

	}

	void OnTriggerEnter2D(Collider2D coll){
		// For players
		if (attack){
			GameObject enemy = coll.gameObject;
			if (enemy.tag == "Player" && enemy != player) {
				PlayerScript playerScript = enemy.GetComponent<PlayerScript>();
				playerScript.Health -= damage;
				psPlayerHit.maxParticles = (int) damage;
				if(transform.parent.transform.localScale.x == 1)
				{
					q = psPlayerHit.transform.rotation;
				} else {
					q = psPlayerHit.transform.rotation * Quaternion.Euler(0,90,0);
				}
				Instantiate(psPlayerHit, coll.transform.position , q);
			} else if (enemy.tag == "Mob") {
				MobScript mobScript = enemy.GetComponent<MobScript>();
				mobScript.Health -= damage;
			}
		}
	}

	void getControls() {
		GetComponentInParent<PlayerController>().PlayerControlNr = GetComponentInParent<PlayerScript> ().PlayerControlNr;
		//get input
		fire1Btn = GetComponentInParent<PlayerController>().Fire1Btn;
		fire1BtnDown = GetComponentInParent<PlayerController>().Fire1BtnDown;
	}

}
