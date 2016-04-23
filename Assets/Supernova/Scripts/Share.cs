using UnityEngine;
using System.Collections;
using System.IO;

namespace Supernova {

	/*
	 * Opens a native window for sharing the game over screen with friends.
	 * Currently works only on Android devices.
	 */
	public class Share : MonoBehaviour {

		private bool isProcessing = false;

		public void ShareIt(float score) {
			if(!isProcessing)
				StartCoroutine( ShareScreenshot(score) );
		}
		
		public IEnumerator ShareScreenshot(float score)
		{
			// Wait until the frame is rendered
			yield return new WaitForEndOfFrame();

			try {
				isProcessing = true;

				string file = Screenshot();
				string text = score + " planets in Supernova. Can you get more?";

				AndroidShare(file, text);

			} finally {
				isProcessing = false;
			}
		}

		// Generates a screenshot of the gameover screen
		string Screenshot() {
			string filename = "Score.png";
			Application.CaptureScreenshot(filename);
			string path = Application.persistentDataPath + "/" + filename;
			Debug.Log("Generated Screenshot at: " + path);
			return path;
		}

		// Opens the native android share window
		void AndroidShare(string file, string text) {
			if(!Application.isMobilePlatform)  {
				Debug.LogWarning("Share only available on Android devices");
				return;
			}

			#if UNITY_ANDROID
				AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
				AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
				intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
				AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
				AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + file);
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text);
				intentObject.Call<AndroidJavaObject>("setType", "image/png");
				AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

				currentActivity.Call("startActivity", intentObject);
			#endif
		}
	}

}