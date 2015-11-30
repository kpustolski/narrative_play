using UnityEngine;
using System.Collections;

public class FadeMaterial : MonoBehaviour {
	public float fadeTime = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeOut() {
		iTween.ValueTo(gameObject, iTween.Hash(
			"from", 1.0f, "to", 0.0f,
			"time", fadeTime, "easetype", "linear",
			"onupdate", "setAlpha"));
	}

	public void FadeIn() {
		iTween.ValueTo(gameObject, iTween.Hash(
		"from", 0.0f, "to", 1.0f,
			"time", fadeTime, "easetype", "linear",
		"onupdate", "setAlpha"));
	   }

	public void setAlpha(float newAlpha) {
		Debug.Log ("alpha callled");
		Material [] mat = gameObject.GetComponent<Renderer>().materials;
		foreach (Material mObj in mat) {
			mObj.color = new Color(mObj.color.r, mObj.color.g, mObj.color.b, newAlpha);
		}
	}


}
