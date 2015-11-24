using UnityEngine;
using System.Collections;

public class StoryItem : MonoBehaviour {
	public bool isAlien;
	private AudioSource AS;

	// Use this for initialization
	void Start () {
		isAlien = true;
		gameObject.GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void rotateAnimation(){
		iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 1.0f);
	}


	public void playAudio(){
		Debug.Log("PlayAduio");

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log("Click and Rotate");
			isAlien = !isAlien;
			rotateAnimation();
		}

		playAudio ();
	}


}
