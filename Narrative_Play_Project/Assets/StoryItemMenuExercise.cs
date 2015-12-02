using UnityEngine;
using System.Collections;

public class StoryItemMenuExercise : MonoBehaviour {
	public Vector3 axis = new Vector3(1f, 0, 0);
	private Vector3 amount = new Vector3 (180f, 180f, 180f);
	public bool isAlien;
	public AudioClip clickAudio;
	public AudioClip humanAudio;
	public AudioClip alienAudio;
	private AudioSource AS;
	public bool isPlaced;
	public float originalAngle;
	private bool isback = true;
	
	
	
	// Use this for initialization
	void Start () {
		isAlien = true;
		isPlaced = false;
		gameObject.AddComponent<AudioSource> ();
		AS = gameObject.GetComponent<AudioSource> ();
		AS.loop = false;
		AS.playOnAwake = false;
		AS.volume = 1;
		//AS.isPlaying = false;
		amount = axis * 180.0f;
		originalAngle = transform.rotation.eulerAngles.z;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AS.clip != clickAudio && AS.isPlaying) {
			isback = false;
			clickAnimation();
		}
		if (AS.clip != clickAudio && AS.clip != null && !AS.isPlaying && !isback) {
			iTween.RotateAdd (gameObject,new Vector3(0,0,originalAngle-transform.localRotation.eulerAngles.z), 1.0f);
			isback = true;
		}
		
		
		
		
	}
	
	
	public void rotateAnimation(){
		//isAlien = !isAlien;
		//iTween.RotateAdd (gameObject, new Vector3(180.0f, 0, 0), 0.0f);
		iTween.RotateAdd (gameObject, iTween.Hash("amount", amount, "time", 1.0f, "oncomplete", "flipBoolean", "onstart", "playClickAudio"));
		//		Debug.Log("After Itween");
		
	}
	
	public void flipAnimation(){
		isAlien = !isAlien;
		iTween.RotateAdd (gameObject, amount, 1.0f);
		
	}
	
	public void clickAnimation(){
		//Debug.Log("WHY!!!" + AS.clip.length);
		float speed = 1080.0f / (AS.clip.length * 60);
		gameObject.transform.Rotate (new Vector3 (0,0,1f), speed);
		//iTween.RotateAdd (gameObject, new Vector3(0,0,speed*Time.deltaTime), 0f);
		
		
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
		if (!AS.isPlaying) {
			AS.Play ();
		}
		
	}
	
	public void stopAudio(){
		if (AS.isPlaying) {
			AS.Stop ();
		}
	}
	
	
	
	void flipBoolean(){
		Debug.Log("Flip isAlien");
		isAlien = !isAlien;
	}
	
	// need to get the color and boolean value consistent in flipping 
	void OnMouseOver(){
		if (Input.GetAxis("Mouse ScrollWheel")!=0 && !isPlaced) {
			Debug.Log("Scroll and Rotate");
			//isAlien = !isAlien;
			rotateAnimation();
			//playAudio ();
		}
		
		
	}
	
	
}
