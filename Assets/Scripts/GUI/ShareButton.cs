using UnityEngine;
using System.Collections;

namespace Supernova {

public class ShareButton : MonoBehaviour {

	public Player player;
	public bool onlyMobile = true;

	void Awake() {
		gameObject.SetActive(onlyMobile ? Application.isMobilePlatform : true);		
	}

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

}