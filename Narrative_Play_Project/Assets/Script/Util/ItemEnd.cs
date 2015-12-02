using UnityEngine;
using System.Collections;

public class ItemEnd : MonoBehaviour {
	public bool isFlip = false;
	public bool isActive = false;
	public Transform finalPos;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			iTween.MoveTo(gameObject, finalPos.transform.position, 5.0f);
		}


	
	}

	void rotateAnimation(){

		iTween.RotateAdd (gameObject, iTween.Hash("amount",new Vector3(180f,0,0), "time", 1.0f, "oncomplete", "flipBoolean", "onstart", "playClickAudio"));
		//		Debug.Log("After Itween");
		
	}

	void flipBoolean(){
		isFlip = !isFlip;
	}

	void OnMouseOver(){
		if (Input.GetAxis("Mouse ScrollWheel")!=0) {
			Debug.Log("Scroll and Rotate");
			//isAlien = !isAlien;

			rotateAnimation();
			//playAudio ();
		}
	}

	void OnMouseUp(){
		if (!isFlip) {
			Application.LoadLevel (2);
		} else {
			Application.Quit();
		}
	}


}
