#pragma strict

var player : Player;
var step = 2;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
}

function Update () {
	if(player.CanMove()) {
		Move();
	}
}

function Move() {
	transform.position.y -= step;
}

function OnTriggerEnter2D(col : Collider2D) {
	if (col.gameObject.tag == "Player") {
		Debug.Log("Collided with player");
		
	} else if (col.gameObject.tag == "Finish") {
		Destroy(gameObject);
	}
}