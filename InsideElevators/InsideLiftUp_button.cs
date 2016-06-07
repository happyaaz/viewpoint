using UnityEngine;
using System.Collections;

public class InsideLiftUp_button : MonoBehaviour {

	public void Up () {
		
		PuzzleMaster.pm_scr.insideElevators_class.LiftUp ();
	}
}
