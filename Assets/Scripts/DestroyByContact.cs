using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null)
		{
			Debug.Log("Cannot find game controller script.");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}

		if (other.tag == "EnemyBolt" && gameObject.tag == "Enemy")
		{
			return;
		}

		// if anything hit hazard, add score and show hazard explosion
		gameController.AddScore (scoreValue);

		Instantiate (explosion, transform.position, transform.rotation);

		// if hit player, leave destroy work to player controller
		if (other.tag == "Player")
		{
			return;
		}

		Debug.Log (other.tag + " collided with " + gameObject.tag + ".");
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
