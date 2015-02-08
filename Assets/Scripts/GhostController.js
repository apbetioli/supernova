#pragma strict

var player : PlayerController;
var originalYPosition : float;
var farFactor : float;
var nearFactor : float;
var distance : float = 5;

function Start () {
	player = GameObject.Find("Player").GetComponent(PlayerController);
	if(player == null) 
		Debug.LogError("Could not find the Player");
	
	originalYPosition = transform.position.y;
}

function Update () {
	FollowPlayer();
	MirrorEffect();

	if(player.isDead || !player.isRunning)
		return;
		
	if(player.CanMove()) {
		ToFar();
	} else {
		ToNear();
	}
}

function Velocity() {
	return Time.deltaTime * player.Level();
}

function ToFar() {
	var aux = farFactor;
	
	var target = transform.position.y - aux;
	
	if(target < originalYPosition)
		target = originalYPosition;
		
	transform.position.y = target;
}

function ToNear() {
	var aux = 0.6 * distance + Velocity();
	
	transform.position.y = transform.position.y + aux;
}

function FollowPlayer() {
	transform.position.x = player.transform.position.x;
}

function MirrorEffect() {
	transform.localScale.x = -1 * transform.position.x;
}
