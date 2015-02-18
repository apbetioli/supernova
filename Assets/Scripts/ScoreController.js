#pragma strict

var player : PlayerController;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;
var counter: float;

function Start() {
	counter = 0;
	
	gameOverScoreText.text = "Best : " + player.highscore;
}

function Update () {
	scoreText.text = "" + player.score;
		
	if(player.isDead) {
		if( counter < player.score) {
			gameOverScoreText.text = "Score : " + counter;
			counter += 1;
		}
		else
			gameOverScoreText.text = "Score : " + player.score + "\nBest : " + player.highscore;
	}

}
