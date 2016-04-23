using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

namespace Supernova {

	/*
	 * Controls the interaction with Google play leaderboard
	 */
	public class Leaderboard : MonoBehaviour {

		public void Authenticate() {
			#if UNITY_ANDROID
				if(Application.isMobilePlatform)
					Social.localUser.Authenticate(success => {
						if (success) {
							Debug.Log ("Authentication successful");
							Debug.Log ("Username: " + Social.localUser.userName + "\nUser ID: " + Social.localUser.id + "\nIsUnderage: " + Social.localUser.underage);
						}
						else {
							Debug.LogWarning ("Authentication failed");
						}
					});
			#endif
		}
		
		public void ReportScore(int score) {
			if(!Application.isMobilePlatform)  {
				Debug.LogWarning("Report score only available on mobile platform");
				return;
			}

			#if UNITY_ANDROID
				if(Application.isMobilePlatform)
					Social.ReportScore(score, GPGSIds.leaderboard_best_starters, success => {});
			#endif
		}

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
						PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_best_starters);

					} else {
						Debug.LogError("Could not authenticate with leaderboard");
					}
				});
			#endif
		}
	}

}