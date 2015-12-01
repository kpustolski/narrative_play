using UnityEngine;
using System.Collections;

public class colliderController : MonoBehaviour {

	public GameObject menuItem;
	public GameObject pointLight;
	private Light lt;

	void Start () {
//		light = GameObject.FindGameObjectWithTag("Point_light");
		lt = pointLight.GetComponent<Light> ();
	}

	void OnTriggerEnter(Collider menuItem) {
		Debug.Log ("triggered");
		lt.intensity = 5.0f;
	}

}
