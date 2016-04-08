using UnityEngine;
using System.Collections;

public class ShareButton : MonoBehaviour {

	public Player player;

	public void Open () {
		#if UNITY_ANDROID
			Share shareCs = GetComponent<Share>();
			shareCs.ShareIt(player.score);
		#endif
	}
}
