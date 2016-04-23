using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * Destroys the items when they are beyond the bottom of the screen
	 */
	public class ItemDestroyer : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D col) {
			// Except for the star when it becomes bigger at it's death
			if(col.name != "Player")
				Destroy(col.gameObject);
		}
	}

}