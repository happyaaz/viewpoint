using UnityEngine;
using System.Collections;

public class KeypadColorCode : Photon.MonoBehaviour {


	private bool wasAdded_bool = false;


	void Awake () {

//		Debug.Log (this.name);
	}


	void OnTriggerEnter (Collider other) {
		
		if (other.name.Contains (this.name) && wasAdded_bool == false)
		{
			photonView.RPC ("RPC_IncreaseNumber", PhotonTargets.AllBuffered);
		}
	}


	void OnTriggerExit (Collider other) {
		
		if (other.name.Contains (this.name) && wasAdded_bool == true)
		{
			photonView.RPC ("RPC_DecreaseNumber", PhotonTargets.AllBuffered);

		}
	}

	[PunRPC]
	private void RPC_IncreaseNumber ()
	{
		PuzzleMaster.pm_scr.puzzle_Room3_class.IncreaseNumberOfMatchedKeypads ();
		Debug.Log (PuzzleMaster.pm_scr.puzzle_Room3_class.GetNumberOfMatchedKeypads ());
		wasAdded_bool = true;

//		Debug.Log ("Added a number");
	}

	[PunRPC]
	private void RPC_DecreaseNumber ()
	{
		PuzzleMaster.pm_scr.puzzle_Room3_class.DecreaseNumberOfMatchedKeypads ();
		wasAdded_bool = false;
	}

}
