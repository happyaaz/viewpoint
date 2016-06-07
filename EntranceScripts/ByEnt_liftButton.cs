using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ByEnt_liftButton : Photon.MonoBehaviour {


	public Text byCanLift_txt;
	public GameObject respectiveLift_go;

	void Start () {
	
	}


	void OnTriggerEnter (Collider col) {

		if (col.tag == "ByongYang" && col.gameObject.GetComponent<PhotonView>().isMine == true)
		{ 
			Debug.Log ("AZazaza");
			col.GetComponent <CharController> ().ChangeLift (respectiveLift_go.name);
			byCanLift_txt.text = "Can use the lift";
//			PuzzleMaster.pm_scr.puzzle_Entrance_by_class.EnableButtons ();
		}
	}


	void OnTriggerExit (Collider col) {

		if (col.tag == "ByongYang" && col.gameObject.GetComponent<PhotonView>().isMine == true)
		{
			Debug.Log ("AZazaza");
			col.GetComponent <CharController> ().canMoveLift_bool = false;
			byCanLift_txt.text = string.Empty;
//			PuzzleMaster.pm_scr.puzzle_Entrance_by_class.DisableButtons ();
		}
	}
}
