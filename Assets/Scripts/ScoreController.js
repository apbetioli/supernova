#pragma strict

var player : PlayerController;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;

function Start() {
	gameOverScoreText.text = "Best : " + player.highscore;
}

function Update () {
	scoreText.text = "" + player.score;
	
	if(player.isDead) {
		gameOverScoreText.text = "";
		
		if(player.score >= player.highscore)
			gameOverScoreText.text += "NEW ";
		
		gameOverScoreText.text += "Best : " + player.highscore;
	}

}
