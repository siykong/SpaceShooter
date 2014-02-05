using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;

	IEnumerator EnemyFire()
	{
		while (true)
		{
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			shot.tag = "EnemyBolt";
			audio.Play();
			
			yield return new WaitForSeconds (fireRate);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
		{
			StartCoroutine (EnemyFire ());
		}
	}
}
