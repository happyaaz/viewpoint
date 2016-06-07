using UnityEngine;
using System.Collections;

public class CratePosRotNetworkUpdate : Photon.MonoBehaviour {

	// --Network variables START--
	//Syncing movement accross the network
	private float syncTime = 0f;
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	// --Network variables END--

	// Use this for initialization
	void Start () {
	
//		GameObject.Find ("BY")
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		SyncedMovement();	
	}

	//Next two functions are to sync the movement accross the network
	void SyncedMovement () 
	{		
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
		transform.rotation = Quaternion.Slerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
	}	
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
