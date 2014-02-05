using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		Debug.Log (other.tag + " game object is destroyed by boundary.");
		Destroy (other.gameObject);
	}
}
