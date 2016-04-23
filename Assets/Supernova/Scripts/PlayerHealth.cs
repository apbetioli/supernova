using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * The player health controller
	 */ 
	public class PlayerHealth : MonoBehaviour {
		
		[HideInInspector]
		public bool isDead = false;
		
		// The amount of damage received
		[HideInInspector]
		public int damage = 0;
		
		// The healing counter
		int healing = 0;

		// The damage will be decreased when the healing counter reaches this value
		public int healWhenCollected = 10;

		public void Die() {
			isDead = true;
		}

		public void Heal() {
			if(damage <= 0)
				return;
			
			healing++;
			healing %= healWhenCollected;
			if(healing == 0)
				damage--;
		}

		public void TakeDamage() {
			healing = 0;
			damage++;
		}

	}

}