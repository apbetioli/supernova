using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[HideInInspector]
	public bool isRunning = false;
	[HideInInspector]
	public bool isDead = false;

	public int score = 0;
	public int highscore = 0;
	public int levelScore = 10;

	int initialLevel = 1;
	int missedPlanets = 0;
	Animator playerAnimator;
	CustomEvents analytics;
	float idle = 0;
	int recover = 0;
	float targetPositionX; 
	bool authenticated;

	void Start () {
		score = 0;
		highscore = PlayerPrefs.GetInt("highscore", 0);
		analytics = GetComponent<CustomEvents>();
		playerAnimator = GetComponent<Animator>();
		targetPositionX = transform.position.x;

		Social.localUser.Authenticate(success => {
			authenticated = success;
			if (success) {
				Debug.Log ("Authentication successful");
				string userInfo = "Username: " + Social.localUser.userName + 
					"\nUser ID: " + Social.localUser.id + 
					"\nIsUnderage: " + Social.localUser.underage;
				Debug.Log (userInfo);
			}
			else {
				Debug.Log ("Authentication failed");
			}
		});

	}
	
	void Update () {
		if(isDead || !isRunning)
			return;

		if (idle >= 1.5) {
			Missed("Timeout");
			idle = 0;
		}

		idle += Time.deltaTime;

		Vector2 position = transform.position;
		position.x = Mathf.Lerp(position.x, targetPositionX, 25 * Time.deltaTime);
		transform.position = position;
	}

	void OnTouch() {
		if(isDead)
			return;

		if(!isRunning)
			isRunning = true;

		idle = 0;

		ChangeRoadSideOrNot();
	}

	void ChangeRoadSideOrNot() {

		var middle = Screen.width/2;

		var newSide = Input.mousePosition.x > middle ? 1 : -1;
		var playerSide = transform.position.x > 0 ? 1 : -1;

		var multiplier = newSide * playerSide;

		targetPositionX *= multiplier;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Enemy") {
			Die("Enemy");
			return;
		}

		if(col.gameObject.tag == "Collect") {
			AddScore();
			Destroy(col.gameObject);
			return;
		}

		if(col.gameObject.tag == "Filler") {
			Missed("Missed");
			return;
		}
	}

	void Missed(string by) {
		recover = 0;
		missedPlanets++;

		TriggerMissed(by);
	}

	void TriggerMissed(string by) {
		if(missedPlanets == 0)
			playerAnimator.SetTrigger("White");
		else if(missedPlanets == 1)
			playerAnimator.SetTrigger("Yellow");
		else if(missedPlanets == 2)
			playerAnimator.SetTrigger("Red");
		else	
			Die(by);
	}

	void Die(string by) {
		if(isDead) 
			return;

		isDead = true;
		playerAnimator.SetTrigger("Supernova");

		if(score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("highscore", score);
		}

		analytics.Death(score, by);

		UpdateDeathStatistics(by);
		UpdateTotalScore();

		if(authenticated) 
			Social.ReportScore(score, "CgkI-K6jy4kWEAIQBw", success => {});
	}

	void UpdateDeathStatistics(string by) {
		var deaths = PlayerPrefs.GetInt("DeathBy" + by, 0);
		PlayerPrefs.SetInt("DeathBy" + by, deaths+1);
	}

	void UpdateTotalScore() {
		var totalScore = PlayerPrefs.GetInt("totalscore", 0) + score;
		PlayerPrefs.SetInt("totalscore", totalScore);

		Debug.Log("Total score: " + totalScore);

		if(authenticated) 
			Social.ReportScore(totalScore, "CgkI-K6jy4kWEAIQCA", success => {});
		
	}

	bool IsIdle() {
		return !Input.GetMouseButtonDown(0);
	}

	void AddScore () {
		if(isDead) 
			return;

		score++;

		Heal();		
	}

	int Level() {
		return initialLevel + score / levelScore;
	}

	bool LevelUp() {
		var mod = (score % levelScore) == 0;
		return Level() > 1 && mod;
	}

	void Heal() {
		recover++;

		if(recover == 4+Level() && missedPlanets > 0) {
			recover = 0;
			missedPlanets--;
			TriggerMissed("Heal");
		}
	}

}
