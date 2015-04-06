using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	void Start () {
		
		const string projectId = "ed94333c-51d7-4d35-9f5c-faeacb54e4f6";
		UnityAnalytics.StartSDK (projectId);
		
	}

}