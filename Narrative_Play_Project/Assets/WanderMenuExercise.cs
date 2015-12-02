using UnityEngine;
using System.Collections;

public class WanderMenuExercise : MonoBehaviour {
	private float startWanderTime;
	private Vector3 randomDir;
	private bool isStartNew = false;
	
	// Use this for initialization
	void Start () {
		startWanderTime = Time.time;
		toNewPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		//toNewPosition ();
		if (Time.time - startWanderTime >= 0.0f && Time.time - startWanderTime <2.0f) {
			iTween.MoveAdd (gameObject, randomDir*0.2f, 2.0f);
		}
		if (Time.time - startWanderTime >= 3.0f && Time.time - startWanderTime <5.0f) {
			iTween.MoveAdd (gameObject, -randomDir*0.2f, 2.0f);
		}
		if (Time.time - startWanderTime >= 6.0f) {
			toNewPosition();
		}
	}
	
	IEnumerator waitForMovement(float _sec) {
		//print(Time.time);
		yield return new WaitForSeconds (_sec);
		//print(Time.time);
	}
	
	void toNewPosition(){
		//Vector2 circle = Random.insideUnitSphere
		randomDir = Random.insideUnitSphere;
		startWanderTime = Time.time;
		//		startWanderTime = Time.time;
		//		iTween.MoveAdd (gameObject, randomDir*0.5f, 2.0f);
		//		Debug.Log("#Before");
		//		waitForMovement (2.0f);
		//		Debug.Log("#After");
		//		iTween.MoveAdd (gameObject, -randomDir*0.5f, 2.0f);
		//
		//		//transform.Translate( 
		
	}
}
