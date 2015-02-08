#pragma strict

var player : PlayerController;
var enemies : GameObject[];
var coins : GameObject[];
var turnsToWait : int = 0;
var idleSpawnTime : float;
var idleCounter :float;

function Update () {
	if(player.CanMove()) {
		Spawn();
	}

	if(player.isDead) {
		TimedSpawn();
	}
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
	if(!SpawnEnemy())
		SpawnCoin();
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

function SpawnCoin() {

	if(Random.Range(-10, 9) < 8)
		return;
	
	var position = transform.position;
	position.x += RandomSide();
	
	var coinsIndex = Random.Range(0, coins.Length);
	Instantiate(coins[coinsIndex], position, transform.rotation);
}

function RandomSide() {
	var rand = Random.Range(-10, 9);
	return rand >= 0 ? 1 : -1;
}