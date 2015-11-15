using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Implement the Othello Game Control 
// Things need to be done: 
// ## 1. add cubes to the data struture: the cubes on the board || the cubes need to be flipped 
// ## 2. search for the reverse cubes 
// ## 3. 

public struct cellPos{
	public int cx;
	public int cy;
	public cellPos(int x, int y)
	{
		cx = x;
		cy = y;
	}
}

public class boardCell{
	// a cell in the board contains information about a certain story cube and the position of it 
	public int cellCubeId; // the cell of the cube 
	public int x;
	public int y;
	public int side = 0;
	public StoryCube cube;



	public boardCell(StoryCube _cube)
	{
		cellCubeId = _cube.ID;
		x = _cube.cPosX;
		y = _cube.cPosY;
		if (_cube.isGreen) {
			side = 2; // green cube
		} else {
			side = 1; // red cube
		}
		cube = _cube;
	}

}


public class GameBoard : MonoBehaviour {

	public Material red;
	public Material green;

	public Transform BoardCenter;
	public GameObject Tile;
	public int SizeH;
	public int SizeW;
	private float TileSize;

	private List<StoryCube> CubesOnBoard;  // current cubes on the board 
	private List<StoryCube> CubesToFlip;
	private boardCell [][] cells;    // cell information 


	// Use this for initialization
	void Awake(){
		Debug.Log ("AWAKE!!");
	
	}

	void Start () {
		Debug.Log ("Start!!");
		TileSize = 10.0f;
		CubesOnBoard = new List<StoryCube> ();
		CubesToFlip = new List<StoryCube> ();

	
		// init board cell data 
		cells = new boardCell[SizeW][];
		for (int i = 0; i<SizeW; i++) {
			cells[i] = new boardCell [SizeH];
		}
		CubesToFlip.Clear ();
		// init game board (Tiles)
		GenerateBoard ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	// ## generate board 
	// 
	public void GenerateBoard(){
		Vector3 tpos = BoardCenter.position;
		GameObject tileClone;
		tpos.x = BoardCenter.position.x - Mathf.Floor (SizeW / 2) * TileSize;
		tpos.z = BoardCenter.position.z - Mathf.Floor (SizeH / 2) * TileSize;
		int i = 0;
		int j = 0;

		for (i=0; i < SizeW; i++) 
		{
			for (j=0; j < SizeH; j++)
			{
				Vector3 position;
				float pz = tpos.z + TileSize * j;
				float px = tpos.x + TileSize * i;
				position.z = pz;
				position.x = px;
				position.y = tpos.y;
				// generate a new tile
				tileClone = Instantiate (Tile, position, transform.rotation) as GameObject;
				tileClone.transform.parent = gameObject.transform;
//				tileClone.GetComponent<Tile>().posX = i;
//				tileClone.GetComponent<Tile>().posY = j;
				tileClone.GetComponent<Tile>().initTile(i,j);
				tileClone.GetComponent<Tile>().tIdx = i*SizeW + j;

			}
			
		}
	}



	// ### add a cube to the onboard list 
	// param: cube data + tile data 
	public void AddToBoard(StoryCube _cube){
		Debug.Log ("Add Cube info to current board!!");
		//boardCell ncell = new boardCell (_tile.posX, _tile.posY, _cube);
		// First check if the item is in the bp already
		if (CheckExisted (_cube)) {
			Debug.Log ("ERROR!! Cube already existed!!");
		} else {
			Debug.Log ("Adding cube " + _cube.ID);
			CubesOnBoard.Add(_cube);
			RefreshCell();

		}
	}

	public bool CheckExisted(StoryCube _cube)
	{
		return false;
	}

	// ## delete a cube from the onboard list
	public void RemoveFromBoard(StoryCube _cube)
	{
		Debug.Log ("Deleting cube " + _cube.ID);
		//CubesOnBoard.Remove (_cube);
	}

	// ## update board cell data 
	public void RefreshCell()
	{
		foreach (StoryCube sc in CubesOnBoard) {
			int i = sc.cPosX;
			int j = sc.cPosY;
			cells[i][j] = new boardCell(sc);
		}

	}

	
	// search the cubes on the board for Flipping 
	public void SearchForReverse(StoryCube _cube){
		Debug.Log("Search for flip");
		// clear the reverse cube array


		// -- search the current position -- //
		// ## search for different color 
		// ## search for same color 
		// ## all the cubes in between are added to the flip array 
		int _posX = _cube.cPosX;
		int _posY = _cube.cPosY;
		// -- 1. search horizontally
		// searching along +x
		for (int i = _posX + 1; i < SizeW; i++) {
			if(cells[i][_posY] == null)
			{
				Debug.Log("+x is null");
				break;
			}

			if(cells[i][_posY] != null && cells[_posX][_posY].side == cells[i][_posY].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						CubesToFlip.Add(cells[j][_posY].cube);
					}
					break;

				}else{
					break;
				}


			}
		
		}

