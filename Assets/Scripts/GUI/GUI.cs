using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace Supernova {

public class GUI : MonoBehaviour {

	public Toggle sound;
	public AudioSource backgroundSoundTrack;
	public AudioSource uiSound;
	public Text scoreText;
	public GameObject pauseScreen;

	public Player player; 

	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
		sound.isOn = Settings.IsSoundOn();

		PlayBackgroundSoundTrack();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			HandleExit();
			return;
		}

		if(player.IsDead()) {
			animator.SetTrigger("GameOver");
			return;
		}

		if(Input.GetMouseButtonDown(0)) {
			if(IsPaused())
				Pause(false);
			else 
				SendOnTouchEvent();
		}

		if(player.Score() > player.Highscore()) {
			animator.SetTrigger("HighScore");
			scoreText.color = Color.cyan;
			return;
		}

		if(player.IsRunning() && !IsPaused()) {
			animator.SetTrigger("Start");
			return;
		}
	}

	public void Play() {
		SceneManager.LoadScene("Main");
	}

	void SendOnTouchEvent() {
		GameObject[] objects = FindObjectsOfType<GameObject>();
		for(var i = 0; i < objects.Length; i++) {
			objects[i].SendMessage ("OnTouch", SendMessageOptions.DontRequireReceiver);
		}
	}

	void HandleExit() {
		if(IsPaused() || player.IsDead())
			Application.Quit();
		else
			Pause(true);	
	}

	bool IsPaused() {
		return Time.timeScale == 0;
	}

	void Pause(bool pause) {
		if(player.IsDead())
			return;

		pauseScreen.SetActive(pause);

		if(pause) {
			Time.timeScale = 0;
			backgroundSoundTrack.pitch = 0;
		} else {
			Time.timeScale = 1;
			backgroundSoundTrack.pitch = 1;
		}
	}

	void OnApplicationPause(bool pause) {
		Pause(pause);
	}

	void PlayBackgroundSoundTrack() {
		if(!sound.isOn)
			return;

		backgroundSoundTrack.Play();
	}

	void PlayUiSound() {
		if(!sound.isOn)
			return;

		uiSound.Play();
	}

	void ToggleSound() {
		Settings.SetSound(sound.isOn);

		PlayUiSound();

		if(!sound.isOn) {
			backgroundSoundTrack.Stop();
		} else {
			if(! player.IsDead())
				backgroundSoundTrack.Play();
		}

	}

}

}