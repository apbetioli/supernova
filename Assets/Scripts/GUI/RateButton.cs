using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * Opens the Market URL when pressed for the user to rate the app
	 */ 
	public class RateButton : MonoBehaviour {

		//Enter your google play market URL here
		public string marketURL = "market://details?id=com.cosmicgardenlabs.supernova";

		// If true, only show this button when on mobile platform
		public bool onlyMobile = true;

		// Until now it only supports google play
		void Awake () {
			#if UNITY_ANDROID
				gameObject.SetActive(onlyMobile ? Application.isMobilePlatform : true);	
			#else
				gameObject.SetActive(false);	
			#endif
		}

		public void Open() {
			Application.OpenURL(marketURL);
			// Once the rating is done the button can be hidden
			gameObject.SetActive(false);	
		}

	}

}