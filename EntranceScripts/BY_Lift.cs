using UnityEngine;
using System.Collections;

public class BY_Lift : Photon.MonoBehaviour {

//	public Vector3 dest_vt3;
//	public bool timeToMove_bool = false;
//	private float speed_fl = 1;
	public GameObject passenger_go;
	public GameObject reference_go;
	public bool isUp_bool = false;

//	private bool moveLift_Bool = false;
//	private Vector3 temp_dest_vt3;

	public Animator lift_Platform_Animator;

	/*
	void Start () {
	
		if (this.name.Contains ("0-1"))
		{
			Debug.Log (this.transform.position.y);
			if (this.transform.position.y > 4)
			{
				isUp_bool = true;
			}
		}
		else if (this.name.Contains ("1-2"))
		{
			if (this.transform.position.y < 5)
			{
				isUp_bool = false;
			}
		}
	}
	*/


//	void Update () {
//	
//		if (timeToMove_bool == true)
//		{
//			GetComponent<PhotonView>().RPC("RPC_MoveLift_Bool", PhotonTargets.All);
//		}
//		if(moveLift_Bool == true)
//		{
//			float step = speed_fl * Time.deltaTime;
//			transform.position = Vector3.MoveTowards(this.transform.position, temp_dest_vt3, step);
//			if (Vector3.Distance (this.transform.position, temp_dest_vt3) == 0)
//			{
//				timeToMove_bool = false;
//				//PuzzleMaster.pm_scr.puzzle_Entrance_by_class.EnableButtons ();
//				if (passenger_go != null)
//				{
//					passenger_go.transform.root.parent = null;
//				}
//			}
//		}
//	}

	public void Lift_Platform_Up ()
	{

		Debug.Log ("Up");
		isUp_bool = true;
		StartCoroutine (Unparent ());
		ParentingGuy ();
		photonView.RPC ("RPC_Lift_Platform_Up", PhotonTargets.All);
	}


	IEnumerator Unparent () {

		yield return new WaitForSeconds (3);
		Debug.Log ("Done");

		UnparentingGuy ();
	} 


	public void ParentingGuy () {
	
		photonView.RPC ("RPC_Parenting", PhotonTargets.All);
	}


	public void UnparentingGuy () {
		
		photonView.RPC ("RPC_Unparenting", PhotonTargets.All);
	}


	[PunRPC]
	private void RPC_Parenting ()
	{
		try
		{
			passenger_go.transform.parent = reference_go.transform;
		}
		catch
		{
		}
		//		Debug.Log ("Lifting the platform");
	}


	[PunRPC]
	private void RPC_Unparenting ()
	{
		try
		{
			passenger_go.transform.parent = null;
		}
		catch
		{
		}
		//		Debug.Log ("Lifting the platform");
	}


	[PunRPC]
	private void RPC_Lift_Platform_Up ()
	{
		lift_Platform_Animator.SetInteger ("Lift_Platform_AnimState", 1);
//		Debug.Log ("Lifting the platform");
	}

	public void Lift_Platform_Down ()
	{
		try
		{
			passenger_go.transform.parent = reference_go.transform;
		}
		catch
		{
		}
//		passenger_go.transform.root.parent = reference_go.transform;
		isUp_bool = false;
		StartCoroutine (Unparent ());
		photonView.RPC ("RPC_Lift_Platform_Down", PhotonTargets.All);
	}
	
	[PunRPC]
	private void RPC_Lift_Platform_Down ()
	{
		lift_Platform_Animator.SetInteger ("Lift_Platform_AnimState", 2);
	}

//	[PunRPC]
//	void RPC_MoveLift_Bool ()
//	{
//		moveLift_Bool =! moveLift_Bool;
////		if(move
//		lift_Platform_Animator.SetInteger ("Lift_Platform_AnimState", 1);
////		temp_dest_vt3 = dest_vt3;
////		moveLift_Bool = true;
//	}
}
