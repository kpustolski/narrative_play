#pragma strict

function Start () {

}

function Update () {

}

function StartGame () {

    Application.LoadLevel(2);
}

function StartRules(){
    Application.LoadLevel(1);

}

function StartExercise(){
	Application.LoadLevel(3);
}
function ExitGame () {
    Application.Quit();
}