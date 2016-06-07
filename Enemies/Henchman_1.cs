using UnityEngine;
using System.Collections;

public class Henchman_1 : MonoBehaviour {

	public Transform bulletSpawner_tr;
	public GameObject shootingArea_go;

	public GameObject targetToShootAt_go;
	public GameObject bullet_go;

	//Bullet shooting rate
	private float shootRate_fl = 0.9f;
	private float elapsedTime_fl;


	void Update () {
	
		if (targetToShootAt_go != null) 
		{
			Vector3 targetPosition = targetToShootAt_go.transform.position;
			targetPosition.y = transform.position.y;
			transform.LookAt(targetPosition);

			elapsedTime_fl += Time.deltaTime;
			if (elapsedTime_fl >= shootRate_fl)
			{
				//Reset the time
				elapsedTime_fl = 0.0f;
				//Instantiate the bullet
				GameObject _bullet_go = Instantiate(bullet_go, bulletSpawner_tr.position, bulletSpawner_tr.rotation) as GameObject;
				_bullet_go.GetComponent <Rigidbody> ().AddForce (this.transform.forward * 2, ForceMode.Impulse);
			}
		}
	}
}
