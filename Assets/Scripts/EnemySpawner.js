#pragma strict

var enemies : GameObject[];
var turnsToWait : int = 0;
var idleSpawnTime : float;
var idleCounter :float;

var player : Player;

function Update () {
	if(player.CanMove()) {
		Spawn();
	}

	if(player.isDead) {
		if(idleCounter >= idleSpawnTime) {
			idleCounter = 0;
			Spawn();
		} else {
			idleCounter += Time.deltaTime;
		}
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
	Instantiate(enemies[enemyIndex], position, transform.rotation);
	
	turnsToWait = Random.Range(1, 3);
}