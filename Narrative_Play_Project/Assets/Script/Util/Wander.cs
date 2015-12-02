using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {
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
			iTween.MoveAdd (gameObject, new Vector3(randomDir.x*0.5f,randomDir.y*0.5f, 0) , 2.0f);
		}
		if (Time.time - startWanderTime >= 3.0f && Time.time - startWanderTime <5.0f) {
			iTween.MoveAdd (gameObject, new Vector3(-randomDir.x*0.5f,-randomDir.y*0.5f, 0), 2.0f);
		}
		if (Time.time - startWanderTime >= 5.0f) {
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
		//Vector3 rotation = new Vector3 (0, 0, Random.Range (-360f, 360f));
		//iTween.RotateAdd (gameObject, rotation, 3.0f);

	}
}
