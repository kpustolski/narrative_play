using UnityEngine;
using System.Collections;

public class TurnCube : MonoBehaviour {

    public string humanText="";
    public string alienText= "";

    public Camera selectCam1;

    private bool isRotated;
    private bool showText;
    private string labelText;
    Vector3 wHitRot = new Vector3(180, 0, 0);
	Vector3 sHitRot = new Vector3(-180, 0, 0);

   // Vector3 humanSide;
    //Vector3 alienSide;

    // Use this for initialization
    void Start()
    {
        isRotated = false;
        showText = false;
  //      humanSide = new Vector3(0.3515936f, 0, 0);
       // alienSide = new Vector3(359.8242f, 180, 180);
    }

    // Update is called once per frame
    void Update()
    {
        print("camera position.x: "+ selectCam1.transform.position.x);
        print("cube position.x: " + (transform.position.x));
        if (transform.rotation.x >= 358f && transform.rotation.x <= 360f  && selectCam1.transform.position.x == transform.position.x)
        {
               print("Equal positions human");
               labelText = humanText;
         
        }
       if (transform.rotation.x <= 1f && selectCam1.transform.position.x == transform.position.x)
        {
               print("Equal positions alien");
               labelText = alienText;            
        }

        if(labelText != "")
        {
            showText = true;
        }
        else
        {
            labelText = "";
            showText = false;
        }

        if (Input.GetKeyDown("w")) {
            iTween.RotateAdd(gameObject, wHitRot, 1);
            print("rotatae");
            //isRotated = true;
        }
        if (Input.GetKeyDown("s"))
        {
            iTween.RotateAdd(gameObject, sHitRot, 1);
            print("rotatae");
            //isRotated = true;
        }
    }

    void OnGUI()
    {
        if (showText == true)
        {
            GUIStyle myStyle = new GUIStyle();
            GUI.color = Color.black;
            myStyle.fontSize = 10;
            //http://answers.unity3d.com/questions/17683/custom-font-in-guilabel-but-cant-change-its-color.html
            myStyle.normal.textColor = Color.white;
            myStyle.fontStyle = FontStyle.Bold;

       
            //myStyle.alignment= TextAnchor.MiddleCenter;
            //GUI.backgroundColor=Color.clear;
           // GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");

           GUI.Label(new Rect(10, 10, 100, 20), labelText, myStyle);

        }
    }
}
