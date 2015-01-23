#pragma strict

var player : Player;
var totalOffset : float = 10;
var step : float = 0.5;
var counter : float = 0;
var materials : Material[];

function Start () {
	var meshRenderer : MeshRenderer = GetComponent(MeshRenderer);
	meshRenderer.material = materials[Random.Range(0, materials.Length)];

	player = GameObject.Find("Player").GetComponent(Player);
	if(player == null) 
		Debug.LogError("Could not find the Player");
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
