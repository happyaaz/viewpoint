using UnityEngine;
using System.Collections;

public class RotateStuff : MonoBehaviour {
	
	// public variables:
	public float rotateX = 0.0f;
	public float rotateY = 1.0f;
	public float rotateZ = 0.0f;

	public float delta = 1.5f;  // Amount to move left and right from the start point
	public float speed = 2.0f; 
	private Vector3 startPos;

	// private variables:
	private Vector3 rotateXYZ;

	// Use this for initialization
	void Start () {
		// init Vector3:
		rotateXYZ = new Vector3 (rotateX, rotateY, rotateZ);

		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// set/update Vector3 according to public variables:
		rotateXYZ.x = rotateX;
		rotateXYZ.y = rotateY;
		rotateXYZ.z = rotateZ;

		// rotate:
		transform.Rotate(rotateXYZ);

		Vector3 v = startPos;
		v.y += delta * Mathf.Sin (Time.time * speed);
		transform.position = v;
	}
}
