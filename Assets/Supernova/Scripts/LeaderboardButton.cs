using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

namespace Supernova {

	/*
	 * Opens the Leaderboard when pressed
	 */ 
	public class LeaderboardButton : MonoBehaviour {

		// If true, only shows this button when on mobile platform
		public bool onlyMobile = true;

		Leaderboard leaderboard;

		void Awake() {
			leaderboard = GetComponent<Leaderboard>();
			if(leaderboard == null) {
				Debug.LogError("Leaderboard not found");
				enabled = false;
				return;
			}

			gameObject.SetActive(onlyMobile ? Application.isMobilePlatform : true);		
		}

		public void Open() {
			leaderboard.Open();
		}

	}

}