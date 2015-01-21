#pragma strict

var player : Player;
var step = 2;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
}

function Update () {
	if(player.CanMove()) {
		Move();
	}
}

function Move() {
	transform.position.y -= step;
}
