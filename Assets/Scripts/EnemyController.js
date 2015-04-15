#pragma strict

var player : PlayerController;
var step : float;
var counter : float = 0;
var positiony : float;

function Start () {
	player = GameObject.Find("Player").GetComponent(PlayerController);
	if(player == null) 
		Debug.LogError("Could not find the Player");
		
	positiony = transform.position.y;
}

function Update () {
	if(Input.GetMouseButtonDown(0) && !player.isDead)
		positiony = positiony - step;
	
	transform.position.y = Mathf.Lerp(transform.position.y, positiony, 10 * step * Time.deltaTime);
}

