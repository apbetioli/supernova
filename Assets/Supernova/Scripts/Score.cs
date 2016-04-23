using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Supernova {
	
	/*
	 * Controls the score and gameOverScore GUI texts.
	 */ 
	public class Score : MonoBehaviour {

		public Player player;
		public Text scoreText;
		public Text gameOverScoreText;

		void Update () {
			// Always update score text
			scoreText.text = "" + player.Score();

			if(player.IsDead()) {
				// Builds the game over text, with score and highscore
				gameOverScoreText.text = "score: " + player.Score() + "\nbest : " + player.Highscore();

				// When a new highscore is achieved, shows "NEW RECORD" and changes the color of the text
				if(player.Score() >= player.Highscore()) {
					gameOverScoreText.text = "NEW RECORD: " + player.Score();
					gameOverScoreText.color = Color.cyan;
				}

			}

		}

	}

}