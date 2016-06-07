using UnityEngine;
using System.Collections;

public class BYPushObjects : Photon.MonoBehaviour {

	private bool RPC_canPushObjects_bool = false;

	private bool canPushObjects_bool = false;
	private Rigidbody toPush_rb;
	private GameObject crateToPush_go;


	void Update () {

		//Check first if we are the controller of the character or not, then allow us to control it
		if (photonView.isMine == true) 
		{
			if (Input.GetMouseButtonDown (0))
			{
				if (canPushObjects_bool == true)
				{
					GetComponent<PhotonView>().RPC("AddForceToRB", PhotonTargets.All);
				}
			}
		}
		if(RPC_canPushObjects_bool == true)
		{
			if (crateToPush_go.GetComponent <Rigidbody> () == null)
			{
				crateToPush_go.AddComponent <Rigidbody> ().AddForce (this.transform.forward * 10, ForceMode.Impulse);
			}
			else
			{
				crateToPush_go.GetComponent <Rigidbody> ().AddForce (this.transform.forward * 10, ForceMode.Impulse);
			}
		}
	}

	[PunRPC]
	void AddForceToRB () 
	{
		RPC_canPushObjects_bool = true;
	}


	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Pushable")
		{
			canPushObjects_bool = true;
			crateToPush_go = other.transform.gameObject;
		}
	}


	void OnTriggerExit(Collider other) {
		
		if (other.tag == "Pushable")
		{
			canPushObjects_bool = false;
			RPC_canPushObjects_bool = false;
			crateToPush_go = null;
		}
	}
}
