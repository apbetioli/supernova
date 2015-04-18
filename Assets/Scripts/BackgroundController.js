#pragma strict

var player : PlayerController;
var velocity : float;
var material : Material;
var randomDirection : boolean;
var directionX : int = 1;
var directionY : int = 1;

function Start() {
	material = GetComponent.<Renderer>().material;
	var offset = material.mainTextureOffset;
	offset.x = Random.Range(0.0, 1.0);
	offset.y = Random.Range(0.0, 1.0);
	material.mainTextureOffset = offset;
	
	if(randomDirection) {
		directionX = RandomSide();
		directionY = RandomSide();
	}
}

function RandomSide() {
	var rand = Random.Range(-10, 9);
	return rand >= 0 ? 1 : -1;
}

function Update () {
	var offset = material.mainTextureOffset;
	offset.x = Mathf.Lerp(offset.x, offset.x + velocity*directionX, Time.deltaTime);
	offset.y = Mathf.Lerp(offset.y, offset.y + velocity*directionY, Time.deltaTime);
	material.mainTextureOffset = offset;
}
