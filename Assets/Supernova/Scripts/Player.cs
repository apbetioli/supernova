using UnityEngine;
using System.Collections;

namespace Supernova {

	/**
	 * This is the main player control. It delegates the control to other scripts, manages animations and sounds.
	 */ 
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(PlayerHealth))]
	[RequireComponent(typeof(PlayerScore))]
	[RequireComponent(typeof(PlayerMovement))]

	public class Player : MonoBehaviour {

		public AudioSource scoreSound;  //The sound that is played when the player scores
		public AudioSource deathSound;  //The sound that is played when the player dies
		public AudioSource damageSound; //The sound that is played then the player misses a planet

		Animator animator; 				
		PlayerHealth playerHealth; 		
		PlayerScore playerScore; 		
		PlayerMovement playerMovement; 	
		Leaderboard leaderboard; 		
		Analytics analytics;

		//Configures the timeout when the player is idle causing a damage event
		public float idleTimeout = 1.5f;

		//Counts the idle time
		float idle = 0;

		void Awake() {
			playerHealth = GetComponent<PlayerHealth>();
			playerScore = GetComponent<PlayerScore>();
			playerMovement = GetComponent<PlayerMovement>();
			animator = GetComponent<Animator>();

			//Optional

			leaderboard = GetComponent<Leaderboard>();
			if(leaderboard == null)
				Debug.LogWarning("Leaderboard not found");

			analytics = GetComponent<Analytics>();
			if(analytics == null)
				Debug.LogWarning("Analytics not found");
		}

		void Start() {
			if(leaderboard != null)
				leaderboard.Authenticate();
		}

		void Update () {
			VerifyTimeout();
			IncrementIdleTime();
		}

		// Verifies if the idle time has excedded the idle timeout time, causing a damage event
		void VerifyTimeout() {
			if (idle >= idleTimeout) {
				TakeDamage("Timeout");
				idle = 0;
			}
		}

		// If the play has started, the idle time is incremented
		void IncrementIdleTime() {
			if(playerMovement.isRunning)
				idle += Time.deltaTime;
		}

		public bool IsDead() {
			return playerHealth.isDead;	
		}

		public int Score() {
			return playerScore.score;
		}

		public int Highscore() {
			return playerScore.highscore;
		}

		public bool IsRunning() {
			return playerMovement.isRunning;
		}

		/*
		 * Damages the player, changing the state of the star, and may cause it to die. 
		 * It receives a cause that is used for analytics.
		 */ 
		public void TakeDamage(string cause) {
			if(IsDead())
				return;

			PlayDamageSound();
			playerHealth.TakeDamage();
			animator.SetInteger("Damage", playerHealth.damage); 

			if(playerHealth.damage > 2)
				Die(cause);
		}

		/*
		 * Triggers the death. 
		 * It receives a cause that is used for analytics.
		 */ 
		public void Die(string cause) {
			if(IsDead())
				return;

			PlayDeathSound();
			playerHealth.Die();
			playerScore.HandleHighscore();
			animator.SetTrigger("Supernova");

			if(leaderboard != null)
				leaderboard.ReportScore(Score());
			if(analytics != null)
				analytics.Death(Score(), cause);
		}

		// Adds one point of score, a new planet has been eaten.
		public void AddScore() {
			if(IsDead())
				return;

			idle = 0;
			playerHealth.Heal();
			playerScore.AddScore();
			animator.SetInteger("Damage", playerHealth.damage); 
			PlayScoreSound();
		}

		// This method receives a touch event from the gui
		void OnTouch() {
			if(IsDead())
				return;

			playerMovement.ChangeSideOrNot();
		}

		void PlayDeathSound() {
			if(!Settings.IsSoundOn())
				return;

			deathSound.Play();
		}

		void PlayScoreSound() {
			if(!Settings.IsSoundOn())
				return;
			
			scoreSound.Play();
		}

		public void PlayDamageSound() {
			if(!Settings.IsSoundOn())
				return;

			damageSound.Play();
		}

	}

}