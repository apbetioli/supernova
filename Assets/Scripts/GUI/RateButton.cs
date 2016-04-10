using UnityEngine;
using System.Collections;

public class RateButton : MonoBehaviour {

	//Enter your google play market URL here
	public string marketURL = "market://details?id=com.cosmicgardenlabs.supernova";

	void Start () {
		#if UNITY_ANDROID
			enabled = true;	
		#else
			enabled = false;	
		#endif
	}

	public void Open() {
		Application.OpenURL(marketURL);
		/* Once the rate is done we can hide the button */
		enabled = false;
	}

}
