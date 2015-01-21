#pragma strict

var score = 0;
var highscore = 0;

function Start() {
	highscore = PlayerPrefs.GetInt("highscore", 0);
}

function Update () {
	var scoreboard = guiText;
	scoreboard.text = "Score : " + score + "\nTop : " + highscore;
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