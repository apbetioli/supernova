using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * The BlackHole is the enemy of the supernova. If the star touches it, it's certainly death.
	 */
	public class BlackHole : Item {

		void OnTriggerEnter2D(Collider2D col) {
			if(col.tag == "Player") {
				player.Die("BlackHole");
			}
		}

	}

}