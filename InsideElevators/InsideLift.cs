using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsideLift : MonoBehaviour {


	public GameObject liftToPass_go;
	public Text byCanLift_txt;
	public GameObject respectiveLift_go;

	void Start () {
	

	}

	void OnTriggerEnter (Collider col) {
		
		if (col.gameObject.GetComponent<PhotonView>().isMine == true)
		{
			Debug.Log ("Time to go");
			PuzzleMaster.pm_scr.insideElevators_class.lift_go = liftToPass_go;
			PuzzleMaster.pm_scr.insideElevators_class.byLift_scr = liftToPass_go.GetComponent <BY_Lift> ();
//			PuzzleMaster.pm_scr.insideElevators_class.EnableButtons ();

			col.GetComponent <CharController> ().ChangeLift (respectiveLift_go.name);
			byCanLift_txt.text = "Can use the lift";
		}
	}
	
	
	void OnTriggerExit (Collider col) {
		
		if (col.tag == "ByongYang" && col.gameObject.GetComponent<PhotonView>().isMine == true)
		{
			Debug.Log ("Nah");
//			PuzzleMaster.pm_scr.insideElevators_class.DisableButtons ();
			col.GetComponent <CharController> ().canMoveLift_bool = false;
			byCanLift_txt.text = string.Empty;
		}
	}
}
