using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * A planet is food for the star, when they touch the planet gives one score point and vanishes into the void.
	 */ 
	public class Planet : Item {

		void OnTriggerEnter2D(Collider2D col) {
			if(col.tag == "Player") {
				player.AddScore();
				Destroy(gameObject);
			}
		}

	}

}