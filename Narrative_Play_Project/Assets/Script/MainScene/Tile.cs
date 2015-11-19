using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Tile : MonoBehaviour {

	private Color startcolor;
	private Renderer rend;
	private bool isFilled = false;
	public bool isActivated = false; // is the tile activated for placing 
	public int posX;
	public int posY;
	public int tIdx; // tile index 

	public GameObject SCubeR;
	public GameObject SCubeG;
	public GameObject Board;  // get the game board to place cube in it 
	public GameObject SelectWindow;
	public GameObject SelectCamera;
	public GameObject StoryTab;


	public void initTile(int _x, int _y)
	{
		posX = _x;
		posY = _y;
	}



	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		startcolor = rend.material.color;
		Board = GameObject.Find("Grid");
		StoryTab = GameObject.FindWithTag("StoryTab");
		SelectWindow = GameObject.Find ("Selection Cubes");
		SelectCamera = GameObject.Find ("Selection Camera");

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
			//---- For Othello ----//
			// create cube according to cube type 
			// get the current cube type
			GameObject lookAtCube = SelectWindow.GetComponent<SelectManager>().getLookAtCube();
			string lookAtText = "";

			//Debug.Log ("TEST CUBE BOOL : " + lookAtCube.GetComponent<SCube>().isAlien.ToString());

			if(lookAtCube.GetComponent<SCube>().isAlien){
				CloneCube = Instantiate (SCubeG, cposition, transform.rotation) as GameObject;
				CloneCube.GetComponent<StoryCube>().isGreen = true;
				lookAtText = lookAtCube.GetComponent<SCube>().alienText;
			}else{
				CloneCube = Instantiate (SCubeR, cposition, transform.rotation) as GameObject;
				CloneCube.GetComponent<StoryCube>().isGreen = false;
				lookAtText = lookAtCube.GetComponent<SCube>().humanText;
			}
			isFilled = true;
			// update the story cube prefab data 
			CloneCube.GetComponent<StoryCube>().ID = lookAtCube.GetComponent<SCube>().cID;
			CloneCube.GetComponent<StoryCube>().cPosX = gameObject.GetComponent<Tile>().posX;
			CloneCube.GetComponent<StoryCube>().cPosY = gameObject.GetComponent<Tile>().posY;
			CloneCube.GetComponent<StoryCube>().aText = lookAtCube.GetComponent<SCube>().alienText;
			CloneCube.GetComponent<StoryCube>().hText = lookAtCube.GetComponent<SCube>().humanText;


			// register the cube to the board 
			Board.GetComponent<GameBoard>().AddToBoard(CloneCube.GetComponent<StoryCube>());
			Board.GetComponent<GameBoard>().SearchForReverse(CloneCube.GetComponent<StoryCube>());

			// delete the cube from the selection window 
			SelectWindow.GetComponent<SelectManager>().useCube(lookAtCube);


			// add text to the story tab
//			lookAtText = "\n" + lookAtText + "\n";
//			StoryTab.GetComponent<Text>().text += lookAtText;



		} else {
			Debug.Log ("Cube already existed!!");
		}


	}


}
