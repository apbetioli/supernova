using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public float step = 1f;
	public float rotationVelocity = Random.Range(0f, 0.5f);

	Player player;
	float positiony;

	void Awake() {
		player = GameObject.Find("Player").GetComponent<Player>();
		if(player == null) 
			Debug.LogError("Could not find the Player");
	}
		
	void Start () {
		positiony = transform.position.y;
	}

	void Update () {
		transform.Rotate(0, 0, -rotationVelocity * Time.deltaTime * transform.position.x);

		Vector2 pos = transform.position;
		pos.y = Mathf.Lerp(pos.y, positiony, 5 * step * Time.deltaTime);
		transform.position = pos;
	}

	void OnTouch() {
		if(!player.isDead)
			positiony = positiony - step;
	}

}
