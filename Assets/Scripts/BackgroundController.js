#pragma strict

var player : PlayerController;
var totalOffset : float;
var step : float;
var counter : float;
var idleStep : float;
var materials : Material[];

function Start () {
	/*
	var meshRenderer : MeshRenderer = GetComponent(MeshRenderer);
	meshRenderer.material = materials[Random.Range(0, materials.Length)];
	*/
}

function Update () {
	if( counter > 0 ) {
		RollBackground(step);
		counter -= step;
	}
	
	RollBackground(idleStep * Time.deltaTime);
}

function OnTouch() {
	if(player.isDead)
		return;
		
	counter = totalOffset;
}

function RollBackground(rollStep : float) {
	var mat = GetComponent.<Renderer>().material;
	var offset = mat.mainTextureOffset;
	offset.y += rollStep;
	offset.y = offset.y % 1;
	mat.mainTextureOffset = offset;
}
