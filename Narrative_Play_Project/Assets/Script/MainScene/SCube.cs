using UnityEngine;
using System.Collections;

public class SCube : MonoBehaviour {

	// cube id
	public int cID; 

	// text
	public string humanText;
	public string alienText;

	// each cube has an x and y position
	public float x;
	public float y;
	
	public bool isAlien = false;
	
	//camera
	public Camera selectCam1;
	
	//private variables
	private bool showText;
	private string labelText;
	private bool isHumanText;
	
	//Vector3 variables
	Vector3 wHitRot = new Vector3(180, 0, 0);
	//Vector3 sHitRot = new Vector3(-180, 0, 0);
	
	// Use this for initialization
	void Start()
	{
		//init variables
		
		showText = false;
		labelText = "";
		isHumanText = true;
	}

	public void initCube(int _cid, float _posx, float _posy)
	{
		cID = _cid;
		x = _posx;
		y = _posy;

        // assign human/alien text 
        // ----- for test 
        humanText = "Cube " + cID.ToString() + ": human side";
        alienText = "Cube " + cID.ToString() + ": alien side";

        // -----TODO: hard code all the story cubes 
        /*switch (cID) {
            //ACT 1
			case 1:
				humanText = "My body aches to break the seal.\nI feel pale and weird and unfinished.";
				alienText = "Within a humble virus, I quaff this pulsing body,\nthese sensory stimuli, and spread mightily.";
				break;
            case 2:
                humanText = "Three months of darkness and shit-stained slush. Why bother?";
                alienText = "Sunlight glitters through falling snow.\nEach crystal is unique, not unlike human beings.";
                break;
            case 3:
                humanText = "I randomly blacked out\nand woke up on the foorball field.\nSome jocks were roughing me up. FTW";
                alienText = "I left home planet and clan\nto reap the riches of Earth via this vessel,\nmy precious host.";
                break;
            case 4:
                humanText = "I saw my crush today.\nWe made eye contact - twice.";
                alienText = " It pains me to feel your heart race\nover a childish crush. Aren't I enough for you?";
                break;
            case 5:
                humanText = "Something's hella wrong with me.\nPhones go dead in my hands.\nParents shield their children.";
                alienText = " We are one body, one consciousness, one soul.\nThere's no room for anyone else!";
                break;
            // ACT 2
            case 6:
                humanText = "I can't control my hands.\nMy legs look foreign.\nI just want to sleep, but I keep walking.";
                alienText = "The world is gashes of light\nand icy wet sepping down to the bone.\nI am large. I carry us both.";
                break;
            case 7:
                humanText = "The sleet cocoons me from the chaos.\nWind-ripped branches make broken glass.";
                alienText = "I shake like a rattle in this ribcage.\nThe cold could be our death.";
                break;
            case 8:
                humanText = "I saved my crush from tripping.\nWe touched hands and fingers.\nThe rest is hazy...";
                alienText = "The flitting eyelashes of my host's crush spell my doom.\nThere's not host enough for both of us.";
                break;
            case 9:
                humanText = "I never fit in, and I never will.\nI'm running away. The horizon is calming.\nI have a plan.";
                alienText = "I am my host's only friend.\nWe will journey the Earth together or die.";
                break;
            case 10:
                humanText = "I'm either asleep, tripping, or giving birth to a demon.\nNo one would understand, except...";
                alienText = "Nearly full grown,\nI press a tendril against my host's abdomen\nto make my presence known.";
                break;
            // ACT 3
            case 11:
                humanText = "God God God\nGod God God God";
                alienText = "I breath each breath.\nI beat this heart.\nI am one with the universe.";
                break;
            case 12:
                humanText = "It's warm and misty.\nThe smell is amazing.";
                alienText = "Shafts of moonlight fracture rainbows\nthrough prisms of dew garnishing rows of Orchidaceae";
                break;
            case 13:
                humanText = "I wake in a glass house filled with plants.\nMy crush - my love,\nis kissing my frozen fingers back to life.";
                alienText = "I blacked out and awoke in the greenhouse\nof my host's crush.\nI feel cheated, but releaved; we're alive.";
                break;
            case 14:
                humanText = "I disclose the alien.\n My love isn't scared.\n We pet and kiss and start screwing super slowly.\n It dawns on me that lovemaking is prolly why\n humans endure all this BS.";
                alienText = "I'm shocked by the raw bliss of lovemaking.\nI grasp our love's hair.\nI feel our love as one with me.\nThis must be why I came so very far,\nand gave up so much.";
                break;
            case 15:
                humanText = "I am part alien.\nWeirdly, this validates how I always felt,\nbut never knew for sure.\nMy alien status may limit my life in some ways,\nbut it could also clarify what- and who- is important.";
                alienText = "I'm ashamed of my arrogance.\nMy petty jealousy nearly cost our lives.\nLove gave us a second chance.\nI am only one voice in this body,\none small voice in this universe,\nborn of what, we know not.";
                break;

        }*/
    }


    // Update is called once per frame
    void Update()
	{
		//print("camera position.x: "+ selectCam1.transform.position.x);
		//print("cube position.x: " + (transform.position.x));
		
		// if there is text, show it
		// else don't show it
		if(labelText != "")
		{
			showText = true;

		}
		else
		{
			labelText = "";
			showText = false;

			// show default text 
		}
		
		// if the camera's x position is the same as the cube's...
		if (selectCam1.transform.position.x == transform.position.x) {

			if(isAlien)
			{
				labelText = alienText;
			}else{
				labelText = humanText;
			}

			/// if the w key is pressed...
			if (Input.GetKeyDown ("w")) {
				// rotate the block 180 degrees in the x direction
				iTween.RotateAdd (gameObject, wHitRot, 0.3f);
				//print ("rotate");
				//print ("Equal positions human");
				isAlien = !isAlien;
				
				// determine whether to show human or alien text. 

			
			} // end keydown w
			// else label text is blank
		} 
		else {
			labelText="";
		}
		
	} // end update
	
	// GUI function
	void OnGUI()
	{
		// if we want the text to show...
		if (showText == true)
		{
			// play with text style
			GUIStyle myStyle = new GUIStyle();
			GUI.color = Color.black;
			myStyle.fontSize = 15;
			//http://answers.unity3d.com/questions/17683/custom-font-in-guilabel-but-cant-change-its-color.html
			myStyle.normal.textColor = Color.white;
			myStyle.fontStyle = FontStyle.Bold;
			
			
			//myStyle.alignment= TextAnchor.MiddleCenter;
			//GUI.backgroundColor=Color.clear;
			// GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
			
			// create text.
			GUI.Box(new Rect(Screen.width/3, 10, 100, 20), labelText, myStyle);
			
		}
	}



}
