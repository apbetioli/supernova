#pragma strict

var enemies : GameObject[];
var wait = false;

var player : Player;

function Start () {
	player = GameObject.Find("Player").GetComponent(Player);
}

function Update () {
	if(player.CanMove()) {
		Spawn();
	}
}

function Spawn() {
	if(wait) {
		wait = false;
		return;
	}

	var enemyIndex = Random.Range(0, enemies.Length);
	
	var position = transform.position;
	var rand = Random.Range(0, 10);
	
	var offset = rand > 5 ? 1 : -1;
	position.x += offset;
	
	var enemy = Instantiate(enemies[enemyIndex], position, transform.rotation);
	
	wait = true;
	
	return enemy;
}