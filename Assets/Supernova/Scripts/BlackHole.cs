using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * The BlackHole is the worst enemy of the star. If the star touches it, it's death certainly.
	 */
	public class BlackHole : Item {

		void OnTriggerEnter2D(Collider2D col) {
			if(col.name == "Player") {
				player.Die("BlackHole");
			}
		}

	}

}