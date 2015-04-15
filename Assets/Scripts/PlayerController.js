#pragma strict

var score : int = 0;
var highscore : int = 0;
var initialLevel: int = 1;
var playerAnimator : Animator;
var isDead = false;
var isRunning = false;
var ui : UIController;
var scoreCounterController: ScoreCounterController;
var ghost : GhostController;
var analytics : CustomEvents;

function Start() {
	score = 0;
	highscore = PlayerPrefs.GetInt("highscore", 0);
	analytics = this.GetComponent("CustomEvents");
}

function OnTouch() {
	if(isDead)
		return;
	
	if(!isRunning)
		isRunning = true;
	
	ChangeRoadSideOrNot();
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
		Die("Enemy");
		return;
	}
	
	if(col.gameObject.tag == "Ghost") {
		Die("Ghost");
		return;
	}
	
	if(col.gameObject.tag == "Collect") {
		AddScore();
		Destroy(col.gameObject);
		return;
	}
}

function Die(by) {
	if(isDead) 
		return;
		
	analytics.Death(score, by);
		
	if(score > highscore) {
		highscore = score;
		PlayerPrefs.SetInt("highscore", score);
	}

	isDead = true;
	playerAnimator.SetTrigger("Death");
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
	return initialLevel + score / 10;
}

