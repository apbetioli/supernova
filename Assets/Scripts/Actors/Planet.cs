using UnityEngine;
using System.Collections;

namespace Supernova {

public class Planet : Item {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			player.AddScore();
			Destroy(gameObject);
		}
	}

}

}