#pragma strict

var player : PlayerController;
var step : int;

function Start () {
	player = GameObject.Find("Player").GetComponent(PlayerController);
	if(player == null) 
		Debug.LogError("Could not find the Player");
}

function Update () {
	if(player.CanMove()) 
		transform.position.y -= step;
		
	if(player.isDead) 
		transform.position.y -= step * Time.deltaTime * 0.5;
	
}

