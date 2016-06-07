using UnityEngine;
using System.Collections;

public class DetectionField_1 : MonoBehaviour {

	public OfficeHenchmen oh_scr;


	void OnTriggerEnter (Collider col) {
		
		if (oh_scr.noEnemyDetected_bool == false)
		{
			if (col.tag == "Russky" || col.tag == "ByongYang" || col.tag == "Gunnar")
			{
				Debug.Log ("Ururu");
				oh_scr.noEnemyDetected_bool = true;
			}
		}
	}
}
