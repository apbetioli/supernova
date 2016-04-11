using UnityEngine;
using System.Collections;

namespace Supernova {

public class Settings {

	public static bool IsSoundOn() {
		return PlayerPrefs.GetString("sound", "ON") == "ON";
	}

	public static void SetSound(bool on) {
		PlayerPrefs.SetString("sound", on ? "ON" : "OFF");
	}

}

}