using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * It provides an interface for accessing the PlayerPrefs
	 */ 
	public class Settings {

		public static bool IsSoundOn() {
			return PlayerPrefs.GetString("sound", "ON") == "ON";
		}

		public static void SetSound(bool on) {
			PlayerPrefs.SetString("sound", on ? "ON" : "OFF");
		}

		public static int GetHighscore() {
			return PlayerPrefs.GetInt("highscore", 0);
		}

		public static void SetHighscore(int highscore) {
			PlayerPrefs.SetInt("highscore", highscore);
		}
	}

}