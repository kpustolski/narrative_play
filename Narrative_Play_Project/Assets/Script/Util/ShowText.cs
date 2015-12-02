using UnityEngine;
using System.Collections;

public class ShowText : MonoBehaviour {
	public GameObject overHumanTxt;
	public GameObject overAlienTxt;
	private bool isHover = false;

	// Use this for initialization
	void Start () {
//		overHumanTxt.SetActive (false);
//		overAlienTxt.SetActive (false);
		overAlienTxt.GetComponent<FadeMaterial> ().setAlpha (0);
		overHumanTxt.GetComponent<FadeMaterial> ().setAlpha (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		if (!isHover) {
			overHumanTxt.GetComponent<FadeMaterial> ().FadeIn();
			overAlienTxt.GetComponent<FadeMaterial> ().FadeIn();
			isHover = true;
		}

	}

	void OnMouseExit(){
		if (isHover) {
			overHumanTxt.GetComponent<FadeMaterial> ().FadeOut();
			overAlienTxt.GetComponent<FadeMaterial> ().FadeOut();
			isHover = false;
		}

	}


}
