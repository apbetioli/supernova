using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * The filler is just an empty space, no sprites, but has collider, and is used to damage the player.
	 */ 
	public class Filler : Item {

		void OnTriggerEnter2D(Collider2D col) {
			if(col.tag != null && col.tag == "Player") {
				player.TakeDamage("Filler");
			}
		}

	}

}