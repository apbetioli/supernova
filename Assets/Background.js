#pragma strict

var velocity : float = 12;

var player : Player;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
}

function Update () {

	if(player.CanMove()) {
		var mat = renderer.material;
		var offset = mat.mainTextureOffset;
		offset.y += Time.deltaTime * velocity;
		mat.mainTextureOffset = offset;
	}
}