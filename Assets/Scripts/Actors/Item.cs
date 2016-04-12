using UnityEngine;
using System.Collections;

namespace Supernova {

public abstract class Item : MonoBehaviour {

	public float step = 1f;
	public float rotationVelocity = 0.2f;

	protected Player player;
	float targetPosition;

	void Awake() {
		player = FindObjectOfType<Player>();
		if(player == null) {
			Debug.LogError("Could not find the Player");
			enabled = false;
		}
	}
		
	void Start() {
		targetPosition = transform.position.y;
	}

	void Update() {
		Rotate();
		Move();
	}

	void Rotate() {
		transform.Rotate(0, 0, -rotationVelocity * Time.deltaTime * transform.position.x);
	}

	void Move() {
		Vector2 pos = transform.position;
		pos.y = Mathf.Lerp (pos.y, targetPosition, 5 * step * Time.deltaTime);
		transform.position = pos;
	}

	void OnTouch() {
		if(!player.IsDead())
			targetPosition = targetPosition - step;
	}

}

}