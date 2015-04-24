using UnityEngine;
using System.Collections;
using System.IO;

// www.daniel4d.com
public class Share : MonoBehaviour {

	private bool isProcessing = false;

	public void ShareIt(float score) {
		if(!isProcessing)
			StartCoroutine( ShareScreenshot(score) );
	}
	
	public IEnumerator ShareScreenshot(float score)
	{
		isProcessing = true;
		
		// wait for graphics to render
		yield return new WaitForEndOfFrame();

		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		screenTexture.Apply();

		byte[] dataToSave = screenTexture.EncodeToPNG();
		
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		Debug.Log(destination);
		
		File.WriteAllBytes(destination, dataToSave);
		
		if(!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), score + " planets in Supernova. Can you get more? https://play.google.com/store/apps/details?id=com.cosmicgardenlabs.supernova");
			//intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;
	}
}
