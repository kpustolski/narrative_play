using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 original;
	private Vector3 target;
	private bool isTargetFound;
	private bool isSettled;
	private Node node;

	void Start(){
		isTargetFound = false;
		isSettled = false;
		original = gameObject.transform.localPosition;
		//oriPos = new Vector3 (original.x, original.y, original.z);
	}


	void OnMouseDown()
	{ 
		if (!isSettled) {
			// get the current gameobj position in screen
			// world --> screen 
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			// get the offset (difference in cooordinates) for dragging 
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}
	
	void OnMouseDrag() 
	{  
		if (!isSettled) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			gameObject.transform.position = curPosition;
		}
	}

	// when release the mouse, check for collision 
	void OnMouseUp()
	{
		if (!isTargetFound) {
			target = original;

//			target.x = oriPos.x;
//			target.y = oriPos.y;
//			target.z = oriPos.z;
		} else {
			node.activateMore();
			isSettled = true;
		}


		// move the object to the target position 
		iTween.MoveTo (gameObject,target, 2.0f);

	}


	void OnTriggerEnter(Collider other)
	{
		if (other != null && other.CompareTag("node")) {
			isTargetFound = true;
			target = other.transform.position;
			target -= 1.0f * Vector3.forward;
			node = other.GetComponent<Node>();

		}
	}

	void OnTriggerExit(Collider other)
	{
		isTargetFound = false;
	}
	
}
