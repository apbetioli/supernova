#pragma strict

var velocity : float = 10;

function Start () {

}

function Update () {

	if(Input.GetMouseButtonDown(0)) {
		var mat = renderer.material;
		var offset = mat.mainTextureOffset;
		offset.y += Time.deltaTime * velocity;
		mat.mainTextureOffset = offset;
	}
}