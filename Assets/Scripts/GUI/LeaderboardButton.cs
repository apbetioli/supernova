using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

namespace Supernova {

	/*
	 * Opens the Google Play Leaderboard when pressed
	 */ 
	public class LeaderboardButton : MonoBehaviour {

		// If true, only show this button when on mobile platform
		public bool onlyMobile = true;

		GooglePlayLeaderboard leaderboard;

		void Awake() {
			leaderboard = GetComponent<GooglePlayLeaderboard>();
			if(leaderboard == null) {
				Debug.LogError("GooglePlayLeaderboard not found");
				enabled = false;
				return;
			}

			gameObject.SetActive(onlyMobile ? Application.isMobilePlatform : true);		
		}

		void Start() {
			leaderboard.Activate();
		}

		public void Open() {
			leaderboard.Open();
		}

	}

}