using UnityEngine;
using System.Collections;

public class KeypadDisplayRoom : MonoBehaviour {


	private bool on_bool = false;


	void OnTriggerEnter (Collider other) {

		if (on_bool == false)
		{
			if (other.tag == "ByongYang") 
			{
				on_bool = true;
				PuzzleMaster.pm_scr.puzzle_Room5_class.CheckNumberInCombination (int.Parse (this.name));

				Debug.Log ("number sent to PuzzleMaster");
			}
		}
	}


	void OnTriggerExit (Collider other) {

		if (on_bool == true)
		{
			if (other.tag == "ByongYang") 
			{
				on_bool = false;
			}
		}
	}
}
