using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	public GameObject enemy;
	public Vector3 spawnValues;
	public float hazardCount;
	public float startWait;
	public float waveInterval;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameoverText;
	private int score;
	private bool gameOver;
	private bool restart;

	// Use this for initialization
	void Start () {

		score = 0;
		restart = false;
		gameOver = false;
		restartText.text = "";
		gameoverText.text = "";
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	
	}

	void Update()
	{
		if (restart && Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				// randomly pick asteroid or enemy
				int selection = Random.Range(0, 3);
				if (selection <= 1)
				{
					Instantiate (asteroid, spawnPosition, spawnRotation);
				}
				else
				{
					Instantiate (enemy, spawnPosition, enemy.transform.rotation);
				}

				yield return new WaitForSeconds(0.5f);
			}

			yield return new WaitForSeconds(waveInterval);

			if (gameOver)
			{
				restartText.text = "Press 'R' to restart.";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOver = true;
		gameoverText.text = "Game Over!";
	}
}
