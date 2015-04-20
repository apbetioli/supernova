#pragma strict

/*
	Desabilita o box collider do objeto para nao lançar mais triggers.
	Necessario por causa do Lerp do Enemy.
*/
function OnTriggerEnter2D(col : Collider2D) {
	var box : BoxCollider2D = col.gameObject.GetComponent(BoxCollider2D);
	box.enabled = false;
}