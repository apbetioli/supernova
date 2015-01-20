#pragma strict

var canMove = false;
var isDead = false;
var deathCooldown = 0.5;

var scoreController : ScoreController;

function Start () {
	scoreController = GameObject.Find("Score").GetComponent(ScoreController);
}
function Update () { 
	if(isDead) {
		deathCooldown -= Time.deltaTime;
		if(deathCooldown < 0 && Input.GetMouseButtonDown(0)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	} else {
		canMove = CanMove();
	}
}

function FixedUpdate() {
	if(canMove) {
		canMove = false;
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
	
	scoreController.Add();
}

function OnTriggerEnter2D(col: Collider2D) {
	isDead = true;
	Debug.Log("Game Over");
	Destroy(col.gameObject);
}
