#pragma strict

var player : Player;
var level : float;
var originalYPosition : float;
var maxYPosition : float;
var minusFactor : float;
var plusFactor : float;
var position : float;

function Start () {		
	originalYPosition = transform.position.y;
}

function Update () {

	if(player.isDead)
		return;
	
	var delta = Time.deltaTime * level;
	
	var toSubtract = minusFactor * delta;
	
	if(player.CanMove()) {
		if( (transform.position.y - toSubtract) < originalYPosition )
			position = originalYPosition;
		else
			position = transform.position.y - toSubtract;
			
		return;
	}
	
	var toAdd = plusFactor * delta;
	
	if(transform.position.y < maxYPosition)
		position = transform.position.y + toAdd;
	else 
		player.Die();
		
}

function FixedUpdate() {
	transform.position.y = position;
}
