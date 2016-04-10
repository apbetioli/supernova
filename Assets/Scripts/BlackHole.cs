using UnityEngine;
using System.Collections;

public class BlackHole : Item {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			player.Die("BlackHole");
		}
	}

}
