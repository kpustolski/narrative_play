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
		//isAlien = !isAlien;
		//iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 0.0f);
		iTween.RotateAdd (gameObject, iTween.Hash("amount", new Vector3(180.0f, 0, 0), "time", 1.0f, "oncomplete", "flipBoolean"));
//		Debug.Log("After Itween");

	}

	public void flipAnimation(){
		isAlien = !isAlien;
		iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 1.0f);
	}


	public void playAudio(){
		Debug.Log("PlayAduio");

	}

	void flipBoolean(){
		Debug.Log("Flip isAlien");
		isAlien = !isAlien;
	}

	// need to get the color and boolean value consistent in flipping 
	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log("Click and Rotate");
			//isAlien = !isAlien;
			rotateAnimation();
			playAudio ();
		}


	}


}
