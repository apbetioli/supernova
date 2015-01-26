#pragma strict

var animator : Animator;
var isDead = false;

function Update () {
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
	if(!isDead) {
		Destroy(col.gameObject);
		Die();
	}
}

function Die() {
	isDead = true;
	animator.SetTrigger("Death");
	Handheld.Vibrate();
}
