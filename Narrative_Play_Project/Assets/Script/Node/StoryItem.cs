using UnityEngine;
using System.Collections;

public class StoryItem : MonoBehaviour {
	public bool isAlien;
	public AudioClip clickAudio;
	public AudioClip humanAudio;
	public AudioClip alienAudio;
	private AudioSource AS;
	public bool isPlaced;



	// Use this for initialization
	void Start () {
		isAlien = true;
		isPlaced = false;
		gameObject.AddComponent<AudioSource> ();
		AS = gameObject.GetComponent<AudioSource> ();
		AS.loop = false;
		AS.playOnAwake = false;
		AS.volume = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void rotateAnimation(){
		//isAlien = !isAlien;
		//iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 0.0f);
		iTween.RotateAdd (gameObject, iTween.Hash("amount", new Vector3(180.0f, 0, 0), "time", 1.0f, "oncomplete", "flipBoolean", "onstart", "playClickAudio"));
//		Debug.Log("After Itween");



	}

	public void flipAnimation(){
		isAlien = !isAlien;
		iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 1.0f);

	}

	void playClickAudio(){
		AS.clip = clickAudio;
		AS.Play ();
	}

	public void playAudio(){
		Debug.Log("PlayAduio");
		if (isAlien) {
			AS.clip = alienAudio;
		} else {
			AS.clip = humanAudio;
		}
		AS.Play ();
	}



	void flipBoolean(){
		Debug.Log("Flip isAlien");
		isAlien = !isAlien;
	}

	// need to get the color and boolean value consistent in flipping 
	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1) && !isPlaced) {
			Debug.Log("Click and Rotate");
			//isAlien = !isAlien;
			rotateAnimation();
			//playAudio ();
		}


	}


}
