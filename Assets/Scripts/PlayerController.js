#pragma strict

var score : int = 0;
var highscore : int = 0;
var initialLevel: int = 1;
var missedPlanets: int = 0;
var playerAnimator : Animator;
var isDead = false;
var isRunning = false;
var ui : UIController;
var analytics : CustomEvents;
var idle : float = 0;
var recover : int = 0;
var targetPositionX : float;
var levelScore : int;

var authenticated : boolean;

function Start() {
	score = 0;
	highscore = PlayerPrefs.GetInt("highscore", 0);
	analytics = this.GetComponent("CustomEvents");
	targetPositionX = transform.position.x;
	
	Social.localUser.Authenticate(function(success){
		authenticated = success;
	});
}

function Update() {
	if(isDead || !isRunning)
		return;
		
	if (idle >= 1.5) {
		Missed("Timeout");
		idle = 0;
	}
		
	idle += Time.deltaTime;
	
	transform.position.x = Mathf.Lerp(transform.position.x, targetPositionX, 25 * Time.deltaTime);
}

function OnTouch() {
	if(isDead)
		return;
		
	if(!isRunning)
		isRunning = true;
	
	idle = 0;
	
	ChangeRoadSideOrNot();
}

function ChangeRoadSideOrNot() {

	var middle = Screen.width/2;
	
	var newSide = Input.mousePosition.x > middle ? 1 : -1;
	var playerSide = transform.position.x > 0 ? 1 : -1;
		
	var multiplier = newSide * playerSide;
	
	targetPositionX *= multiplier;
}

function OnTriggerEnter2D(col: Collider2D) {
	if(col.gameObject.tag == "Enemy") {
		Die("Enemy");
		return;
	}

	if(col.gameObject.tag == "Collect") {
		AddScore();
		Destroy(col.gameObject);
		return;
	}
	
	if(col.gameObject.tag == "Filler") {
		Missed("Missed");
		return;
	}
}

function Missed(by) {
	ui.PlayMissedSound();

	recover = 0;
	missedPlanets++;
	
	TriggerMissed(by);
}

function TriggerMissed(by) {
	if(missedPlanets == 0)
		playerAnimator.SetTrigger("White");
	else if(missedPlanets == 1)
		playerAnimator.SetTrigger("Yellow");
	else if(missedPlanets == 2)
		playerAnimator.SetTrigger("Red");
	else	
		Die(by);
}

function Die(by) {
	if(isDead) 
		return;
		
	isDead = true;
	playerAnimator.SetTrigger("Supernova");
	
	if(score > highscore) {
		highscore = score;
		PlayerPrefs.SetInt("highscore", score);
	}
		
	analytics.Death(score, by);

	updateDeathStatistics(by);
	updateTotalScore();

	if(authenticated) {
		Social.ReportScore(highscore, "CgkI-K6jy4kWEAIQBw", function(success){
		});
	}
}

function updateDeathStatistics(by) {
	var deaths = PlayerPrefs.GetInt("DeathBy" + by, 0);
	PlayerPrefs.SetInt("DeathBy" + by, deaths+1);
}

function updateTotalScore() {
	var totalScore = PlayerPrefs.GetInt("totalscore", 0) + score;
	PlayerPrefs.SetInt("totalscore", totalScore);
	Debug.Log("Total score: " + totalScore);
}

function isIdle() {
	return !Input.GetMouseButtonDown(0);
}

function AddScore () {
	if(isDead) 
		return;

	score++;
	
	Heal();		
	
	ui.AdjustPitch();
	ui.PlayScoreSound();
}

function Level() {
	return initialLevel + score / levelScore;
}

function LevelUp() {
	var mod = (score % levelScore) == 0;
	return Level() > 1 && mod;
}

function Heal() {
	recover++;
	
	if(recover == 4+Level() && missedPlanets > 0) {
		recover = 0;
		missedPlanets--;
		TriggerMissed("Heal");
		ui.PlayMissedSound();
	}
}
