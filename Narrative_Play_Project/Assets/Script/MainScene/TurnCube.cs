using UnityEngine;
using System.Collections;

public class TurnCube : MonoBehaviour {

    Vector3 rightClickRot = new Vector3(0, 90, 0);
    Vector3 leftClickRot = new Vector3(90, 0, 0);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            iTween.RotateAdd(gameObject, leftClickRot, 1);

        if (Input.GetMouseButtonDown(0))
            iTween.RotateAdd(gameObject, rightClickRot, 1);
    }

   /* void OnMouseDown()
    {
        Vector3 newRot = new Vector3(90, 0, 0);
        //transform.Rotate(Vector3.right * Time.deltaTime * 180);
        iTween.RotateAdd(gameObject,newRot, 1);
    }*/
}
