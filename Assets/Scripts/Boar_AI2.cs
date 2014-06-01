using UnityEngine;
using System.Collections;

public class Boar_AI2 : MonoBehaviour {

	public float speed;	
	public float agroSpeed;
	public float agroDistance;


	private float timerGlobal;
	private float timerWandering = 0;
	private float wanderingPauseTime = 1;
	private float timerAgro = 0;
	private float agroPauseTime = 1;


	private int direction = 1;
	private float rayDistance = 6;

	private GameObject target;

	private enum Action { 
		Wandering,
		Agro,
	}

	private Action action;

	// Use this for initialization
	void Awake (){
	}
	
	// Update is called once per frame
	void Update () {
		Timer ();
		//RaycastCollision ();
		//check for Agro
		if (checkForAgro()){
			action = Action.Agro;
		}
		else{
			action = Action.Wandering;
		}


		caseSwitcher ();
		RaycastCollision ();
		//Debug.Log (timerAgro);
		}



	void caseSwitcher(){
		switch (action){
			case Action.Wandering :
				doWandering ();
				//Debug.Log ("Wandering");
				break;
			case Action.Agro :
				doAgro (target);
				//Debug.Log ("Agro");
				break;
		}
	}


	void doWandering (){
		//Wandering routine

		if (timerWandering > 0){
			//charge
			rigidbody2D.velocity = new Vector2 (speed * direction, 0);
		}
		else{
			if (timerWandering > -wanderingPauseTime){
				//pause wandering
				rigidbody2D.velocity = new Vector2 (0, 0);
				if (timerGlobal <= 0){
					//switch direction while in pause
					direction *= -1;
					timerGlobal = Random.Range (1f, 3f);
				}
			}
			else {
				//set new value timerAgro and agroTimerPause
				timerWandering = Random.Range (1f, 3f);
				wanderingPauseTime = Random.Range (0.5f, 2f);
			}

		}
	}


	void doAgro (GameObject target){
		//Agro routine

		agroSpeed = Mathf.Abs (agroSpeed);
		if (transform.position.x + 0.2 > target.transform.position.x) {
			agroSpeed = -agroSpeed;
		}
		if (timerAgro > 0){
			//charge
			rigidbody2D.velocity = new Vector2 (agroSpeed, 0);
		}
		else{
			if (timerAgro > -agroPauseTime){
				//pause charge
				rigidbody2D.velocity = new Vector2 (0, 0);
			}
			else {
				//set new value timerAgro and agroTimerPause
				timerAgro = Random.Range (0.2f, 0.5f);
				agroPauseTime = Random.Range (0f, 0.3f);
			}
		}
	}


	void RaycastCollision (){
		//Get collision left or right and change directiopn and reset timerGlobal

		RaycastHit2D leftRay = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 1), Vector2.right, -rayDistance); 
		RaycastHit2D rightRay = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 1), Vector2.right, rayDistance); 
		if (leftRay.collider != null && leftRay.rigidbody == null){
			Debug.Log ("COLLISION!!!!");
			direction = 1;
			timerGlobal = Random.Range (1f, 3f);
		}
		if (rightRay.collider != null && leftRay.rigidbody == null){
			Debug.Log ("COLLISION!!!!");
			direction = -1;
			timerGlobal = Random.Range (1f, 3f);
		}


	}



	private bool checkForAgro (){
		//Debug.Log ("CHECK foragro");
		GameObject[] playersAvailable = GameObject.FindGameObjectsWithTag ("Player");
		if (playersAvailable.Length != 0){
			target = findClosestTarget ();
			if (Mathf.Abs(target.transform.position.x - transform.position.x) < agroDistance &&
			    Mathf.Abs(target.transform.position.y - transform.position.y) < 3)
			{
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}


	GameObject findClosestTarget(){
		//Find and return closest Player

		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("Player");
		GameObject closestTarget = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject tar in targets) {
			Vector3 diff = tar.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
					closestTarget = tar;
					distance = curDistance;
			}
		}
		return closestTarget;
	}


	void Timer() {
		//Function for substracting timer per deltaTime

		if (timerGlobal > 0){
			timerGlobal -= Time.deltaTime;
		}
		if (timerAgro > - agroPauseTime){
			timerAgro -= Time.deltaTime;
		}
		if (timerWandering > - wanderingPauseTime){
			timerWandering-= Time.deltaTime;
		}
	}
}
		
