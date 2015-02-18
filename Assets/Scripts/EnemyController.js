#pragma strict

var player : PlayerController;
var step : float;
var counter : float = 0;

function Start () {
	player = GameObject.Find("Player").GetComponent(PlayerController);
	if(player == null) 
		Debug.LogError("Could not find the Player");
}

function Update () {
	if(player.isDead) {
		transform.position.y -= step * Time.deltaTime;
		return;
	}

	if(counter > 0) {
		var aux : float = step/4.0;
		transform.position.y -= aux;
		counter -= aux;
	}
	else {
	
		if(player.CanMove()) {
			transform.position.y -= counter;		
			counter = step;
		}
	}
}

