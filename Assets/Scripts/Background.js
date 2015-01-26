#pragma strict

var player : Player;
var totalOffset : float;
var step : float;
var counter : float;
var materials : Material[];

function Start () {
	var meshRenderer : MeshRenderer = GetComponent(MeshRenderer);
	meshRenderer.material = materials[Random.Range(0, materials.Length)];
}

function Update () {
	if( counter > 0 ) {
		RollBackground();
		counter -= step;
	} else {
		if(player.CanMove()) {
			counter = totalOffset;
		}
	}
}

function RollBackground() {
	if(player.isDead)
		return;
	
	var mat = renderer.material;
	var offset = mat.mainTextureOffset;
	offset.y += step * Time.deltaTime;
	mat.mainTextureOffset = offset;
}
