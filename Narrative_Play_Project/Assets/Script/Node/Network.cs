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

	private Vector3 [] nodePositions;
	private Vector3 randomOffset;

	// bck music 
	private GameObject bckMusic;

	// human-alien head turning 
	//private int crtHead; // the current head position 
	private int nxtHead; // the head position to turn 
	private GameObject [] heads;
	private int headIdx;
	// for ending 
	private int endIdx;
	private GameObject[] ends;

	// timing for the end 
	private float timeStart;
	[SerializeField] float endWaitTime = 10.0f;
	private bool isReadyToEnd = false;
	private bool isEndTriggered = false;

	// Use this for initialization
	void Start () {
		// initialize the network 
		nodes = new List<Node> ();
		nodesToFlip = new List<NodeCell> ();
	
		cells = new NodeCell[nWidth][];
		for (int i = 0; i<nWidth; i++) {
			cells[i] = new NodeCell[nHeight];
		}

		// init the nodes 
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
		checkForActivation ();

		// initialize the Environment 
		bckMusic = GameObject.Find("Sound");
		initItems ();
		enterAct (1);
		initHeads ();
		initEnds ();


	}

	void initHeads(){
		heads = new GameObject[5];
		heads[0] = GameObject.Find("humanTurn2");
		heads[1] = GameObject.Find("humanTurn1");
		heads[2] = GameObject.Find("balance");
		heads[3] = GameObject.Find("alienTurn1");
		heads[4] = GameObject.Find("alienTurn2");
		for (int i=0; i<5; i++) {
			heads[i].GetComponent<FadeMaterial>().setAlpha(0);
		}
		headIdx = 2;
		heads [headIdx].GetComponent<FadeMaterial> ().FadeIn ();
	}

	void initEnds(){
		ends = new GameObject[7];
		ends[0] = GameObject.Find("HE3");
		ends[1] = GameObject.Find("HE2");
		ends[2] = GameObject.Find("HE1");
		ends[3] = GameObject.Find("HAEQUAL");
		ends[4] = GameObject.Find("AE1");
		ends[5] = GameObject.Find("AE2");
		ends[6] = GameObject.Find("AE3");

		for (int i=0; i<7; i++) {
			ends[i].GetComponent<FadeMaterial>().setAlpha(0);
		}
		endIdx = -1;
	}

	// generate random offset for node position 
