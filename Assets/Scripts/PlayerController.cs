using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;

	public GameObject playerExplosion;
	public GameController gameController;

	void Start()
	{
		StartCoroutine (PlayerFire ());
    }

	IEnumerator PlayerFire()
	{
		while (true)
		{
			if (Input.GetButton("Fire1"))
			{
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				shot.tag = "Bolt";
				audio.Play();
				
				yield return new WaitForSeconds(fireRate);
            }
            else 
                yield return null;
        }
    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}

		// if anything hit player, destroy both objects, then game over
		Instantiate (playerExplosion, transform.position, transform.rotation);

		Debug.Log (other.tag + " collided with player.");
		Destroy (other.gameObject);
		Destroy (gameObject);
		gameController.GameOver ();
	}
}
