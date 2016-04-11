using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

namespace Supernova {

public class Services : MonoBehaviour {

	public void Death(int score, string by) {
		#if UNITY_ANALYTICS
			Debug.Log ("Death Analytics :: score: " + score + ", by: " + by);
			
			Analytics.CustomEvent("death", new Dictionary<string, object>
			{
				{ "score", score },
				{ "by", by }
			}); 
		#endif
	}

	public void Authenticate() {
		#if UNITY_ANDROID
			if(Application.isMobilePlatform)
				Social.localUser.Authenticate(success => {
					if (success) {
						Debug.Log ("Authentication successful");
						string userInfo = "Username: " + Social.localUser.userName + 
							"\nUser ID: " + Social.localUser.id + 
							"\nIsUnderage: " + Social.localUser.underage;
						Debug.Log (userInfo);
					}
					else {
						Debug.LogWarning ("Authentication failed");
					}
				});
		#endif
	}

	public void ReportScore(int score) {
		#if UNITY_ANDROID
			if(Application.isMobilePlatform)
				Social.ReportScore(score, GooglePlayConstants.leaderboard_best_starters, success => {});
		#endif
	}

}

}