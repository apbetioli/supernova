#pragma strict

var player : PlayerController;
var scoreText : UI.Text;
var gameOverScoreText : UI.Text;

function Update () {
	scoreText.text = "" + player.score;
	
	if(player.isDead) {
		gameOverScoreText.text = "score: " + player.score + "\nbest : " + player.highscore;
		
		if(player.score >= player.highscore) {
			gameOverScoreText.text = "NEW RECORD: " + player.score;
			gameOverScoreText.color = Color.cyan;
		}
		
	}

}
