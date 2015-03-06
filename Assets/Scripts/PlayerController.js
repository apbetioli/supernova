#pragma strict

var score : int = 0;
var highscore : int = 0;
var soundOn : boolean = true;
var initialLevel: int = 1;
var playerAnimator : Animator;
var isDead = false;
var isRunning = false;
var ui : UIController;
var scoreCounterController: ScoreCounterController;
var ghost : GhostController;

function Start() {
	score = 0;
	highscore = PlayerPrefs.GetInt("highscore", 0);
}

function OnTouch() {
	if(isDead)
		return;
	
	if(!isRunning)
		isRunning = true;
	
	ChangeRoadSideOrNot();
	AddScore();
}

function ChangeRoadSideOrNot() {

	var middle = Screen.width/2;
	
	var newSide = Input.mousePosition.x > middle ? 1 : -1;
	var playerSide = transform.position.x > 0 ? 1 : -1;
		
	var multiplier = newSide * playerSide;
	
	transform.position.x *= multiplier;
	
	scoreCounterController.transform.position.x = -transform.position.x;
}

function OnTriggerEnter2D(col: Collider2D) {
	if(isDead)
		return;
	
	if(col.gameObject.tag == "Enemy") {
		Die();
		return;
	}
	
	if(col.gameObject.tag == "Ghost") {
		Die();
		return;
	}
}

function Die() {
	if(isDead) 
		return;
		
	if(score > highscore) {
		highscore = score;
		PlayerPrefs.SetInt("highscore", score);
	}

	isDead = true;
	playerAnimator.SetTrigger("Death");
	
	ui.PlayDeathSound();
}

function isIdle() {
	return !Input.GetMouseButtonDown(0);
}

function AddScore () {
	if(isDead) 
		return;

	score++;
	
	ui.AdjustPitch();
}

function Level() {
	return initialLevel + score / 20;
}

