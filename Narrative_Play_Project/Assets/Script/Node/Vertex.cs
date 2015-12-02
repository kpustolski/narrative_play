using UnityEngine;
using System.Collections;

public class Vertex : MonoBehaviour {
	// 
	public GameObject anchorA; 
	public GameObject anchorB;
	public bool isVertical;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// update the vertex position 
		refreshPosition ();
	}

	public void getAnchors(GameObject _a, GameObject _b){
		anchorA = _a;
		anchorB = _b;
	}

	public void refreshPosition(){
		Vector3 posA = anchorA.transform.position;
		Vector3 posB = anchorB.transform.position;
		Vector3 position = new Vector3 ((posA.x + posB.x) / 2, (posA.y + posB.y) / 2, (posA.z + posB.z) / 2);
		//
		Vector3 lookPos = posA-posB;
		float angle = Mathf.Atan2 (lookPos.y, lookPos.x) * Mathf.Rad2Deg;

//		Quaternion rotation = new Quaternion();
//		rotation.eulerAngles = new Vector3(0, 0, Mathf.Atan2 (posA.y - posB.y, posA.x - posB.x));
		//rotation.eulerAngles = new Vector3(0, 0, 20.0f);
		gameObject.transform.position = position;
		gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//		if (isVertical) {
//			Debug.Log("Vertical");
//			gameObject.transform.Rotate(Vector3.forward, 90.0f);
//			//			Vector3 scaleTemp = vtx.transform.localScale;
//			//			scaleTemp.x = 1.0f;
//		}
	}
		            

}
