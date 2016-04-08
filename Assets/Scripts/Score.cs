using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Player player;
	public Text scoreText;
	public Text gameOverScoreText;

	void Update () {
		scoreText.text = "" + player.score;

		if(player.isDead) {
			gameOverScoreText.text = "score: " + player.score + "\nbest : " + player.highscore;

			if(player.score >= player.highscore) {
				gameOverScoreText.text = "NEW RECORD: " + player.score;
				gameOverScoreText.color = Color.cyan;
			}

		}

	}

}
