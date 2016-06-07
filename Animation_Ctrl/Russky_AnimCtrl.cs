using UnityEngine;
using System.Collections;

public class Russky_AnimCtrl : Photon.MonoBehaviour {

	public Animator RusskiAnimator;



	public void Russky_Idle_Anim ()
	{
		photonView.RPC ("RPC_Russky_Idle_Anim", PhotonTargets.AllBuffered);
	}
	
	[PunRPC]
	private void RPC_Russky_Idle_Anim ()
	{
		RusskiAnimator.SetInteger("ImWalking",0);		
	}

	public void Russky_Walk_Anim ()
	{
		photonView.RPC ("RPC_Russky_Walk_Anim", PhotonTargets.AllBuffered);
	}

	[PunRPC]
	private void RPC_Russky_Walk_Anim ()
	{
		RusskiAnimator.SetInteger("ImWalking",1);
	}
}
