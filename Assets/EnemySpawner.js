#pragma strict

var enemies : GameObject[];
var wait = 0;

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
	if(wait > 0) {
		wait--;
		return;
	}

	var enemyIndex = Random.Range(0, enemies.Length);
	
	var position = transform.position;
	var rand = Random.Range(-10, 9);
	
	var offset = rand >= 0 ? 1 : -1;
	position.x += offset;
	
	var enemy = Instantiate(enemies[enemyIndex], position, transform.rotation);
	
	wait = Random.Range(1, 2);
	
	return enemy;
}