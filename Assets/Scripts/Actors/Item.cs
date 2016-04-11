using UnityEngine;
using System.Collections;

namespace Supernova {

public abstract class Item : MonoBehaviour {

	public float step = 1f;
	public float rotationVelocity = 0.2f;

	protected Player player;
	float yPosition;

	void Awake() {
		player = FindObjectOfType<Player>();
		if(player == null) {
			Debug.LogError("Could not find the Player");
			enabled = false;
		}
	}
		
	void Start () {
		yPosition = transform.position.y;
	}

	void Update () {
		transform.Rotate(0, 0, -rotationVelocity * Time.deltaTime * transform.position.x);

		Vector2 pos = transform.position;
		pos.y = Mathf.Lerp(pos.y, yPosition, 5 * step * Time.deltaTime);
		transform.position = pos;
	}

	void OnTouch() {
		if(!player.IsDead())
			yPosition = yPosition - step;
	}

}

}