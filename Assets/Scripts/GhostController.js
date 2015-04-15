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

	if(player.isDead || !player.isRunning) 
		return;
		
	if(sleep > 0) {
		sleep -= nearFactor * Velocity();
		transform.position.y -= farFactor * Velocity();
		ghostAnimator.SetTrigger("Sleep");
	} else {
		sleep = 0;
		transform.position.y += Velocity();
		ghostAnimator.SetTrigger("Haunt");
	}
	
	if(transform.position.y < minYPosition) 
		transform.position.y = minYPosition;
	
}

function Acceleration() {
	return nearFactor - nearFactor/(1+player.Level());
}

function Velocity() {
	return Time.deltaTime * Acceleration();
}

function FollowPlayer() {
	transform.position.x = player.transform.position.x;
}

function MirrorEffect() {
	//transform.localScale.x = -1 * transform.position.x;
}

function OnTouch() {
	sleep = Mathf.Min(sleep+1, Acceleration()); 
}

function AddScore() {
	start = true;
}
