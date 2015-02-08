#pragma strict

var player : PlayerController;
var animator : Animator;

function Update () {
	if(player.isDead) 
		animator.SetTrigger("GameOver");
	if(player.isRunning)
		animator.SetTrigger("Start");
}

function Play() {
	Application.LoadLevel(Application.loadedLevel);
	animator.SetTrigger("Menu");
}
