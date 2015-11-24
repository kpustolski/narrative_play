using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NodeCell{
	// a cell in the board contains information about a certain story cube and the position of it 
	public int cellCubeId; // the cell of the cube 
	public bool isActivedOnce; 
	public bool isActive;// 1: the node is visible 0: the node is not visible 
	public int x;
	public int y;
	public int side; // 0: no item | 1: human side | 2: alien side
	public GameObject node;  // clone of the node prefab
	// the story object
	// public StoryItem item;
		
	// construction function 
	public NodeCell(int _px, int _py){
		x = _px;
		y = _py;
		isActive = false;
		isActivedOnce = false;
		side = 0;
	}
}


public struct Index{
	public int x;
	public int y;
	public Index(int _x, int _y){
		x = _x;
		y = _y;
	}
}

// network 
public class Network : MonoBehaviour {

	public Transform startPosition; // the start position of the network
	public GameObject nodePrefab;
	public GameObject vertexPrefab;
	public int nHeight = 5;
	public int nWidth = 5;
	[SerializeField] float nodeStepW = 2.0f; // the distance between two nodes
	[SerializeField] float nodeStepH = 1.7f;
	public int alienCount = 0; // number of alien objects 
	public int humanCount = 0; // number of human objects 
	public int totalCount = 0; // number of total objects 


	private List<Node> nodes;        // current cubes on the board 
	private List<NodeCell> nodesToFlip;  // node to be flipped 
	public NodeCell [][] cells;    // cell information 

	// Use this for initialization
	void Start () {
		// initialize the network 
		nodes = new List<Node> ();
		nodesToFlip = new List<NodeCell> ();
	
		cells = new NodeCell[nWidth][];
		for (int i = 0; i<nWidth; i++) {
			cells[i] = new NodeCell[nHeight];
		}

		Index middle = new Index((int)(Mathf.Floor (nWidth / 2)), (int)Mathf.Floor (nHeight / 2));
		for (int i = 0; i<nWidth; i++) {
			for (int j = 0; j<nHeight; j++)
			{
				cells[i][j] = new NodeCell(i,j);
				// activate the middle node
				if(i == middle.x && j == middle.y && !cells[i][j].isActive)
				{
					cells[i][j].isActive = true;
				}
			}
		} 
	}
	

	// generate a new node
	// called when new node position are allowed on the board 
	public GameObject generateNode(int _id, Vector3 _position, int _i, int _j){
		GameObject node = Instantiate(nodePrefab, _position, transform.rotation) as GameObject;
		node.transform.parent = startPosition.parent;
		node.GetComponent<Node>().isFilled = false;
		node.GetComponent<Node>().nodeID = _id;
		node.GetComponent<Node> ().nodeIdx = new Index (_i, _j);
		return node;
	}

	// check if new nodes are added to the current board 
	public void checkForActivation(){
		for (int i = 0; i<nWidth; i++) {
			for (int j = 0; j<nHeight; j++)
			{
				if(cells[i][j].isActive && !cells[i][j].isActivedOnce)
				{
					Vector3 nodePos = startPosition.position;
					nodePos.x += nodeStepW * (float)i;
					nodePos.y += nodeStepH * (float)j;
					cells[i][j].node = generateNode(i*nWidth + j, nodePos, i, j);
					cells[i][j].isActivedOnce = true;
				}
			}
		} 
	}


	// check if the cell need to be flipped 
	// search for four directions instead of eight 
	// animation + voice over  
	void searchForReverse(){


	}

	// refresh cell information when the items are flipped 
	void refreshCells(){
		
	}

	void flipItems (){

	}

	
	// Update is called once per frame
	void Update () {

		// 1. check the network for unfolding the board 
		checkForActivation ();
	
	}
}
