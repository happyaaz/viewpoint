using UnityEngine;
using System.Collections;

public class LeaveTutorialArea_Ctrl : MonoBehaviour {

	public Transform leavingTutorial_SpawnFP;
	public Transform leavingTutorial_SpawnSP;
	public Transform leavingTutorial_SpawnTP;

	//New masks for the players when they leave the tutorial so they can't see it after they have left
	//No need to set up for the second person since we can set it up on the cameras in the scene
	public LayerMask FP_LMask;
	public LayerMask TP_LMask;

	private GameObject FP_Camera_GO;
	private GameObject TP_Camera_GO;

	private Rigidbody russky_RigidB;

	void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Russky")
		{
			StartCoroutine (DisableRigidbody());

			col.transform.parent.position = leavingTutorial_SpawnFP.position;
			col.transform.parent.rotation = leavingTutorial_SpawnFP.rotation;

			FP_Camera_GO = GameObject.FindGameObjectWithTag ("MainCamera");
			//			TP_Camera_GO = col.transform.Find ("Main Camera").gameObject;
			
			if(FP_Camera_GO != null)
			{
				FP_Camera_GO.GetComponent<Camera>().cullingMask = FP_LMask;
			}
			else
			{
				Debug.Log("Can't find the camera of Russky");
			}
		}
		else if(col.tag == "Gunnar")
		{
			col.transform.position  = leavingTutorial_SpawnSP.position;
			col.transform.rotation  = leavingTutorial_SpawnSP.rotation;

		}
		else if(col.tag == "ByongYang")
		{
			col.transform.position = leavingTutorial_SpawnTP.position;
			col.transform.rotation = leavingTutorial_SpawnTP.rotation;

			TP_Camera_GO = GameObject.FindGameObjectWithTag ("MainCamera");
//			TP_Camera_GO = col.transform.Find ("Main Camera").gameObject;

			if(TP_Camera_GO != null)
			{
				TP_Camera_GO.GetComponent<Camera>().cullingMask = TP_LMask;
			}
			else
			{
				Debug.Log("Can't find the camera of ByongYang");
			}
		}
	}

	IEnumerator DisableRigidbody ()
	{
		russky_RigidB.useGravity = false;
		russky_RigidB.detectCollisions = false;

		yield return new WaitForSeconds (3);

		russky_RigidB.useGravity = true;
		russky_RigidB.detectCollisions = true;
	}
}
