#pragma strict

var player : Player;
var totalOffset : int = 2;
var step : float = 0.5;
var counter : float = 0;
var idleCounter : float = 0;

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