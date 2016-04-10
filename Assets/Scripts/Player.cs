using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

	Animator animator;
	PlayerHealth playerHealth; 
	PlayerScore playerScore;
	PlayerMovement playerMovement;
	Services services;

	public float timeout = 1.5f;
	float idle = 0;

	void Awake() {
		playerHealth = GetComponent<PlayerHealth>();
		playerScore = GetComponent<PlayerScore>();
		playerMovement = GetComponent<PlayerMovement>();
		animator = GetComponent<Animator>();
		services = GetComponent<Services>();
		if(services == null)
			Debug.LogWarning("Services not found");
	}

	void Start() {
		if(services != null)
			services.Authenticate();
	}

	void Update () {
		if (idle >= timeout) {
			Missed("Timeout");
			idle = 0;
		}

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

	public void Missed(string by) {
		if(IsDead())
			return;
		
		playerHealth.Missed(by);
		animator.SetInteger("MissedPlanets", playerHealth.missedPlanets); 

		if(playerHealth.missedPlanets > 2)
			Die(by);
	}

	public void Die(string by) {
		if(IsDead())
			return;

		playerHealth.Die(by);
		playerScore.Die(by);

		if(services != null) {
			services.Death(Score(), by);
			services.ReportScore(Score());
		}

		animator.SetTrigger("Supernova");
	}

	public void AddScore() {
		if(IsDead())
			return;

		idle = 0;
		playerHealth.Heal();
		playerScore.AddScore();
		animator.SetInteger("MissedPlanets", playerHealth.missedPlanets); 
	}

	void OnTouch() {
		if(IsDead())
			return;

		playerMovement.ChangeSideOrNot();
	}


}
