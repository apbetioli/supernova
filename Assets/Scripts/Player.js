#pragma strict

var animator : Animator;
var isDead = false;
var deathCooldown : float = 0.5;

function Start () {
	animator = transform.GetComponentInChildren(Animator);
	if(animator == null)
		Debug.LogError("Could not find the animator");
}
function Update () { 
	if(isDead) {
		deathCooldown -= Time.deltaTime;
		if(deathCooldown < 0 && Input.GetMouseButtonDown(0)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	} else {
		if(CanMove())
			ChangeRoadSideOrNot();
	}
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
