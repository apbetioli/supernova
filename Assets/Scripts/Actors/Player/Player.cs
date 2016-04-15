﻿using UnityEngine;
using System.Collections;

namespace Supernova {

	/**
	 * This is the main player control. 
	 * It delegates the control to other scripts, mamnages animations and sounds.
	 */ 
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(PlayerHealth))]
	[RequireComponent(typeof(PlayerScore))]
	[RequireComponent(typeof(PlayerMovement))]

	public class Player : MonoBehaviour {

		public AudioSource deathSound; //The sound that is played when the player dies
		public AudioSource damageSound; //The sound that is played then the player misses a planet

		Animator animator; 				
		PlayerHealth playerHealth; 		
		PlayerScore playerScore; 		
		PlayerMovement playerMovement; 	
		Services services; 				

		//Configures the timeout when the player is idle causing a missed event
		public float idleTimeout = 1.5f;

		//Counts the idle time
		float idle = 0;

		void Awake() {
			playerHealth = GetComponent<PlayerHealth>();
			playerScore = GetComponent<PlayerScore>();
			playerMovement = GetComponent<PlayerMovement>();
			animator = GetComponent<Animator>();

			//The services script is optional
			services = GetComponent<Services>();
			if(services == null)
				Debug.LogWarning("Services not found");
		}

		void Start() {
			if(services != null)
				services.Authenticate();
		}

		void Update () {
			VerifyTimeout();
			IncrementIdleTime();
		}

		// Verifies if the idle time has excedded the idle timeout time, causing a missed event
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

			if(services != null) {
				services.Death(Score(), cause);
				services.ReportScore(Score());
			}
		}

		// Adds one point of score, a new planet has been eaten.
		public void AddScore() {
			if(IsDead())
				return;

			idle = 0;
			playerHealth.Heal();
			playerScore.AddScore();
			animator.SetInteger("Damage", playerHealth.damage); 
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

		public void PlayDamageSound() {
			if(!Settings.IsSoundOn())
				return;

			damageSound.Play();
		}

	}

}