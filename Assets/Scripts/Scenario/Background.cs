using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * Controls the scrolling backgrounds
	 */ 
	[RequireComponent(typeof(Material))]
	public class Background : MonoBehaviour {

		// The velocity of the scrolling
		public float velocity = 0.01f;

		// If true, will choose a random direction every play
		public bool randomDirection = true;

		// Else, will follow this direction
		public Vector2 direction = new Vector2(1, 1);

		// The background texture material
		Material material;

		void Awake() {
			material = GetComponent<Renderer>().material;
		}

		void Start() {
			RandomizeTextureOffset();

			if(randomDirection) 
				RandomizeDirection();
		}

		// Starts in a new position
		void RandomizeTextureOffset() {
			Vector2 offset = material.mainTextureOffset;
			offset.x = Random.Range(0f, 1f);
			offset.y = Random.Range(0f, 1f);
			material.mainTextureOffset = offset;
		}

		// Scrolls in a random direction
		void RandomizeDirection() {
			direction = new Vector2(RandomValue(), RandomValue());
		}

		int RandomValue() {
			int rand = Random.Range(-10, 9);
			return rand >= 0 ? 1 : -1;
		}

		void Update () {
			MoveTexture();
		}

		void MoveTexture() {
			Vector2 offset = material.mainTextureOffset;
			offset.x = Mathf.Lerp(offset.x, offset.x + velocity*direction.x, Time.deltaTime);
			offset.y = Mathf.Lerp(offset.y, offset.y + velocity*direction.y, Time.deltaTime);
			material.mainTextureOffset = offset;
		}
	}

}