//	Vector3 genRandom(){
//		
//	}

	// generate a new node
	// called when new node position are allowed on the board 
	public GameObject generateNode(int _id, Vector3 _position, int _i, int _j){
		// random offset 
		Vector3 random = Random.insideUnitSphere * 0.2f;
		_position.x += random.x;
		_position.y += random.y;
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
		_itm.GetComponent<StoryItem> ().isPlaced = true;
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
		// turn the head 
		checkHeadPosition (- humanCount + alienCount);
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
		if (_diff >= -14 && _diff < -7) {
			nxtHead = 0;
		}
		if (_diff >= -7 && _diff < -2) {
			nxtHead = 1;
		}
		if (_diff >= -2 && _diff < 3) {
			nxtHead = 2;
		}
		if (_diff >= 3 && _diff < 8) {
			nxtHead = 3;
		}
		if (_diff >= 8 && _diff < 14) {
			nxtHead = 4;
		}

		if (headIdx != nxtHead) {
			heads[headIdx].GetComponent<FadeMaterial>().FadeOut();
			heads[nxtHead].GetComponent<FadeMaterial>().FadeIn();
			headIdx = nxtHead;
		}
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

//		if (totalCount == ACT_TIR && !isEnterTrd) {
//			Debug.Log("Enter Endding");
//			//showEnd();
//			isEnterTrd = true;
//			
//		}
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
				itm.SetActive(true);
				itm.GetComponent<FadeMaterial>().FadeIn();
			}
		}

		if (_actIdx == 2) {
			//clearBackgroud();
			bckFst.GetComponent<FadeMaterial>().FadeOut();
			bckSnd.GetComponent<FadeMaterial>().FadeIn();
			foreach (GameObject itm in stage2) {
				itm.SetActive(true);
				itm.GetComponent<FadeMaterial>().FadeIn();
			}

		}

		if (_actIdx == 3) {
			//clearBackgroud();
			bckSnd.GetComponent<FadeMaterial>().FadeOut();
			bckTrd.GetComponent<FadeMaterial>().FadeIn();
			foreach (GameObject itm in stage3) {
				itm.SetActive(true);
				itm.GetComponent<FadeMaterial>().FadeIn();
			}
		}

		bckMusic.GetComponent<BackgroundMusic> ().playMusic (_actIdx);
	}

	// Initialize Item positions according to act index
	void initItems(){
		stage1 = GameObject.FindGameObjectsWithTag ("Act1");
		stage2 = GameObject.FindGameObjectsWithTag ("Act2");
		stage3 = GameObject.FindGameObjectsWithTag ("Act3");
		foreach (GameObject itm in stage1) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
			itm.SetActive(false);
		}
		foreach (GameObject itm in stage2) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
			itm.SetActive(false);
		}
		foreach (GameObject itm in stage3) {
			itm.GetComponent<FadeMaterial>().setAlpha(0.0f);
			itm.SetActive(false);
		}
	}

	// show ending 
	void showEnd(){
		// TODO: show end by the difference of human and alien 
		bckMusic.GetComponent<BackgroundMusic> ().playMusic (4);
		heads [headIdx].GetComponent<FadeMaterial> ().fadeTime = 15.0f;
		heads [headIdx].GetComponent<FadeMaterial> ().FadeOut ();
		int difference = alienCount - humanCount;
		if (difference >= -14 && difference <= -11) {
			Debug.Log("humanEnding3");
			endIdx = 0;
		}
		if (difference >= -10 && difference <= -7) {
			Debug.Log("humanEnding2");
			endIdx = 1;
		}
		if (difference >= -6 && difference <= -3) {
			Debug.Log("humanEnding1");
			endIdx = 2;
		}
		if (difference >= -2 && difference <= 2) {
			Debug.Log("humanAlienEnding");
			endIdx = 3;
		}
		if (difference >= 3 && difference <= 6) {
			Debug.Log("AlienEnding1");
			endIdx = 4;
		}
		if (difference >= 7 && difference <= 10) {
			Debug.Log("AlienEnding2");
			endIdx = 5;
		}
		if (difference >= 11 && difference <= 14) {
			Debug.Log("AlienEnding3");
			endIdx = 6;
		}
		ends [endIdx].GetComponent<FadeMaterial> ().fadeTime = 15.0f;
		ends [endIdx].GetComponent<FadeMaterial> ().FadeIn ();
		ends [endIdx].GetComponent<AudioSource> ().Play ();

	}

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
		// checkForActivation ();
		if (!isEndTriggered && totalCount == 14) {
			timeStart = Time.time;
			isEndTriggered = true;
		} else {
			//timeStart = Time.time;
		}

		if (!isReadyToEnd && isEndTriggered && Time.time - timeStart >= endWaitTime) {
			isReadyToEnd = true;
		}

		// enter the end 
		if (isReadyToEnd && !isEnterTrd ){
			Debug.Log("Enter Ending");
			for (int i = 0; i<nWidth; i++) {
				for (int j = 0; j<nHeight; j++)
				{
					if(cells[i][j].item != null)
					{
						cells[i][j].item.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50.0F, 50.0F),Random.Range(-50.0F, 50.0F), 0));
						cells[i][j].item.GetComponent<StoryItem>().isPlaced = false;
						cells[i][j].item.GetComponent<DragDrop>().isSettled= false;
						cells[i][j].item.GetComponent<AudioSource>().Stop();
					}

				}
            }

			GameObject[] nodes = GameObject.FindGameObjectsWithTag("node");
			GameObject[] vertice = GameObject.FindGameObjectsWithTag("vertex");
			foreach (GameObject n in nodes){
				n.AddComponent<Rigidbody>();
				n.GetComponent<Rigidbody>().useGravity = false;
				n.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-30.0F, 30.0F)*2.0f,Random.Range(-30.0F, -10.0F)*2.0f, 0));

			}
			foreach (GameObject v in vertice){
				v.AddComponent<Rigidbody>();
				v.GetComponent<Rigidbody>().useGravity = false;
				v.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-30.0F, 30.0F)*2.0f,Random.Range(-30.0F, -10.0F)*2.0f, 0));
				
			}
			showEnd();
			isEnterTrd = true;
		}

		// if enter the ending condition :
		// 1. check for audio playing 
		// 2. wait for some time to reveal the ending 


	}
}
