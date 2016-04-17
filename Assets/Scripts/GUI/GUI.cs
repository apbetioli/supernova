using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif

namespace Supernova {

	/*
	 * User interface management.
	 */ 
	public class GUI : MonoBehaviour {

		// A button to on/off sounds
		public Toggle sound;

		// The background soundtrack
		public AudioSource backgroundSoundTrack;

		// The UI sound, that is played when a button is pressed
		public AudioSource uiSound;

		// The score text in the top of the screen
		public Text scoreText;

		// The pause screen that appears when the application is backgrounded
		public GameObject pauseScreen;

		// The player
		public Player player; 

		// The animator of the GUI
		Animator animator;

		void Awake() {
			animator = GetComponent<Animator>();
		}

		void Start() {
			sound.isOn = Settings.IsSoundOn();
			PlayBackgroundSoundTrack();
		}

		void Update () {
			// Paus or exit if the exit button was pressed
			if (Input.GetKeyDown(KeyCode.Escape)) {
				HandleExit();
				return;
			}

			// Triggers the GameOver animation if the player is dead
			if(player.IsDead()) {
				animator.SetTrigger("GameOver");
				return;
			}

			// Unpause or send the touch event when the screen is touched
			if(Input.GetMouseButtonDown(0)) {
				if(IsPaused())
					Pause(false);
				else 
					SendOnTouchEvent();
			}

			// The score text has the color changed when the highscore is reached
			if(player.Score() > player.Highscore()) {
				animator.SetTrigger("HighScore");
				scoreText.color = Color.cyan;
				return;
			}

			// It has started
			if(player.IsRunning() && !IsPaused()) {
				animator.SetTrigger("Start");
				return;
			}
		}

		// Loads the scene
		public void Play() {
			#if UNITY_5_3
				SceneManager.LoadScene("Main");
			#else
				Application.LoadLevel("Main");
			#endif
		}

		// Sends a global event, calling the method OnTouch of the classes
		void SendOnTouchEvent() {
			GameObject[] objects = FindObjectsOfType<GameObject>();
			for(var i = 0; i < objects.Length; i++) {
				objects[i].SendMessage ("OnTouch", SendMessageOptions.DontRequireReceiver);
			}
		}

		// Pause or exit
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
			// Can't pause if the player is dead
			if(player.IsDead())
				return;

			// Shows the pause screen
			pauseScreen.SetActive(pause);

			// Stops everything
			if(pause) {
				Time.timeScale = 0;
				backgroundSoundTrack.pitch = 0;
			} else {
				Time.timeScale = 1;
				backgroundSoundTrack.pitch = 1;
			}
		}

		// Mobile event
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

		// Turns the sound on / off
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