using UnityEngine;
using System.Collections;

namespace Supernova {

public class RateButton : MonoBehaviour {

	//Enter your google play market URL here
	public string marketURL = "market://details?id=com.cosmicgardenlabs.supernova";
	public bool onlyMobile = true;

	void Awake () {
		#if UNITY_ANDROID
			gameObject.SetActive(onlyMobile ? Application.isMobilePlatform : true);	
		#else
			gameObject.SetActive(false);	
		#endif
	}

	public void Open() {
		Application.OpenURL(marketURL);
		/* Once the rate is done we can hide the button */
		gameObject.SetActive(false);	
	}

}

}