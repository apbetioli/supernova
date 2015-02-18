#pragma strict

var player : PlayerController;
var animator : Animator;
var sound: UI.Toggle;
var gameOverSound : AudioSource;
var backgroundSoundTrack : AudioSource;
var deathSound : AudioSource;
var passbySound : AudioSource;

function Start() {
	sound.isOn = PlayerPrefs.GetString("sound", "ON") == "ON";
	PlayBackgroundSoundTrack();
}

function Update () {	
	if (Input.GetKeyDown(KeyCode.Escape))
		Application.Quit(); 

	if(player.isDead) {
		animator.SetTrigger("GameOver");
		return;
	}
	if(player.score >= player.highscore) {
		animator.SetTrigger("HighScore");
		return;
	}
	if(player.isRunning) {
		animator.SetTrigger("Start");
		return;
	}
}

function Play() {
	Application.LoadLevel(Application.loadedLevel);
}

function PlayBackgroundSoundTrack() {
	if(!sound.isOn)
		return;
	
	backgroundSoundTrack.Play();
}

function PlayDeathSound() {
	if(!sound.isOn)
		return;

	deathSound.Play();
	gameOverSound.Play();
	backgroundSoundTrack.Stop();
}

function PlayPassbySound() {
	if(!sound.isOn)
		return;
		
	passbySound.Play();
}

function ToggleSound() {
	
	PlayerPrefs.SetString("sound", sound.isOn ? "ON" : "OFF");
	
	if(!sound.isOn) {
		backgroundSoundTrack.Stop();
		gameOverSound.Stop();
	} else {
		if(player.isDead)
			gameOverSound.Play();
		else
			backgroundSoundTrack.Play();
	}
}

function AdjustPitch() {
	backgroundSoundTrack.pitch = 1.0 + (player.Level() - 1) / 100.0;
}

function Share() {
	ShareTwitter("Teste", "http://twitter.com", "apbetioli", "en");
}

function ShareTwitter(text, url, related, lang) {
	var address = "http://twitter.com/intent/tweet";

    Application.OpenURL(address +
        "?text=" + WWW.EscapeURL(text) +
        "&amp;url=" + WWW.EscapeURL(url) +
        "&amp;related=" + WWW.EscapeURL(related) +
        "&amp;lang=" + WWW.EscapeURL(lang));
	
}

function PlayNewHighScoreAnimation() {
	animator.SetTrigger("HighScore");
}
