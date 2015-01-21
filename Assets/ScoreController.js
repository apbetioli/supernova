#pragma strict

var score = 0;
var highscore = 0;
var player : Player;

function Start() {
	highscore = PlayerPrefs.GetInt("highscore", 0);
	player = GameObject.Find("Player").GetComponent(Player);
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