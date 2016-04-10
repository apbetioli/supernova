using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int healWhenCollected = 10;

	[HideInInspector]
	public bool isDead;

	[HideInInspector]
	public int missedPlanets = 0;
	
	int heal = 0;

	void Start() {
		isDead = false;
	}

	public void Die(string by) {
		isDead = true;
	}

	public void Heal() {
		heal++;
		heal %= healWhenCollected;
		
		if(heal == 0 && missedPlanets > 0) {
			missedPlanets--;
		}
	}

	public void Missed(string by) {
		heal = 0;
		missedPlanets++;
	}

}
