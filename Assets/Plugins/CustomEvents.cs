using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Cloud.Analytics;

public class CustomEvents : MonoBehaviour {

	public void Death(int score, string by) {
		Debug.Log ("Death Analytics :: score: " + score + ", by: " + by);
		UnityAnalytics.CustomEvent("death", new Dictionary<string, object>
		{
			{ "score", score },
			{ "by", by }
		}); 
	}

}

