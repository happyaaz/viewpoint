using UnityEngine;
using System.Collections;

public class PullerPusher : Photon.MonoBehaviour {

	public static PullerPusher pp_scr;
	public bool ifJumping_bool;
	public Transform russky_tr;

	public bool possibleToPushSmth_bool = false;
	public Transform pushableObjectToMove_tr;
	public bool russkyIsPulling_bool;


	void Awake () {

		pp_scr = this;
	}


	void FixedUpdate () {
	
		//  BTW: We can push only when not jumping

		if (russky_tr != null)
		{
			if (ifJumping_bool == false)
			{
				this.transform.position = russky_tr.position + russky_tr.forward * 1.5f + Vector3.up;
			}
			else
			{
				this.transform.position = new Vector3 (russky_tr.position.x, this.transform.position.y, russky_tr.position.z) + russky_tr.forward;
			}


			if (ifJumping_bool == false && Input.GetKey (KeyCode.Q) && possibleToPushSmth_bool == true)
			{
				GetComponent<PhotonView>().RPC("RPC_MovePushableObject", PhotonTargets.All);

				russkyIsPulling_bool = true;
			}
			else
			{
				russkyIsPulling_bool = false;
			}
		}
		else
		{
			Debug.LogError ("No russky?");
		}
	}


	void OnTriggerEnter (Collider other) {

		//if (other.tag == "Pushable")
		//{
			possibleToPushSmth_bool = true;
			pushableObjectToMove_tr = other.transform;

			Debug.Log ("ASDASDASDASD");

			if (other.transform.gameObject.GetComponent <Rigidbody> () != null)
			{
				other.transform.gameObject.GetComponent <Rigidbody> ().isKinematic = true;
				other.transform.gameObject.GetComponent <Rigidbody> ().useGravity = false;
			}
		//}
//		else
//		{
//			Debug.LogError ("No");
//		}

	}

	void OnTriggerExit(Collider other) {
		
		if (other.tag == "Pushable")
		{
			possibleToPushSmth_bool = false;
			pushableObjectToMove_tr = null;
		}
	}

	[PunRPC]
	void RPC_MovePushableObject ()
	{
		pushableObjectToMove_tr.position = new Vector3 (this.transform.position.x, pushableObjectToMove_tr.position.y, this.transform.position.z);
	}
}
