#pragma strict

var level = 1;
var player : Player;
var originalScale : float = 0.7875;
var minusFactor : float = 0.1;
var plusFactor : float = 1.0;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
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