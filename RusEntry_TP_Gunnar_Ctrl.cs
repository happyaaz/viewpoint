using UnityEngine;
using System.Collections;

public class RusEntry_TP_Gunnar_Ctrl : MonoBehaviour {

	public Transform newGunnarPos;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Gunnar")
		{
			other.transform.position = newGunnarPos.position;
		}
	}
}
