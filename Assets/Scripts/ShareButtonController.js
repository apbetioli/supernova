#pragma strict

var player : PlayerController;

function Share () {
	var shareCs : Share = this.GetComponent("Share");
	shareCs.ShareIt(player.score);
}