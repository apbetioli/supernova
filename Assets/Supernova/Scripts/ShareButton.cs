using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * A button to share the gave over screen with friends.
	 */
	public class ShareButton : MonoBehaviour {

		public Player player;

		// If true, only shows this button when on mobile platform
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