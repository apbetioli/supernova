using UnityEngine;
using System.Collections;

namespace Supernova {

public class PlayerScore : MonoBehaviour {

	public int score = 0;
	public int highscore = 0;
	public int levelScore = 10;
	public AudioSource highscoreSound;

	void Start () {
		score = 0;
		highscore = PlayerPrefs.GetInt("highscore", highscore);
	}

	public void Die(string by) {
		if(score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("highscore", score);
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