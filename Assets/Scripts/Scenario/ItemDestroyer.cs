using UnityEngine;
using System.Collections;

namespace Supernova {

public class ItemDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag != "Player")
			Destroy(col.gameObject);
	}
}

}