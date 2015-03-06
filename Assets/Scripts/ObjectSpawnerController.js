#pragma strict

var player : PlayerController;
var enemies : GameObject[];
var turnsToWait : int = 0;
var idleSpawnTime : float;
var idleCounter :float;

function Start() {
	InitialConfig();
}

function InitialConfig() {
	for(var i = 5; i < 8; i += 3) {
		turnsToWait = 0;
		var enemy = SpawnEnemy();
		if(enemy) 		
			enemy.transform.position.y = i;
	}
}

function Update() {
	if(player.isDead) {
		TimedSpawn();
		return;
	}
}

function OnTouch() {
	if(player.isDead)
		return;
		
	Spawn();
}

function TimedSpawn() {
	if(idleCounter >= idleSpawnTime) {
		idleCounter = 0;
		Spawn();
	} else {
		idleCounter += Time.deltaTime;
	}
}

function Spawn() {
	SpawnEnemy();
}

function SpawnEnemy() {
	if(turnsToWait > 0) {
		turnsToWait--;
		return;
	}
	
	var position = transform.position;
	position.x += RandomSide();
	
	var enemyIndex = Random.Range(0, enemies.Length);
	var enemy = Instantiate(enemies[enemyIndex], position, transform.rotation);
	
	turnsToWait = Random.Range(1, 3);
	
	return enemy;
}

function RandomSide() {
	var rand = Random.Range(-10, 9);
	return rand >= 0 ? 1.2 : -1.2;
}