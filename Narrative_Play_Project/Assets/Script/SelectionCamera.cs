using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionCamera : MonoBehaviour {
	// Variables
	public GameObject[] cubePositions;

	// button variables for moving camera position
	public Button left;
	public Button right;
	public bool isClicked;

	public int cubeNum;

	private float initialCamPosX;
	// Use this for initialization
	void Start () {
		cubePositions= new GameObject[0];
		cubeNum = 0;
		initialCamPosX = cubePositions [cubeNum].transform.position.x;

		transform.position = new Vector3 (initialCamPosX, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if( left.onClick) {
			transform.position= new Vector3(cubePositions[]);
		}
	}

}
