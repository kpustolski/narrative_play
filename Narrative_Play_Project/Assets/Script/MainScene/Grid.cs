using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	private Color startcolor;
	private Renderer rend;
	private bool isFilled = false;
	public GameObject SCubePrefab;


	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		startcolor = rend.material.color;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// highlight the cube 
	void OnMouseEnter()
	{
		//renderer.material.color = Color.yellow;
		rend.material.color = Color.yellow;
	}
	void OnMouseExit()
	{
		rend.material.color = startcolor;
	}

	// select grid cell and place the cube 
	// TODO: generate cube according to the cube index in the right position 
	void OnMouseDown(){
		if (!isFilled) {
			Debug.Log ("Place a cube!!");
			GameObject CloneCube;
			Vector3 cposition = gameObject.transform.position;
			cposition.y += 4.0F;
			CloneCube = Instantiate (SCubePrefab, cposition, transform.rotation) as GameObject;
			isFilled = true;
		} else {
			Debug.Log ("Cube already existed!!");
		}

	}
}
