using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
#if UNITY_ANDROID
	using GooglePlayGames;
#endif

namespace Supernova {

public class GooglePlayLeaderboard : MonoBehaviour {
	
	public void Activate() {
		if(!Application.isMobilePlatform)  {
			Debug.LogWarning("Leaderboard only available on mobile platform");
			return;
		}

		#if UNITY_ANDROID
			PlayGamesPlatform.Activate();
		#endif
	}

	public void Open () {
		if(!Application.isMobilePlatform)  {
			Debug.LogWarning("Leaderboard only available on mobile platform");
			return;
		}

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

}