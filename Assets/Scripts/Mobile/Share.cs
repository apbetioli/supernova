using UnityEngine;
using System.Collections;
using System.IO;

namespace Supernova {

public class Share : MonoBehaviour {

	private bool isProcessing = false;

	public void ShareIt(float score) {
		if(!Application.isMobilePlatform)  {
			Debug.LogWarning("Share only available on mobile platform");
			return;
		}
		
		if(!isProcessing)
			StartCoroutine( ShareScreenshot(score) );
	}
	
	public IEnumerator ShareScreenshot(float score)
	{
		// Wait until the frame is rendered
		yield return new WaitForEndOfFrame();

		try {
			isProcessing = true;

			string file = PrintScreen();
			string text = score + " planets in Supernova. Can you get more?";

			#if UNITY_ANDROID
				AndroidShare(file, text);
			#endif

		} finally {
			isProcessing = false;
		}
	}

	string PrintScreen() {
		string file = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		Debug.Log("Generating screenshot at " + file);

		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		screenTexture.Apply();
		byte[] dataToSave = screenTexture.EncodeToPNG();

		File.WriteAllBytes(file, dataToSave);

		return file;
	}

	void AndroidShare(string file, string text) {
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
	}
}

}