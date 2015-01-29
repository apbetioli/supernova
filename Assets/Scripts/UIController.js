#pragma strict

var animator : Animator;
var player : Player;

function Update () {
	if(player.isDead) 
		animator.SetTrigger("GameOver");
	if(player.running)
		animator.SetTrigger("Start");
}

function Play() {
	Application.LoadLevel(Application.loadedLevel);
	animator.SetTrigger("Menu");
}