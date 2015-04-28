using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class LeaderboardButtonController : MonoBehaviour {

	public void Start() {
		PlayGamesPlatform.Activate();
	}

	public void Open () {
		Social.localUser.Authenticate ((bool success) => {
			if (success)
				PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI-K6jy4kWEAIQBw");
		});
	}

}
