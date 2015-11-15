using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionScroll : MonoBehaviour
{

    // Variables
    //cube array
    public GameObject[] cubePositions;

    // button variables for moving camera position
    public Button left;
    public Button right;

    //camera
    public Camera selectCam;

    //private variables
    private int cubeNum;
    private float initialCamPosX;
    private float finalCamPosX;

    // Use this for initialization
    void Start()
    {
        // find cubes with the following label
        cubePositions = GameObject.FindGameObjectsWithTag("SelectionCubes"); ;

        // init. variables
        selectCam = selectCam.GetComponent<Camera>();
        left = left.GetComponent<Button>();
        right = right.GetComponent<Button>();
        cubeNum = 0;

        //init position of camera. The camera is set to face the first cube in the array.
        initialCamPosX = cubePositions[cubeNum].transform.position.x;
        selectCam.transform.position = new Vector3(initialCamPosX, selectCam.transform.position.y, selectCam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // once the right button is clicked, increase array count and 
    // change camera's x position to that cube in the array
    public void ChangeCameraPosRight()
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

        Vector3 newPosition = new Vector3(cubePositions[cubeNum].transform.position.x, selectCam.transform.position.y, selectCam.transform.position.z);
        // change camera position with tweening
        iTween.MoveTo(selectCam.gameObject, newPosition, 2);
        //basic changing positions
       // selectCam.transform.position = new Vector3(cubePositions[cubeNum].transform.position.x, selectCam.transform.position.y, selectCam.transform.position.z);
    }
    // once the left button is clicked, decrease array count and 
    // change camera's x position to that cube in the array
    public void ChangeCameraPosLeft()
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
        Vector3 newPosition = new Vector3(cubePositions[cubeNum].transform.position.x, selectCam.transform.position.y, selectCam.transform.position.z);
        // change camera position with tweening
        iTween.MoveTo(selectCam.gameObject, newPosition, 2);
        // basic changing positions
        //selectCam.transform.position = new Vector3(cubePositions[cubeNum].transform.position.x, selectCam.transform.position.y, selectCam.transform.position.z);

    }
}

