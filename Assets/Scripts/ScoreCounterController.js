#pragma strict

var player : PlayerController;
var ghost : GhostController;

function OnTriggerEnter2D(col : Collider2D) {
	if(col.tag == "Enemy") {
		player.AddScore();
		ghost.AddScore();
	}
}