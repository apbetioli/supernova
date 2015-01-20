#pragma strict

var score = 0;
var highscore = 0;
var scoreboard: UI.Text;

function Start() {
	highscore = PlayerPrefs.GetInt("highscore", 0);
}

function Update () {
	scoreboard.text = "Score: " + score + "\nTop: " + highscore;
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