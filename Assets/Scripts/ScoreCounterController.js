#pragma strict

var scoreController : ScoreController;

function OnTriggerEnter2D(col : Collider2D) {
	scoreController.Add();
}