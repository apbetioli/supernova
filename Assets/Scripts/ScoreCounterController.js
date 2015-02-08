#pragma strict

var player : PlayerController;

function OnTriggerEnter2D(col : Collider2D) {
	if(col.tag == "Enemy")
		player.AddScore();
}