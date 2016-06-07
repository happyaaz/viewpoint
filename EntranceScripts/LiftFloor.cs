using UnityEngine;
using System.Collections;

public class LiftFloor : MonoBehaviour {


	public BY_Lift byLift_scr;

	void OnTriggerEnter (Collider col) {


		if (col.gameObject.GetComponent<PhotonView>().isMine == true)
		{
			if (col.tag == "Russky" || col.tag == "ByongYang")
			{
				//activate buttons
				byLift_scr.passenger_go = col.gameObject;
				byLift_scr.reference_go = this.transform.parent.gameObject;
				//col.transform.root.parent = this.transform.parent;
	//				byLift_scr.lift
			}
			else if (col.tag == "Gunnar")
			{
				byLift_scr.passenger_go = col.gameObject.transform.parent.gameObject;
				byLift_scr.reference_go = this.transform.parent.gameObject;
			}
			else
			{
				Debug.Log (col.tag);
			}
		}
	}


	void OnTriggerExit (Collider col) {

		if (col.gameObject.GetComponent<PhotonView>().isMine == true)
		{
			byLift_scr.passenger_go = null;
			byLift_scr.UnparentingGuy ();
		}
		/*
		if (col.tag == "Russky" || col.tag == "Gunnar")
		{
			Debug.Log ("UUUUUUUU");
			byLift_scr.passenger_go = col.gameObject;
//			col.transform.parent.transform.parent = null;
		}
		*/
	}
}
