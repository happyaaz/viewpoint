using UnityEngine;
using System.Collections;

public class RusEntr_Lock : Photon.MonoBehaviour {

	public GameObject plank_L_GO;
	public GameObject plank_R_GO;
	public GameObject liftThingie_go;


	void OnCollisionEnter (Collision col) {

		if (col.transform.tag == "Throwable")
		{
			Debug.Log ("DADADADDDADADAD");
			PuzzleMaster.pm_scr.puzzle_Entrance_ru_class.OpenDoor ();
			photonView.RPC ("RPC_OpenDoor", PhotonTargets.All);
		}
	}

	[PunRPC]
	private void RPC_OpenDoor ()
	{
		Destroy (plank_L_GO);
		Destroy (plank_R_GO);
		Destroy (liftThingie_go);
		Destroy (this.gameObject);
	}
}
