using UnityEngine;
using System.Collections;

public class ShootingArea_1 : MonoBehaviour {

	public Henchman_1 hcmn_scr;


	void OnTriggerEnter (Collider col) {

		if (hcmn_scr.targetToShootAt_go == null)
		{
			if (col.tag == "Russky" || col.tag == "ByongYang" || col.tag == "Gunnar")
			{
				Debug.Log ("Ururu");
				hcmn_scr.targetToShootAt_go = col.gameObject;
			}
		}
	}


	void OnTriggerStay (Collider col) {

		if (hcmn_scr.targetToShootAt_go == null)
		{
			if (col.tag == "Russky" || col.tag == "ByongYang" || col.tag == "Gunnar")
			{
				Debug.Log ("Yolo");
				hcmn_scr.targetToShootAt_go = col.gameObject;
			}
		}
	}


	void OnTriggerExit (Collider col) {
		
		if (col.tag == "Russky" || col.tag == "ByongYang" || col.tag == "Gunnar")
		{
			if (hcmn_scr.targetToShootAt_go == col.gameObject)
			{
				Debug.Log ("LALALA");
				hcmn_scr.targetToShootAt_go = null;
			}
		}
	}
}
