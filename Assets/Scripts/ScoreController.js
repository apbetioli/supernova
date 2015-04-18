#pragma strict

var player : PlayerController;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;

function Start() {
	gameOverScoreText.text = "\n\n\ntap to play\n\n\nleft and right";
}

function Update () {
	scoreText.text = "" + player.score;
	
	if(player.isDead) {
		gameOverScoreText.text = "score: " + player.score + "\n";
		
		if(player.score >= player.highscore) {
			gameOverScoreText.text += "NEW ";
			gameOverScoreText.color = Color.cyan;
		}
		
		gameOverScoreText.text += "best : " + player.highscore;
	}

}
