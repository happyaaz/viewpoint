using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Destroy (this.gameObject, 2);
	}


	void OnCollisionEnter (Collision col) {

		if (col.transform.tag == "Shield")
		{
			Debug.Log ("Did it");
			Destroy (this.gameObject);
		}
	}


	void OnTriggerEnter (Collider col) {
		
		if (col.transform.tag == "Shield")
		{
			Debug.Log ("Done it");
			Destroy (this.gameObject);
		}
	}
}
