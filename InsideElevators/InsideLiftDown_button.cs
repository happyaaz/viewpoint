using UnityEngine;
using System.Collections;

public class InsideLiftDown_button : MonoBehaviour {

	public void Down () {
		
		PuzzleMaster.pm_scr.insideElevators_class.LiftDown ();
	}
}
