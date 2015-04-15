#pragma strict

var player : PlayerController;
var enemies : GameObject[];
var collects : GameObject[];

function Start() {
	InitialConfig();
}

function InitialConfig() {
	for(var i = 1; i <= 7; i += 2) {
		var collect = SpawnCollect();
		if(collect) 		
			collect.transform.position.y = i;
	}
}

function SpawnCollect() {
	var position = transform.position;
	position.x += RandomSide();
	
	return RandomSpawn(collects, position);
}

function Spawn() {
	var collect = SpawnCollect();
	if(collect) 		
		SpawnEnemy(-collect.transform.position.x);
}

function OnTouch() {
	if(player.isDead)
		return;
		
	Spawn();
}

function SpawnEnemy(positionx : int) {
	var should = Random.Range(0, 10);
	if(should > 5)
		return;
	
	var position = transform.position;
	position.x = positionx;
	
	return RandomSpawn(enemies, position);
}

function RandomSpawn(array : GameObject[], position) {
	var index = Random.Range(0, array.Length);
	var prefab = array[index];
	return Instantiate(prefab, position, prefab.transform.rotation);
}

function RandomSide() {
	var rand = Random.Range(-10, 9);
	return rand >= 0 ? 2 : -2;
}