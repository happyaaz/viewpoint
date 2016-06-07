using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenGunnarPuzzle : Photon.MonoBehaviour {

	public List <GameObject> objToDestroy_list = new List <GameObject> ();
	//public GameObject door_go;
	private GettingKeysDown gkd_scr;

	public GameObject hacking_Canvas_GO;

	private bool hacked_Bool = false;

	public GameObject Keypad_Locked_Particle;
	public GameObject Keypad_Open_Particle;
    AudioClip Opening;

	void Start () {
		
		gkd_scr = GameObject.Find ("_Hacking").GetComponent <GettingKeysDown> ();

//		try
//		{
//			objToDestroy_list [0].GetComponent<Animation>().Play ();
//		}
//		catch {
//		}

	}


	void OnTriggerEnter (Collider col) {

		if (col.tag == "Gunnar" && col.GetComponent<PhotonView>().isMine == true && hacked_Bool == false)
		{
			
			if (!this.name.Contains ("workaround"))
			{
         
                Debug.Log ("The network player of Gunnar hit keypad");
				//DestroyFunction ();
				hacking_Canvas_GO.SetActive (true);
				gkd_scr.ItsTimeToStartHacking (this);
			}
			else
			{
				if (PuzzleMaster.pm_scr.puzzle_Room6_class.numberOfHitButtons_int == 2)
				{
					Debug.Log ("The network player of Gunnar hit keypad");
					//DestroyFunction ();
					hacking_Canvas_GO.SetActive (true);
					gkd_scr.ItsTimeToStartHacking (this);
				}
			}

     
		}
	}


	public void DestroyFunction () 
	{
		hacked_Bool = true;
		GetComponent<PhotonView>().RPC("RPC_DestroyFunction", PhotonTargets.All);
	}


	[PunRPC]
	void RPC_DestroyFunction ()
	{
		foreach (GameObject go in objToDestroy_list)
		{
			try
			{
				Keypad_Locked_Particle.SetActive(false);

				go.GetComponent<Animation>().Play ();
                go.GetComponent<DoorSoundScript>().Playsound();
				StartCoroutine (TurnOffOpenParticle ());
			}
			catch (System.Exception e) 
			{
				Destroy (go);
			}
			//
		}
	}

	private IEnumerator TurnOffOpenParticle ()
	{
		Keypad_Open_Particle.SetActive(true);
		yield return new WaitForSeconds (3f);
		Keypad_Open_Particle.SetActive(false);
	}
}