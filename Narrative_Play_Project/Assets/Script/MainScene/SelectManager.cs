using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {

	[SerializeField] int maxCubeNum=5;
	private int cubeNum=0;
	private int curCubeNum=0; // current cube number
	private int usedCubeNum=0; // used cube number
	private int curStage=0; // current stage of the story 
	private List<GameObject> cubesForSelect; // cubes shown in the current selection window
	private Transform[] cubePositions;
	private bool isProgress = false;

	// 
	public Transform startPosition;
	public GameObject cubePrefab;
	[SerializeField] float step = 5.0f; // the distance between two neighbouring cubes 

	public Camera selCam;
	public GameObject selectCamera;
	public GameObject board;


	// Use this for initialization
	void Start () {
		// initialize the number of cubes 
		cubesForSelect = new List<GameObject> ();
		initCubePosition (0);
		selectCamera.transform.position = new Vector3 (startPosition.position.x, 
		                                               selectCamera.transform.position.y, 
		                                               selectCamera.transform.position.z);
		getCubePos ();

	
	}

	
	void Update()
	{
		if (Input.GetKeyDown ("d")) {
			//ChangeCameraPosRight();
			ChangeCameraPosLeft();
		}
		if (Input.GetKeyDown ("a")) {
			//ChangeCameraPosLeft();
			ChangeCameraPosRight();
		}

		// changing stage 
		if (usedCubeNum == 5 && isProgress) {
			curStage = 1;
			initCubePosition(1);
			isProgress = false;
		}

		if (usedCubeNum == 10 && isProgress) {
			curStage = 2;
			initCubePosition(2);
			isProgress = false;
		}

		if (usedCubeNum == 14 && isProgress) {
			curStage = 3;
			generateEnding(board.GetComponent<GameBoard>().redCube > board.GetComponent<GameBoard>().greenCube);
			isProgress = false;
		}
	}

	// get the positions of the cubes in the selection window 
	public void getCubePos(){
		int i = 0;
		cubePositions = new Transform[cubesForSelect.Count];
		foreach (GameObject c in cubesForSelect) {
			cubePositions[i]=c.transform;
			i += 1;
		}
	}

	public void ChangeCameraPosLeft()
	{
		// The following if statement makes sure the cubeNum doesn't go
		// above or below the number of cubes in the array
		if (cubeNum == cubePositions.Length - 1)
		{
			cubeNum = 0;
		}
		else
		{
			cubeNum++;
		}
		
		Vector3 newPosition = new Vector3(cubePositions[cubeNum].position.x, selectCamera.transform.position.y, selectCamera.transform.position.z);
		// change camera position with tweening
		iTween.MoveTo(selectCamera, newPosition, 1);
	
	}

	public void ChangeCameraPosRight()
	{
		// The following if statement makes sure the cubeNum doesn't go
		// above or below the number of cubes in the array
		if (cubeNum == 0)
		{
			cubeNum = cubePositions.Length - 1;
		}
		else
		{
			cubeNum--;
		}
		Vector3 newPosition = new Vector3(cubePositions[cubeNum].position.x, selectCamera.transform.position.y, selectCamera.transform.position.z);
		// change camera position with tweening
		iTween.MoveTo(selectCamera, newPosition, 1);
		
	}
	
	// arrange the inital cube position 
	public void initCubePosition(int _stage){
		Vector3 cubePos = startPosition.position;
		int curIdx = _stage * maxCubeNum + 1; 

		if (_stage < 2) {
			for (int i=0; i<maxCubeNum; i++) {
				GameObject cube = Instantiate (cubePrefab, cubePos, transform.rotation) as GameObject;
				cube.transform.parent = startPosition.transform;
				cube.GetComponent<SCube> ().initCube (curIdx, cubePos.x, cubePos.z);
				cube.GetComponent<SCube> ().selectCam1 = selCam;
				cubesForSelect.Add (cube);
				cubePos.x += step;
				curIdx ++;
				curCubeNum ++;
			}
		} else {
			for (int i=0; i<maxCubeNum-1; i++) {
				GameObject cube = Instantiate (cubePrefab, cubePos, transform.rotation) as GameObject;
				cube.transform.parent = startPosition.transform;
				cube.GetComponent<SCube> ().initCube (curIdx, cubePos.x, cubePos.z);
				cube.GetComponent<SCube> ().selectCam1 = selCam;
				cubesForSelect.Add (cube);
				cubePos.x += step;
				curIdx ++;
				curCubeNum ++;
			}
		}
	

		Debug.Log ("Current Cube Num: " + curCubeNum);
	}

	// generate the last ending block
	public void generateEnding(bool isHumanEnding)
	{
		selectCamera.transform.position = new Vector3 (startPosition.position.x, 
		                                               selectCamera.transform.position.y, 
		                                               selectCamera.transform.position.z);
		int curIdx = 15;
		if (isHumanEnding) {
			GameObject cube = Instantiate (cubePrefab, startPosition.position, startPosition.transform.rotation) as GameObject;
			cube.transform.parent = startPosition.transform;
			cube.GetComponent<SCube> ().initCube (curIdx, startPosition.position.x, startPosition.position.z);
			cube.GetComponent<SCube> ().wHitRot = new Vector3 (360.0f, 0, 0);
			cube.GetComponent<SCube> ().isEnd = true;
			cube.GetComponent<SCube> ().isAlien = false;
			cube.GetComponent<SCube> ().selectCam1 = selCam;
			cubesForSelect.Add (cube);
		} else {
			Quaternion rotation = startPosition.transform.rotation;
			rotation.x += 180.0f;
			GameObject cube = Instantiate (cubePrefab, startPosition.position, rotation) as GameObject;
			cube.transform.parent = startPosition.transform;
			cube.GetComponent<SCube> ().initCube (curIdx, startPosition.position.x, startPosition.position.z);
			cube.GetComponent<SCube> ().wHitRot = new Vector3 (360.0f, 0, 0);
			cube.GetComponent<SCube> ().isEnd = true;
			cube.GetComponent<SCube> ().isAlien = true;
			cube.GetComponent<SCube> ().selectCam1 = selCam;
			cubesForSelect.Add (cube);
		}
		curCubeNum = 1;
	
	}



	public void reArrangeCubes()
	{
		selectCamera.transform.position = new Vector3 (startPosition.position.x, 
		                                               selectCamera.transform.position.y, 
		                                               selectCamera.transform.position.z);
		if (curCubeNum == maxCubeNum) {
			return;
		}

		if (curCubeNum == 0 && curStage < 4) {
			Debug.Log("Move to next Stage");
			isProgress = true;
		}

		if (curCubeNum > 0 && curCubeNum < 5) {
			Debug.Log ("rearrange position");
			// traverse the cube list 
			Vector3 cubePos = startPosition.position;
			// how to use list<gameobj>.sort??
			foreach (GameObject c in cubesForSelect)
			{
				Vector3 cpos = c.transform.position;
//				float xpos = 0.0f;
//				xpos = cubePos.x;
				cpos.x = cubePos.x;
				cubePos.x += step;
				c.transform.position = cpos;
			}
		}

		// reset the selection camera position 
	}


//	public void useCube(int _cid){
//		foreach (GameObject c in cubesForSelect)
//		{
//			if(c.GetComponent<SCube>().cID == _cid){
//				cubesForSelect.Remove(c);
//				break;
//			}
//		}
//
//		curCubeNum -= 1;
//		reArrangeCubes ();
//	}

	public void useCube(GameObject _cube){
		cubesForSelect.Remove (_cube);
		Destroy (_cube);
		curCubeNum -= 1;
		reArrangeCubes ();
		getCubePos ();
		usedCubeNum += 1;
	}

	public GameObject getLookAtCube(){
		foreach (GameObject c in cubesForSelect)
		{
			if(c.transform.position.x == selCam.transform.position.x)
			{
				Debug.Log("Looking AT: "+ c.GetComponent<SCube>().cID + " " + c.GetComponent<SCube>().isAlien.ToString());
				return c;
			}
		}

		return null;
	}

}
