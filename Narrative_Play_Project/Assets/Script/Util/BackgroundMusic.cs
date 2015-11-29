using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public AudioClip musicFst;
	public AudioClip musicSnd;
	public AudioClip musicTrd;
	public AudioClip musicEnd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playMusic(int _act){
		AudioSource ad = gameObject.GetComponent<AudioSource> ();
		switch (_act) {
		case 1:
			ad.clip = musicFst;
			ad.Play();
			break;
		case 2:
			ad.clip = musicSnd;
			ad.Play();
			break;
		case 3:
			ad.clip = musicTrd;
			ad.Play();
			break;
		case 4:
			ad.clip = musicEnd;
			ad.Play();
			break;
		}
	}
}
