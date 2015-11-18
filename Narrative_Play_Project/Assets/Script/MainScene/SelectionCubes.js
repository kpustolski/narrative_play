#pragma strict

// public variables
//camera
var selectCam1;

//private variables
var  showText;
var labelText;
var isHumanText;

var cube= new Hashtable();
var initXPos;
var initYPos;
//Vector3 variables
var wHitRot = new Vector3(180, 0, 0);

var selectionCubeArray=new Array();;
//Vector3 sHitRot = new Vector3(-180, 0, 0);
// Use this for initialization

function Start () {
  

    MakeCube(0, 0, initYPos, "I AM HUMAN 1", "I AM ALIENANANSZZZZ 1");
    MakeCube(1, 15, initYPos, "I AM HUMAN 2", "I AM ALIENANANSZZZZ 2");

    //   print("done"+ "cube[i]: "+ selectionCubeArray[0]["id"]);

}

function MakeCube(ident,xPos,yPos,humanText,alienText){
    var cube= new Hashtable();
    cube.Add("id",ident);
    cube.Add("x",xPos);
    cube.Add("y",yPos);
    cube.Add("human", humanText);
    cube.Add("alien",alienText);

    selectionCubeArray.push(cube);


}
function Update () {

}
