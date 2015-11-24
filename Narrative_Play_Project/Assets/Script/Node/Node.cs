using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Node : MonoBehaviour {

	// Each Node 
	// Collider: detection the drag and drop function 

	// boolean values 
	public bool isFilled; // whether the node is filled
	public bool isAlien; // 
	public GameObject storyItm; // the story item put inside that cell 
	
	public int nodeID = 0;
	public Index nodeIdx;
	public List<Vertex> vertice;
	public int verticeMax; // max number of vertice exh node allow to have 
	public int verticeNum; // vertice number attacshed to the current node

	[SerializeField] float wanderRange = 0.0f; // random number for the node to float around 
	[SerializeField] float wanderMax = 1.0f;
	[SerializeField] float wanderMin = 1.0f;

	private GameObject net;

	// Use this for initialization
	void Start () {
		net = GameObject.FindGameObjectWithTag("network");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// add one vertex to the list of vertice 
	public void registerVertex(Vertex _vtx){
		if (verticeNum <= verticeNum) {
			vertice.Add(_vtx);
		} else {
			return;
		}
	}	

	public void wanderNodeRandom(){
		//TODO: using code or animation ??
	}

	// when clicked on the node, the node creates 4 adjacent new positions
	public void activateMore(){
		if (!isFilled) {
			Debug.Log ("More nodes");
			if(nodeIdx.x-1>=0)
			{
				net.GetComponent<Network>().cells[nodeIdx.x-1][nodeIdx.y].isActive = true;
			}
			
			if(nodeIdx.x+1<5)
			{
				net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].isActive = true;
			}
			
			if(nodeIdx.y-1>=0)
			{
				net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].isActive = true;
			}
			
			if(nodeIdx.y+1<5)
			{
				net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].isActive = true;
			}
			
		}
		isFilled = true;
	}

	// when place an item on the node, register the item to the network 
	public void addItem(GameObject _itm){
		net.GetComponent<Network> ().addItemToCell (nodeIdx.x, nodeIdx.y, _itm);

		
	}

	void OnMouseDown(){
		//activateMore ();

	}




}
