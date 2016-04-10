using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Player player;
	public Text scoreText;
	public Text gameOverScoreText;

	void Update () {
		scoreText.text = "" + player.Score();

		if(player.IsDead()) {
			gameOverScoreText.text = "score: " + player.Score() + "\nbest : " + player.Highscore();

			if(player.Score() >= player.Highscore()) {
				gameOverScoreText.text = "NEW RECORD: " + player.Score();
				gameOverScoreText.color = Color.cyan;
			}

		}

	}

}
