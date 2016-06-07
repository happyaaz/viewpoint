using UnityEngine;
using System.Collections;

public class OfficeHenchmen : MonoBehaviour {

	public bool noEnemyDetected_bool = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (noEnemyDetected_bool == false)
		{
			transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
		}
	}
}
