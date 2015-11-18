using UnityEngine;
using System.Collections;

public class SelectionCubeManager : MonoBehaviour {

    //each cube has an id
    public int id;
    // each cube has an x and y position
    public float x;
    public float y;

    // each cube has human and alien text.
    public string humanText;
    public string alienText;

	// which side up
	public bool isAlien = false; // default human state 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
