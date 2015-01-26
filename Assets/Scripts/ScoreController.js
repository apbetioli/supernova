#pragma strict

var score : int = 0;
var highscore : int = 0;
var player : Player;

function Start() {
	highscore = PlayerPrefs.GetInt("highscore", 0);
}

function Update () {
	guiText.text = "" + score;
	if(player.isDead) {
		guiText.text += "\nTop : " + highscore;
	} 
}

function Add () {
	score++;
}

function OnDestroy() {
	if(score > highscore) {
		highscore = score;
		PlayerPrefs.SetInt("highscore", score);
	}
	score = 0;
}