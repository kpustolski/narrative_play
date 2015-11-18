using UnityEngine;
using System.Collections;

public class SCube : MonoBehaviour {

	// cube id
	public int cID; 

	// text
	public string humanText;
	public string alienText;

	// each cube has an x and y position
	public float x;
	public float y;
	
	public bool isAlien = false;
	
	//camera
	public Camera selectCam1;
	
	//private variables
	private bool showText;
	private string labelText;
	private bool isHumanText;
	
	//Vector3 variables
	Vector3 wHitRot = new Vector3(180, 0, 0);
	//Vector3 sHitRot = new Vector3(-180, 0, 0);
	
	// Use this for initialization
	void Start()
	{
		//init variables
		
		showText = false;
		labelText = "";
		isHumanText = true;
	}

	public void initCube(int _cid, float _posx, float _posy)
	{
		cID = _cid;
		x = _posx;
		y = _posy;
		
		// assign human/alien text 
		// ----- for test 
		humanText = "Cube " + cID.ToString() + ": human side";
		alienText = "Cube " + cID.ToString() + ": alien side";

		// -----TODO: hard code all the story cubes 
		//		switch (cID) {
		//		case 1:
		//			humanText = "Cube 1: human side";
		//			alienText = "Cube 1: alien side";
		//			break;
		//		
		//		}
	}


	// Update is called once per frame
	void Update()
	{
		//print("camera position.x: "+ selectCam1.transform.position.x);
		//print("cube position.x: " + (transform.position.x));
		
		// if there is text, show it
		// else don't show it
		if(labelText != "")
		{
			showText = true;

		}
		else
		{
			labelText = "";
			showText = false;

			// show default text 
		}
		
		// if the camera's x position is the same as the cube's...
		if (selectCam1.transform.position.x == transform.position.x) {

			if(isAlien)
			{
				labelText = alienText;
			}else{
				labelText = humanText;
			}

			/// if the w key is pressed...
			if (Input.GetKeyDown ("w")) {
				// rotate the block 180 degrees in the x direction
				iTween.RotateAdd (gameObject, wHitRot, 0.3f);
				//print ("rotate");
				//print ("Equal positions human");
				isAlien = !isAlien;
				
				// determine whether to show human or alien text. 

			
			} // end keydown w
			// else label text is blank
		} 
		else {
			labelText="";
		}
		
	} // end update
	
	// GUI function
	void OnGUI()
	{
		// if we want the text to show...
		if (showText == true)
		{
			// play with text style
			GUIStyle myStyle = new GUIStyle();
			GUI.color = Color.black;
			myStyle.fontSize = 20;
			//http://answers.unity3d.com/questions/17683/custom-font-in-guilabel-but-cant-change-its-color.html
			myStyle.normal.textColor = Color.white;
			myStyle.fontStyle = FontStyle.Bold;
			
			
			//myStyle.alignment= TextAnchor.MiddleCenter;
			//GUI.backgroundColor=Color.clear;
			// GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
			
			// create text.
			GUI.Label(new Rect(Screen.width/2-50, 10, 100, 20), labelText, myStyle);
			
		}
	}



}
