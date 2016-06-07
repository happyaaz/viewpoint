using UnityEngine;
using System.Collections;

public class ButtonsShootingRoom_1 : Photon.MonoBehaviour {

	public GameObject henchman_go;

	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Bullet") 
		{
			photonView.RPC ("RPC_ButtonHit", PhotonTargets.All);
		}
	}

	[PunRPC]
	private void RPC_ButtonHit ()
	{
		PuzzleMaster.pm_scr.puzzle_Room6_class.ButtonHit (this.gameObject);
		if(henchman_go != null)
		{
			Destroy (henchman_go);
		}
	}
}
