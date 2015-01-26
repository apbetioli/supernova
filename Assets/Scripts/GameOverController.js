#pragma strict

var animator : Animator;
var player : Player;
var deathCooldown : float;

function Update () {
	if(player.isDead) {
		animator.SetTrigger("GameOver");
		
		deathCooldown -= Time.deltaTime;
		if(deathCooldown < 0 && Input.GetMouseButtonDown(0)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}