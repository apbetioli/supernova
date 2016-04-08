using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	
	public float velocity = 0.01f;
	public bool randomDirection = true;
	public Vector2 direction = new Vector2(1, 1);

	Material material;

	void Awake() {
		material = GetComponent<Renderer>().material;
	}

	void Start() {
		RandomizeTextureOffset();
		if(randomDirection) 
			RandomizeDirection();
	}

	void RandomizeTextureOffset() {
		Vector2 offset = material.mainTextureOffset;
		offset.x = Random.Range(0f, 1f);
		offset.y = Random.Range(0f, 1f);
		material.mainTextureOffset = offset;
	}

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
