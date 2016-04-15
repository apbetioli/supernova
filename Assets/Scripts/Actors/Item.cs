using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * An item is any object that scrolls down from the top of the scene.
	 */ 
	public abstract class Item : MonoBehaviour {

		// It has a step that is the offset to move in the Y axis, each time the player touches the screen.
		public float step = 1f;

		// The rotation velocity is constant and rotates this item
		public float rotationVelocity = 0.2f;

		protected Player player;

		// A variable to control the movement in the Y axis
		float targetPosition;

		void Awake() {
			// Tries to find the Player anywhere in the hierarchy
			player = FindObjectOfType<Player>();
			if(player == null) {
				Debug.LogError("Could not find the Player");
				enabled = false;
			}
		}
			
		void Start() {
			// Initializes the targetPosition with its own position
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
			//Every time a move is made, the targetPosition is updated
			if(!player.IsDead())
				targetPosition = targetPosition - step;
		}

	}

}