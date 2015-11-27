﻿using UnityEngine;
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
	public GameObject item;  // item on that cell 
	// the story object
	// public StoryItem item;
		
	// construction function 
	public NodeCell(int _px, int _py){
		x = _px;
		y = _py;
		isActive = false;
		isActivedOnce = false;
		side = 0;
		node = null;
		item = null;
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

	[SerializeField] int ACT_FST = 5;
	[SerializeField] int ACT_SND = 10;
	[SerializeField] int ACT_TIR = 14;

	[SerializeField] GameObject bckFst;
	[SerializeField] GameObject bckSnd;
	[SerializeField] GameObject bckTrd;
	[SerializeField] bool isEnterFst = false;
	[SerializeField] bool isEnterSnd = false;
	[SerializeField] bool isEnterTrd = false;


	private GameObject [] stage1;
	private GameObject [] stage2;
	private GameObject [] stage3;
	
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

		// initialize the Environment 
		initItems ();
		enterAct (1);


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

	// add item to a specific cell 
	public void addItemToCell(int i,int j, GameObject _itm){
		cells[i][j].item = _itm;
		if (_itm.GetComponent<StoryItem> ().isAlien) {
			Debug.Log("ADD Alien");
			cells [i] [j].side = 2;
		} else {
			Debug.Log("ADD Human");
			cells [i] [j].side = 1;
		}
	}


	// check if the cell need to be flipped 
	// search for four directions instead of eight 
	// animation + voice over  
	public void searchForReverse(Node _node){
		Debug.Log("Search for flip");

		int _posX = _node.nodeIdx.x;
		int _posY = _node.nodeIdx.y;
		// -- 1. search horizontally
		// searching along +x
		for (int i = _posX + 1; i < nWidth; i++) {
			if(cells[i][_posY].side == 0)
			{
				//Debug.Log("+x is null");
				break;
			}
			
			if(cells[i][_posY].side != 0 && cells[_posX][_posY].side == cells[i][_posY].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						nodesToFlip.Add(cells[j][_posY]);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}
		
		// searching along -x
		for (int i = _posX - 1; i >=0; i--) {
			if(cells[i][_posY].side == 0)
			{
				//Debug.Log("-x is null");
				break;
			}
			
			if(cells[i][_posY].side != 0 && cells[_posX][_posY].side == cells[i][_posY].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						nodesToFlip.Add(cells[j][_posY]);
					}
					break;
					
				}else{
					break;
				}
				
			}
			
		}
		
		// -- 2. search vertically
		// search along +y
		for (int j = _posY + 1; j < nHeight; j++) {
			if(cells[_posX][j].side == 0)
			{
				//Debug.Log("+y is null");
				break;
			}
			
			if(cells[_posX][j].side != 0 && cells[_posX][_posY].side == cells[_posX][j].side)
			{
				if(j-_posY > 1)
				{
					for(int i = j-1; i>_posY; i--)
					{
						nodesToFlip.Add(cells[_posX][i]);
					}
					break;
					
				}else{
					break;
				}
				
			}
			
		}
		
		// search along -y
		for (int j = _posY - 1; j >= 0; j--) {
			if(cells[_posX][j].side == 0 )
			{
				//Debug.Log("-y is null");
				break;
			}
			
			if(cells[_posX][j].side != 0 && cells[_posX][_posY].side == cells[_posX][j].side)
			{
				if(_posY - j > 1)
				{
					for(int i = j+1; i<_posY; i++)
					{
						nodesToFlip.Add(cells[_posX][i]);
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
		for (int i = _posX + 1; i < nWidth && i+offset < nHeight; i++) {
			if(cells[i][i+offset].side == 0)
			{
				//Debug.Log("+x +y is null");
				break;
			}
			
			if(cells[i][i+offset].side != 0 && cells[_posX][_posY].side == cells[i][i+offset].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						nodesToFlip.Add(cells[j][j+offset]);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}
		
		// searching along -x, -y
		for (int i = _posX - 1; i >= 0 && i+offset >= 0; i--) {
			if(cells[i][i+offset].side == 0)
			{
				//Debug.Log("-x -y is null");
				break;
			}
			
			if(cells[i][i+offset].side != 0 && cells[_posX][_posY].side == cells[i][i+offset].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						nodesToFlip.Add(cells[j][j+offset]);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}
		
		offset = _posX + _posY;
		// searching along +x, -y
		for (int i = _posX + 1; i < nWidth && offset-i >= 0; i++) {
			if(cells[i][offset-i].side == 0)
			{
				//Debug.Log("+x -y is null");
				break;
			}
			
			if(cells[i][offset-i].side != 0 && cells[_posX][_posY].side == cells[i][offset-i].side)
			{
				if(i-_posX > 1)
				{
					for(int j = i-1; j>_posX; j--)
					{
						nodesToFlip.Add(cells[j][offset-j]);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}
		
		// searching along -x, +y
		for (int i = _posX - 1; i >= 0 && offset - i < nHeight; i--) {
			if(cells[i][offset-i].side == 0)
			{
				//Debug.Log("-x +y is null");
				break;
			}
			
			if(cells[i][offset-i].side != 0 && cells[_posX][_posY].side == cells[i][offset-i].side)
			{
				if(_posX-i > 1)
				{
					for(int j = i+1; j<_posX; j++)
					{
						nodesToFlip.Add(cells[j][offset-j]);
					}
					break;
					
				}else{
					break;
				}
				
				
			}
			
		}
	
		
		flipItems ();


	}


	// refresh cell information when the items are flipped 
	void refreshCells(){
		nodesToFlip.Clear ();
		alienCount = 0;
		humanCount = 0;
		for (int i = 0; i<nWidth; i++) {
			for (int j = 0; j<nHeight; j++)
			{
				if(cells[i][j].side != 0)
				{
					if(cells[i][j].item.GetComponent<StoryItem>().isAlien)
					{
						alienCount += 1;
						cells[i][j].side = 2;

					}else{
						humanCount += 1;
						cells[i][j].side = 1;
					}
				}
			}
		} // end of for
		totalCount = alienCount + humanCount;
		Debug.Log ("Red : " + humanCount + " Green: " + alienCount);
	}

	void flipItems (){
		foreach (NodeCell nc in nodesToFlip) {
			//nc.item.GetComponent<StoryItem>().isAlien = !nc.item.GetComponent<StoryItem>().isAlien;
			nc.item.GetComponent<StoryItem>().flipAnimation();
		}
		// need to refresh cell after all the itween animation are done 
		refreshCells ();
		checkForAct ();
	}

	// check head position according to the difference of alien human obj
	void checkHeadPosition(int _diff)
	{
		
	}

	// check for the progression of acts 
	void checkForAct(){
		if (totalCount == ACT_FST && !isEnterFst) {
			Debug.Log("Enter Second Act");
			enterAct(2);
			isEnterFst = true;
						
		}

		if (totalCount == ACT_SND && !isEnterSnd) {
			Debug.Log("Enter Third Act");
			enterAct(3);
			isEnterSnd = true;	
		}

		if (totalCount == ACT_TIR && !isEnterTrd) {
			Debug.Log("Enter Endding");
			isEnterTrd = true;
			
		}
	}

	// enter act with the act index 
	// TODO: for each act 
	// 1) changing back ground : Fade in and out effects 
	// 2) init objects position 
	void enterAct(int _actIdx){
		if (_actIdx == 1) {
			clearBackgroud();
			bckFst.GetComponent<FadeMaterial> ().FadeIn();
			foreach (GameObject itm in stage1) {
				itm.GetComponent<FadeMaterial>().FadeIn();
			}
		}

		if (_actIdx == 2) {
			//clearBackgroud();
			bckFst.GetComponent<FadeMaterial>().FadeOut();
			bckSnd.GetComponent<FadeMaterial>().FadeIn();
			foreach (GameObject itm in stage2) {
				itm.GetComponent<FadeMaterial>().FadeIn();
			}

		}

		if (_actIdx == 3) {
			//clearBackgroud();
			bckSnd.GetComponent<FadeMaterial>().FadeOut();
			bckTrd.GetComponent<FadeMaterial>().FadeIn();
			foreach (GameObject itm in stage3) {
				itm.GetComponent<FadeMaterial>().FadeIn();
			}
		}
	}

	// Initialize Item positions according to act index
	void initItems(){
		stage1 = GameObject.FindGameObjectsWithTag ("Act1");
		stage2 = GameObject.FindGameObjectsWithTag ("Act2");
		stage3 = GameObject.FindGameObjectsWithTag ("Act3");
		foreach (GameObject itm in stage1) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
		}
		foreach (GameObject itm in stage2) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
		}
		foreach (GameObject itm in stage3) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
		}
	}

	// show ending 

	// set background to black 
	void clearBackgroud(){
//		bckFst.SetActive(false);
//		bckSnd.SetActive(false);
//		bckTrd.SetActive(false);
		bckFst.GetComponent<FadeMaterial> ().setAlpha (0.0f);
		bckSnd.GetComponent<FadeMaterial> ().setAlpha (0.0f);
		bckTrd.GetComponent<FadeMaterial> ().setAlpha (0.0f);


	}

	
	// Update is called once per frame
	void Update () {

		// 1. check the network for unfolding the board 
		checkForActivation ();

	}
}
