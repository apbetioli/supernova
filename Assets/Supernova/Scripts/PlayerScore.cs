using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * The score controller, i.e., how good you are.
	 */ 
	public class PlayerScore : MonoBehaviour {

		public int score = 0;
		public int highscore = 0;
		public AudioSource highscoreSound;

		void Start () {
			score = 0;
			highscore = Settings.GetHighscore();
		}

		public void HandleHighscore() {
			if(score > highscore) {
				highscore = score;
				Settings.SetHighscore(score);
				PlayHighscoreSound();
			}
		}

		public void AddScore () {
			score++;
		}

		void PlayHighscoreSound() {
			if(!Settings.IsSoundOn())
				return;

			highscoreSound.Play();
		}

	}

}