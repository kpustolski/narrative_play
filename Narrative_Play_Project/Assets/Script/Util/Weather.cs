using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour {
	private AudioSource weatherAudio;
	public AudioClip weatherClip;

	// Use this for initialization
	void Start () {
		weatherAudio = GameObject.Find("Weather").GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeWeather(){
		weatherAudio.clip = weatherClip;
		weatherAudio.Play ();
	}
}
