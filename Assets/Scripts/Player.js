#pragma strict

var playerAnimator : Animator;
var isDead = false;
var running = false;

function Update () {
	if(!running) {
		if(Input.GetMouseButtonDown(0))
			running = true;
		return;
	}

	if(CanMove()) 
		ChangeRoadSideOrNot();
}

function CanMove() {
	return Input.GetMouseButtonDown(0) && !isDead;
}

function ChangeRoadSideOrNot() {
	var middle = Screen.width/2;
	
	var newSide = Input.mousePosition.x > middle ? 1 : -1;
	var playerSide = transform.position.x > 0 ? 1 : -1;
		
	transform.position.x *= newSide * playerSide;
}

function OnTriggerEnter2D(col: Collider2D) {
	Debug.Log(col.gameObject.name);
	if(!isDead && col.gameObject.tag == "Enemy") {
		Destroy(col.gameObject);
		Die();
	}
}

function Die() {
	isDead = true;
	playerAnimator.SetTrigger("Death");
	Handheld.Vibrate();
}

function isIdle() {
	return !Input.GetMouseButtonDown(0);
}
