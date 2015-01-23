#pragma strict

var score = 0;
var highscore = 0;
var player : Player;

function Start() {
	player = GameObject.Find("Player").GetComponent(Player);
	if(player == null) 
		Debug.LogError("Could not find the Player");
		
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