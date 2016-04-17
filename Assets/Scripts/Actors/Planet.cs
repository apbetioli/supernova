using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * A planet is food for the star. It scores one point.
	 */ 
	public class Planet : Item {

		void OnTriggerEnter2D(Collider2D col) {
			if(col.tag != null && col.tag == "Player") {
				player.AddScore();
				Destroy(gameObject);
			}
		}

	}

}