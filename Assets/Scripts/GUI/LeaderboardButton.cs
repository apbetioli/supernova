using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
#if UNITY_ANDROID
	using GooglePlayGames;
#endif

public class LeaderboardButton : MonoBehaviour {

	public void Start() {
		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
		#endif
	}

	public void Open () {
		#if UNITY_ANDROID
			Social.localUser.Authenticate (success => {
				if (success) {
					PlayGamesPlatform.Instance.ShowLeaderboardUI(GooglePlayConstants.leaderboard_best_starters);

				} else {
					Debug.LogError("Could not authenticate with leaderboard");
				}
			});
		#endif
	}

}
