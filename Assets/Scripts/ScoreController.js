#pragma strict

var score : int = 0;
var highscore : int = 0;
var player : Player;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;

function Start() {
	highscore = PlayerPrefs.GetInt("highscore", 0);
}

function Update () {
	scoreText.text = "" + score;
	
	if(player.isDead) {
		gameOverScoreText.text = "Score : " + score + "\nBest : " + highscore;
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