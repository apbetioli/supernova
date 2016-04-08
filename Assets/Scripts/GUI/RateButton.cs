using UnityEngine;
using System.Collections;

public class RateButton : MonoBehaviour {

	//Enter your google play market URL here
	public string marketURL = "market://details?id=com.cosmicgardenlabs.supernova";

	void Start () {
		enabled = PlayerPrefs.GetInt("ShowRate", 1) == 0;
	}

	public void Open() {
		Application.OpenURL(marketURL);
		PlayerPrefs.SetInt("ShowRate", 0);
		enabled = false;
	}


}
