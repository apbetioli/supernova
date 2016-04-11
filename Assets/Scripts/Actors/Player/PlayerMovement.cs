using UnityEngine;
using System.Collections;

namespace Supernova {

public class PlayerMovement : MonoBehaviour {

	public float speed = 25f;

	[HideInInspector]
	public bool isRunning = false;	

	float targetPositionX; 

	void Start() {
		targetPositionX = transform.position.x;
	}

	void Update () {
		Vector2 position = transform.position;
		position.x = Mathf.Lerp(position.x, targetPositionX, speed * Time.deltaTime);
		transform.position = position;
	}

	public void ChangeSideOrNot() {
		if(!isRunning)
			isRunning = true;

		var middle = Screen.width/2;

		var newSide = Input.mousePosition.x > middle ? 1 : -1;
		var playerSide = transform.position.x > 0 ? 1 : -1;

		var multiplier = newSide * playerSide;

		targetPositionX *= multiplier;
	}

}

}