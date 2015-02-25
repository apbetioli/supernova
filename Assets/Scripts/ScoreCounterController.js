#pragma strict

var ghost : GhostController;

function OnTriggerEnter2D(col : Collider2D) {
	if(col.tag == "Enemy") {
		ghost.AddScore();
	}
}