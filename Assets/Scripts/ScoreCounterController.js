#pragma strict

var ghost : GhostController;
var ui : UIController;

function OnTriggerEnter2D(col : Collider2D) {
	if(col.tag == "Enemy") {
		ghost.AddScore();
		ui.PlayPassbySound();
	}
}