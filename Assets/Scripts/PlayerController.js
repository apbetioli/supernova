#pragma strict

var score : int = 0;
var highscore : int = 0;
var coins : int = 0;
var soundOn : boolean = true;

var playerAnimator : Animator;
var isDead = false;
var isRunning = false;
var gameOverSound : AudioSource;
var backgroundSoundTrack : AudioSource;
var deathSound : AudioSource;
var coinSound : AudioSource;
var passbySound : AudioSource;

function Start() {
	score = 0;
	highscore = PlayerPrefs.GetInt("highscore", 0);
	coins = PlayerPrefs.GetInt("coins", 0);
	
	soundOn = PlayerPrefs.GetString("sound", "ON") == "ON";
	if(soundOn)
		backgroundSoundTrack.Play();
}

function Update () {
	if(CanMove())  {
		if(!isRunning)
			isRunning = true;
		
		ChangeRoadSideOrNot();
	}
}

function CanMove() {
	return Input.GetMouseButtonDown(0) && !isDead;
}

function ChangeRoadSideOrNot() {
	var middle = Screen.width/2;
	
	var newSide = Input.mousePosition.x > middle ? 1 : -1;
	var playerSide = transform.position.x > 0 ? 1 : -1;
		
	transform.position.x *= newSide * playerSide;
}

function OnTriggerEnter2D(col: Collider2D) {
	if(isDead)
		return;
	
	if(col.gameObject.tag == "Enemy") {
		Die();
	}
	
	if(col.gameObject.tag == "Ghost") {
		Die();
	}
	
	if(col.gameObject.tag == "Coin") {
		Destroy(col.gameObject);
		AddCoin();
	}
}

function Die() {
	if(isDead) 
		return;
		
	PlayerPrefs.SetInt("coins", coins);
		
	if(score > highscore) {
		highscore = score;
		PlayerPrefs.SetInt("highscore", score);
	}

	isDead = true;
	playerAnimator.SetTrigger("Death");
	Handheld.Vibrate();
	if(soundOn) {
		deathSound.Play();
		gameOverSound.Play();
		backgroundSoundTrack.Stop();
	}
}

function isIdle() {
	return !Input.GetMouseButtonDown(0);
}

function AddScore () {
	if(isDead) 
		return;

	score++;
	
	if(soundOn) 
		passbySound.Play();
	
	AdjustPitch();
}

function AddCoin() {
	if(isDead) 
		return;

	coins++;
	
	if(soundOn) 
		coinSound.Play();
}

function Level() {
	return 1 + score / 10;
}

function AdjustPitch() {
	backgroundSoundTrack.pitch = 1.0 + (Level() - 1) / 100.0;
}

function ToggleSound() {
	soundOn = !soundOn;
	
	PlayerPrefs.SetString("sound", soundOn ? "ON" : "OFF");
	
	if(!soundOn) {
		backgroundSoundTrack.Stop();
		gameOverSound.Stop();
	} else {
		if(isDead)
			gameOverSound.Play();
		else
			backgroundSoundTrack.Play();
	}
}

