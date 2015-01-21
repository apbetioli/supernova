#pragma strict

var enemies : GameObject[];
var turnsToWait = 0;

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
	if(turnsToWait > 0) {
		turnsToWait--;
		return;
	}
	
	var position = transform.position;
	
	var rand = Random.Range(-10, 9);
	var side = rand >= 0 ? 1 : -1;
	
	position.x += side;
	
	var enemyIndex = Random.Range(0, enemies.Length);
	var enemy = Instantiate(enemies[enemyIndex], position, transform.rotation);
	
	turnsToWait = Random.Range(1, 2);
	
	return enemy;
}