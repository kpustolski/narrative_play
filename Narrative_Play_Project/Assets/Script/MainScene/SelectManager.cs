using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {

	[SerializeField] int maxCubeNum=5;
	private int curCubeNum=0; // current cube number
	private int usedCubeNum=0; // used cube number
	private int curStage=0; // current stage of the story 
	private List<GameObject> cubesForSelect; // cubes shown in the current selection window

	// 
	public Transform startPosition;
	public GameObject cubePrefab;
	[SerializeField] float step = 5.0f; // the distance between two neighbouring cubes 

	public Camera selCam;


	// Use this for initialization
	void Start () {
		// initialize the number of cubes 
		cubesForSelect = new List<GameObject> ();
		initCubePosition (0);

	
	}

	
	// Update is called once per frame
	void Update () {
	
	}


	// arrange the inital cube position 
	public void initCubePosition(int _stage){
		Vector3 cubePos = startPosition.position;
		int curIdx = _stage * maxCubeNum + 1; 
	
		for (int i=0; i<maxCubeNum; i++) {
			GameObject cube = Instantiate(cubePrefab, cubePos, transform.rotation) as GameObject;
			cube.transform.parent = startPosition.transform.parent;
			cube.GetComponent<SCube>().initCube(curIdx, cubePos.x, cubePos.z);
//			cube.GetComponent<TurnCube>().humanText = cube.GetComponent<SCube>().humanText;
//			cube.GetComponent<TurnCube>().alienText = cube.GetComponent<SCube>().alienText;
			cube.GetComponent<SCube>().selectCam1 = selCam;
			//
			cubesForSelect.Add(cube);
			cubePos.x += step;
			curIdx ++;
			curCubeNum ++;
		}

		Debug.Log ("Current Cube Num: " + curCubeNum);
	}

	public void reArrangeCubes()
	{
		if (curCubeNum == maxCubeNum) {
			return;
		}

		if (curCubeNum == 0 && curStage < 3) {
			Debug.Log("Move to next Stage");
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
			}
		}
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
