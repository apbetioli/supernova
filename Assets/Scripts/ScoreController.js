#pragma strict

var player : PlayerController;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;
var coinsText : UI.Text;

function Update () {
	scoreText.text = "" + player.score;
	coinsText.text = "" + player.coins;
	gameOverScoreText.text = "Best : " + player.highscore;
}
