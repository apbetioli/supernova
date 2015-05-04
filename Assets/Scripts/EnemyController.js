#pragma strict

var player : PlayerController;
var step : float;
var positiony : float;
var rotationVelocity : float;

function Start () {
	player = GameObject.Find("Player").GetComponent(PlayerController);
	if(player == null) 
		Debug.LogError("Could not find the Player");
	
	positiony = transform.position.y;
}

function Update () {
	transform.Rotate(0, 0, -rotationVelocity*Time.deltaTime*transform.position.x);
	
	transform.position.y = Mathf.Lerp(transform.position.y, positiony, 5 * step * Time.deltaTime);
}

function OnTouch() {
 	if(!player.isDead)
		positiony = positiony - step;
}