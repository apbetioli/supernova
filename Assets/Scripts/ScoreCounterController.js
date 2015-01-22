#pragma strict

var scoreController : ScoreController;

function Start () {
	scoreController = GameObject.Find("Score").GetComponent(ScoreController);
	if(scoreController == null)
		Debug.LogError("Coudn't find ScoreController");
}

function OnTriggerEnter2D(col : Collider2D) {
	scoreController.Add();
}