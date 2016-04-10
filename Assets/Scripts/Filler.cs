using UnityEngine;
using System.Collections;

public class Filler : Item {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			player.Missed("Filler");
		}
	}

}
