#pragma strict

var player : PlayerController;
var enemies : GameObject[];
var collects : GameObject[];
var filler: GameObject;
var x: Transform;

function Start() {
	InitialConfig();
}

function InitialConfig() {
	for(var i = player.transform.position.y+2; i < transform.position.y+2; i += 2) {
		var collect = SpawnCollect();
		collect.transform.position.y = i;
		
		var fill = SpawnFiller(-collect.transform.position.x);
		fill.transform.position.y = i;
	}
}

function SpawnCollect() {
	var position = transform.position;
	position.x += RandomSide();
	
	var collect = RandomSpawn(collects, position);
	return collect;
}

function SpawnFiller(positionx) {
	var position = transform.position;
	position.x = positionx;

	return Instantiate(filler, position, filler.transform.rotation); 
}

function Spawn() {
	var collect = SpawnCollect();
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
		return SpawnFiller(positionx);
	
	var position = transform.position;
	position.x = positionx;
	
	return RandomSpawn(enemies, position);
}

function RandomSpawn(array : GameObject[], position) {
	var index = Random.Range(0, array.Length);
	var prefab = array[index];
	var instance = Instantiate(prefab, position, prefab.transform.rotation);
	var scale = Random.Range(0.3, 1.0);
	instance.transform.localScale.x = scale;
	instance.transform.localScale.y = scale;
	return instance;
}

function RandomSide() {
	var rand = Random.Range(-10, 9);
	return rand >= 0 ? 2 : -2;
}