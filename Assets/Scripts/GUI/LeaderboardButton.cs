using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class LeaderboardButton : MonoBehaviour {

	GooglePlayLeaderboard leaderboard;

	void Awake() {
		leaderboard = GetComponent<GooglePlayLeaderboard>();
		if(leaderboard == null) {
			Debug.LogError("GooglePlayLeaderboard not found");
			enabled = false;
			return;
		}

		enabled = Application.isMobilePlatform;		
	}

	void Start() {
		leaderboard.Activate();
	}

	public void Open() {
		leaderboard.Open();
	}

}
