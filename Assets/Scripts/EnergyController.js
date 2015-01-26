#pragma strict

var player : Player;
var level : int;
var originalScale : float;
var minusFactor : float;
var plusFactor : float;

function Start () {		
	originalScale = transform.localScale.x;
}

function Update () {

	if(player.isDead)
		return;
	
	var off = Time.deltaTime * level;
	
	if(player.CanMove()) {
		var toadd = plusFactor * off;
		if( (transform.localScale.x + toadd) > originalScale )
			transform.localScale.x = originalScale;
			
		transform.localScale.x += toadd;
		return;
	}
	
	if(transform.localScale.x > 0)
		transform.localScale.x -= off * minusFactor;
	else 
		player.Die();
		
}