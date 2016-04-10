using UnityEngine;
using System.Collections;

public class ShareButton : MonoBehaviour {

	public Player player;

	public void Open () {
		#if UNITY_ANDROID
			enabled = true;
			Share shareCs = GetComponent<Share>();
			shareCs.ShareIt(player.Score());
		#else
			enabled = false;
		#endif
	}
}
