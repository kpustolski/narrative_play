using UnityEngine;
using System.Collections;

public class select : MonoBehaviour {

	public Color colorStart;
	public Color colorEnd;
	public float duration = 1.0F;
	public Renderer rend;

	void Start() {
		rend = GetComponent<MeshRenderer>();
	}

	void OnMouseEnter() {
		Debug.Log ("Change color");
		rend.material.color = colorEnd;
	}

	void OnMouseExit(){
		Debug.Log ("Change back");
		rend.material.color = colorStart;
	}
}
