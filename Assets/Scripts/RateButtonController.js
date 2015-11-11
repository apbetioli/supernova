#pragma strict

var active = false;

function Start () {
	this.active = PlayerPrefs.GetInt("ShowRate", 1) == 0;
}

function Update() {
	if(this.active)
		this.active = Random.Range(0, 10) >= 5;
}

function Rate() {
	Application.OpenURL("market://details?id=com.cosmicgardenlabs.supernova");
	PlayerPrefs.SetInt("ShowRate", 0);
	this.active = false;
}

