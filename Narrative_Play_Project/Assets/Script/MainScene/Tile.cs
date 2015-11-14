using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour {

	private Color startcolor;
	private Renderer rend;
	private bool isFilled = false;
	public int posX;
	public int posY;
	public int tIdx; // tile index 

	public GameObject SCubeR;
	public GameObject SCubeG;
	public GameObject Board;  // get the game board to place cube in it 


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
			//---- For Othello Test ----//
			// TODO: create cube according to cube type 
			// get the current cube type
			if(Input.GetKey(KeyCode.G)){
				CloneCube = Instantiate (SCubeG, cposition, transform.rotation) as GameObject;
				CloneCube.GetComponent<StoryCube>().isGreen = true;
			}else{
				CloneCube = Instantiate (SCubeR, cposition, transform.rotation) as GameObject;
				CloneCube.GetComponent<StoryCube>().isGreen = false;
			}
			isFilled = true;
			// update the story cube prefab data 
			CloneCube.GetComponent<StoryCube>().ID = gameObject.GetComponent<Tile>().tIdx;
			CloneCube.GetComponent<StoryCube>().cPosX = gameObject.GetComponent<Tile>().posX;
			CloneCube.GetComponent<StoryCube>().cPosY = gameObject.GetComponent<Tile>().posY;

			// regiter the cube to the board 
			Board.GetComponent<GameBoard>().AddToBoard(CloneCube.GetComponent<StoryCube>());
			Board.GetComponent<GameBoard>().SearchForReverse(CloneCube.GetComponent<StoryCube>());
		} else {
			Debug.Log ("Cube already existed!!");
		}





		// travers the list and figure out the flipping 

	}


}
