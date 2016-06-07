using UnityEngine;
using System.Collections;

public class Restart_Ctrl : Photon.MonoBehaviour {

	//Use this to Restart the game

	public void RestartGame ()
	{
		photonView.RPC ("RPC_RestartGame", PhotonTargets.All);
	}

	[PunRPC]
	private void RPC_RestartGame ()
	{
		Application.LoadLevel (0);
	}
}
