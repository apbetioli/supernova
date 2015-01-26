#pragma strict

var player : Player;
var totalOffset : int;
var step : float;
var counter : float;
var idleCounter : float;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
	if(player == null) 
		Debug.LogError("Could not find the Player");
}

function Update () {
	if( counter > 0 ) {
		Move();
		counter -= step;
	} else {
		if(player.CanMove()) {
			counter = totalOffset - idleCounter;
			idleCounter = 0;
		} else {
			IdleMove();
		}
	}
}

function Move() {
	if(player.isDead)
		return;
	
	transform.position.y -= step;
}

function IdleMove() {
	if(player.isDead)
		return;
		
	var idle = Time.deltaTime * step / 2;
	idleCounter += idle;
	transform.position.y -= Time.deltaTime * step;
}