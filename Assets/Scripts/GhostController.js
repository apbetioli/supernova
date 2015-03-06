#pragma strict

var player : PlayerController;
var minYPosition : float;
var farFactor : float;
var nearFactor : float;
var sleep : float;
var ghostAnimator : Animator;
var start : boolean = false;

function Update () {
	FollowPlayer();
	MirrorEffect();

	if(player.isDead || !player.isRunning || !start) 
		return;
		
	if(sleep > 0) {
		sleep -= Velocity();
		transform.position.y -= farFactor * Velocity();
		ghostAnimator.SetTrigger("Sleep");
	} else {
		sleep = 0;
		transform.position.y += Velocity();
		ghostAnimator.SetTrigger("Haunt");
	}
	
	if(transform.position.y < minYPosition) {	
		transform.position.y = minYPosition;
		sleep = 0;
	}
	
}

function Velocity() {
	var acceleration = nearFactor - nearFactor/(1+player.Level());
	return Time.deltaTime * acceleration;
}

function FollowPlayer() {
	transform.position.x = player.transform.position.x;
}

function MirrorEffect() {
	transform.localScale.x = -1 * transform.position.x;
}

function AddScore() {
	sleep++;
	start = true;
}
