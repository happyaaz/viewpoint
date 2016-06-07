using UnityEngine;
using System.Collections;

public class ShotAtTarget : Photon.MonoBehaviour {


	public GameObject text_go;
	public int index_int;
	public static int numberOfNumbers_int = 0;
//	PhotonViewIsMineHandler photonViewIsMineHandler;

	void Start () {
	
//		AddNewNumber () ;
		text_go.SetActive (false);
		StartCoroutine (SmallDelayToRandomize ());
	}


	void OnTriggerEnter (Collider other) {

//		if (text_go.activeSelf == false) 
//		{
//			text_go.SetActive (true);
//		}

		if (other.tag == "Throwable") 
		{
//			GameObject.Find ("PhotonViewIsMineHandler").GetComponent<PhotonViewIsMineHandler>().RandomizeANumber ();
//			AddNewNumber ();

//			StartCoroutine (SmallDelayToRandomize ());
			photonView.RPC ("RPC_ActivateText_GO", PhotonTargets.All);
//			text_go.SetActive (true);
			AddNewNumber ();
			Debug.Log ("Throwable entered the trigger");
		}
	}

	[PunRPC]
	public void RPC_ActivateText_GO ()
	{
		text_go.SetActive (true);
	}

	void AddNewNumber () 
	{
//		int rn = GameObject.Find ("PhotonViewIsMineHandler").GetComponent<PhotonViewIsMineHandler>().rn_Int;
//		
////		int rn = Random.Range (0, 10);
//		Debug.Log ("randomizing a code number");
//		Debug.Log ("rn: " + rn);
//		if (!PuzzleMaster.pm_scr.puzzle_Room5_class.combination_list.Contains (rn))
//		{
//			//photonView.RPC ("RPC_AddNewNumber", PhotonTargets.All, rn);
//			StartCoroutine (RunThatShit (rn));
//			Debug.Log ("Isn't in the combination list");
//		}
//		else
//		{
//			AddNewNumber ();
//		}
		
		text_go.GetComponent <TextMesh>().text = "" + PuzzleMaster.pm_scr.puzzle_Room5_class.combination_list [index_int];
		Debug.Log ("Adding numbers");
//		PuzzleMaster.pm_scr.puzzle_Room5_class.AddNumber ();
		numberOfNumbers_int ++;
		if (numberOfNumbers_int == 3)
		{
			PuzzleMaster.pm_scr.puzzle_Room5_class.PasswordIsReady ();
			Debug.Log ("Ready");
		}
	}


	IEnumerator RunThatShit (int _rn) {
		Debug.Log ("AKAK");
		yield return new WaitForSeconds (1);
		Debug.Log ("OOOOO");
		photonView.RPC ("RPC_AddNewNumber", PhotonTargets.All, _rn);
	}

	[PunRPC]
	public void RPC_AddNewNumber (int rn)
	{
		Debug.Log ("RPC_AddNewNumber run");
		text_go.GetComponent<TextMesh>().text = "" + rn;
		PuzzleMaster.pm_scr.puzzle_Room5_class.AddNumber (index_int, rn);
		numberOfNumbers_int ++;
		if (numberOfNumbers_int == 3)
		{
			PuzzleMaster.pm_scr.puzzle_Room5_class.PasswordIsReady ();
		}
	}

	IEnumerator SmallDelayToRandomize ()
	{
		yield return new WaitForSeconds(1);
		AddNewNumber ();
//		Debug.Log("SmallDelayToRandomize run");
//		GameObject.Find ("PhotonViewIsMineHandler").GetComponent<PhotonViewIsMineHandler>().RandomizeANumber ();
//		yield return new WaitForSeconds(2f);
//		AddNewNumber ();
	}
}
