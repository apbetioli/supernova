#pragma strict

var move = false;
var dead = false;

function Start () {

}

function Update () { 
	if(Input.GetMouseButtonDown(0) && !dead) {
		move = true;
	}
}

function FixedUpdate() {
	if(move) {
		move = false;
		
		var middle = Screen.width/2;
	
		var newSide = Input.mousePosition.x > middle ? 1 : -1;
		var playerSide = transform.position.x > 0 ? 1 : -1;
		
		transform.position.x *= newSide * playerSide;
	}
}

function OnCollisionEnter2D(coll: Collision2D) {
	dead = true;
	Debug.Log("Died");
	//if (coll.gameObject.tag == "Enemy")
	//	coll.gameObject.SendMessage("ApplyDamage", 10);
}
