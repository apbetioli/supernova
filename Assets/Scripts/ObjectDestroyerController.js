#pragma strict

function OnTriggerEnter2D(col : Collider2D) {
	Destroy(col.gameObject);
}