		// searching along -x
		for (int i = _posX - 1; i >=0; i--) {
			if(cells[i][_posY] == null)
			{
				Debug.Log("-x is null");
				break;
			}
			
			if(cells[i][_posY] != null && cells[_posX][_posY].side == cells[i][_posY].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						CubesToFlip.Add(cells[j][_posY].cube);
					}
					break;
					
				}else{
					break;
				}
				
			}
			
		}

		// -- 2. search vertically
		// search along +y
		for (int j = _posY + 1; j < SizeH; j++) {
			if(cells[_posX][j] == null)
			{
				Debug.Log("+y is null");
				break;
			}
			
			if(cells[_posX][j] != null && cells[_posX][_posY].side == cells[_posX][j].side)
			{
				if(j-_posY > 1)
				{
					for(int i = j-1; i>_posY; i--)
					{
						CubesToFlip.Add(cells[_posX][i].cube);
					}
					break;
					
				}else{
					break;
				}

			}
			
		}

		// search along -y
		for (int j = _posY - 1; j >= 0; j--) {
			if(cells[_posX][j] == null)
			{
				Debug.Log("-y is null");
				break;
			}
			
			if(cells[_posX][j] != null && cells[_posX][_posY].side == cells[_posX][j].side)
			{
				if(_posY - j > 1)
				{
					for(int i = j+1; i<_posY; i++)
					{
						CubesToFlip.Add(cells[_posX][i].cube);
					}
					break;
					
				}else{
					break;
				}
				
			}
			
		}

		// -- 3. search diagnally
		// searching along +x, +y
		int offset = _posY - _posX;
		for (int i = _posX + 1; i < SizeW && i+offset < SizeH; i++) {
			if(cells[i][i+offset] == null)
			{
				Debug.Log("+x +y is null");
				break;
			}
			
			if(cells[i][i+offset] != null && cells[_posX][_posY].side == cells[i][i+offset].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						CubesToFlip.Add(cells[j][j+offset].cube);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}

		// searching along -x, -y
		for (int i = _posX - 1; i >= 0 && i+offset >= 0; i--) {
			if(cells[i][i+offset] == null)
			{
				Debug.Log("-x -y is null");
				break;
			}
			
			if(cells[i][i+offset] != null && cells[_posX][_posY].side == cells[i][i+offset].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						CubesToFlip.Add(cells[j][j+offset].cube);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}

		offset = _posX + _posY;
		// searching along +x, -y
		for (int i = _posX + 1; i < SizeW && offset-i >= 0; i++) {
			if(cells[i][offset-i] == null)
			{
				Debug.Log("+x -y is null");
				break;
			}
			
			if(cells[i][offset-i] != null && cells[_posX][_posY].side == cells[i][offset-i].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						CubesToFlip.Add(cells[j][offset-j].cube);
					}
					break;

				}else{
					break;
				}
				
				
			}
			
		}

		// searching along -x, +y
		for (int i = _posX - 1; i >= 0 && offset - i < SizeH; i--) {
			if(cells[i][offset-i] == null)
			{
				Debug.Log("-x +y is null");
				break;
			}
			
			if(cells[i][offset-i] != null && cells[_posX][_posY].side == cells[i][offset-i].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						CubesToFlip.Add(cells[j][offset-j].cube);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}

		FlipCubes ();
	}

	public void FlipCubes(){
		// just change the color of the cubes for now 
		Debug.Log ("FLIP!!!!!!!!!");
		foreach (StoryCube sc in CubesToFlip) {
			if(!sc.isGreen)
			{
				sc.GetComponent<Renderer>().material = green;
			}else
			{
				sc.GetComponent<Renderer>().material = red;
			}

			sc.isGreen = !sc.isGreen;
		}

		RefreshCell();
		CubesToFlip.Clear();
	}


}
