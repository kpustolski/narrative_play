using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Node : MonoBehaviour {
	// boolean values 
	public bool isFilled; // whether the node is filled
	public bool isAlien; // 
	public GameObject storyItm; // the story item put inside that cell 
	
	public int nodeID = 0;
	public Index nodeIdx;
	public List<GameObject> vertice;
	public int verticeMax; // max number of vertice exh node allow to have 
	public int verticeNum; // vertice number attacshed to the current node

	[SerializeField] float wanderRange = 0.0f; // random number for the node to float around 
	[SerializeField] float wanderMax = 1.0f;
	[SerializeField] float wanderMin = 1.0f;

	private GameObject net;

	private int [][] connection;
	[SerializeField] int width = 5;
	[SerializeField] int height = 5;

	// Use this for initialization
	void Awake(){
		initConnections ();
	}

	void Start () {
		net = GameObject.FindGameObjectWithTag("network");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// inti the connection array 
	void initConnections(){
		connection = new int[width][];
		for (int i = 0; i<width; i++) {
			connection[i] = new int[height];
		}
		
		for (int i = 0; i<width; i++) {
			for (int j = 0; j<height; j++)
			{
				connection[i][j] = 0;
			}
		} 
	}

	// add one vertex to the list of vertice 
	public void registerVertex(GameObject _vtx){
		if (verticeNum <= verticeNum) {
			vertice.Add(_vtx);
		} else {
			return;
		}
	}	

	// when clicked on the node, the node creates 4 adjacent new positions
	public void activateMore(){
		if (!isFilled) {
			Debug.Log ("More nodes");
			 
			if(nodeIdx.x-1>=0)
			{
				if(!net.GetComponent<Network>().cells[nodeIdx.x-1][nodeIdx.y].isActive){
					net.GetComponent<Network>().cells[nodeIdx.x-1][nodeIdx.y].isActive = true;
					net.GetComponent<Network>().checkForActivation();
					// add the current node and the next node as the two end points for the vertex
				}

				if(connection[nodeIdx.x-1][nodeIdx.y] == 0)
				{
					connection[nodeIdx.x-1][nodeIdx.y] = 1;
					GameObject nd = net.GetComponent<Network>().cells[nodeIdx.x-1][nodeIdx.y].node;
					//node.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					nd.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					GameObject vtx = createVertex(nd, false);
					vertice.Add(vtx);
				}


			}
			
			if(nodeIdx.x+1<width)
			{
				//net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].isActive = true;
				if(!net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].isActive){
					net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].isActive = true;
					net.GetComponent<Network>().checkForActivation();
					// add the current node and the next node as the two end points for the vertex
//					GameObject vtx = createVertex(net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].node, false);
//					vertice.Add(vtx);
				}

				if(connection[nodeIdx.x+1][nodeIdx.y] == 0)
				{
					connection[nodeIdx.x+1][nodeIdx.y] = 1;
					GameObject nd = net.GetComponent<Network>().cells[nodeIdx.x+1][nodeIdx.y].node;
					//node.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					nd.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					GameObject vtx = createVertex(nd, false);
					vertice.Add(vtx);
				}

			}
			
			if(nodeIdx.y-1>=0)
			{
				//net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].isActive = true;
				if(!net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].isActive){
					net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].isActive = true;
					net.GetComponent<Network>().checkForActivation();
					// add the current node and the next node as the two end points for the vertex
//					GameObject vtx = createVertex(net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].node, true);
//					vertice.Add(vtx);
				}

				if(connection[nodeIdx.x][nodeIdx.y-1] == 0)
				{
					connection[nodeIdx.x][nodeIdx.y-1] = 1;
					GameObject nd = net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y-1].node;
					//node.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					nd.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					GameObject vtx = createVertex(nd, true);
					vertice.Add(vtx);
				}
			}
			
			if(nodeIdx.y+1<height)
			{
				//net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].isActive = true;
				if(!net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].isActive){
					net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].isActive = true;
					net.GetComponent<Network>().checkForActivation();
					// add the current node and the next node as the two end points for the vertex
//					GameObject vtx = createVertex(net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].node, true);
//					vertice.Add(vtx);
				}

				if(connection[nodeIdx.x][nodeIdx.y+1] == 0)
				{
					connection[nodeIdx.x][nodeIdx.y+1] = 1;
					GameObject nd = net.GetComponent<Network>().cells[nodeIdx.x][nodeIdx.y+1].node;
					//node.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					nd.GetComponent<Node>().connection[nodeIdx.x][nodeIdx.y]=1;
					GameObject vtx = createVertex(nd, true);
					vertice.Add(vtx);
				}
			
			}
			
		}
		isFilled = true;
	}

	public GameObject createVertex(GameObject _connectTo, bool _isVertical){
		Transform ta = gameObject.transform;
		Transform tb = _connectTo.transform;
		Vector3 position = (ta.position + tb.position) / 2;
		Quaternion rotation = new Quaternion();
		rotation.eulerAngles = new Vector3(0, 0, Mathf.Atan2 (ta.position.y - tb.position.y, ta.position.x - tb.position.x));
		GameObject vtx = Instantiate (net.GetComponent<Network> ().vertexPrefab, position, rotation) as GameObject;
		vtx.GetComponent<Vertex> ().isVertical = _isVertical;
//		if (_isVertical) {
//			Debug.Log("Vertical");
//			vtx.transform.Rotate(Vector3.forward, 90.0f);
////			Vector3 scaleTemp = vtx.transform.localScale;
////			scaleTemp.x = 1.0f;
//		}
		vtx.GetComponent<Vertex> ().anchorA = gameObject;
		vtx.GetComponent<Vertex> ().anchorB = _connectTo;

		return vtx;
		
	}

	// when place an item on the node, register the item to the network 
	public void addItem(GameObject _itm){
		//net.GetComponent<Network> ().addItemToCell (nodeIdx.x, nodeIdx.y, _itm);

		
	}

	void OnMouseDown(){
		//activateMore ();

	}




}
