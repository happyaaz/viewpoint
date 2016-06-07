using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ChangingCameras : MonoBehaviour {

	public GameObject DirLightGun;
	public AudioClip camSwitchSnd;
	public Text CamNumber;
	public static GameObject activeGunnarsCamera_go;

	ControllingCameraButtonsMaster ccbm_scr;


	public void Start () {

		DirLightGun = GameObject.Find ("GunnarNightDirLight");
		CamNumber = GameObject.Find ("CamTxt").GetComponent <Text> ();

//		DirLightGun.SetActive (false);
	}

	public void GetAccessToListOfCamerasInRoom (ControllingCameraButtonsMaster _ccbm_scr) {
	
		ccbm_scr = _ccbm_scr;
	}


	public void ChangeCamera () {

		GetComponent<AudioSource>().PlayOneShot(camSwitchSnd, 0.2F);

		foreach (GameObject cam in ccbm_scr.neededCameras_list) 
		{
			cam.SetActive (false);
		}

		ccbm_scr.neededCameras_list [int.Parse (this.name)].SetActive (true);
		//  we always want to know the current camera. Once you leave the room, this camera should be disabled
		//  and the first camera in the next room should be enabled
		activeGunnarsCamera_go = ccbm_scr.neededCameras_list [int.Parse (this.name)];
		CamNumber.text = "CAM:" + (int.Parse(this.name) + 1);
	}


	public void NightVisionOn(){

		GetComponent<AudioSource>().PlayOneShot(camSwitchSnd, 0.2F);
		NightVision1 Nv = ccbm_scr.neededCameras_list [int.Parse (this.name)].GetComponent<NightVision1> ();
		Nv.enabled = !Nv.enabled;

		if(Nv.enabled)
		{
			DirLightGun.SetActive(true);
		}
		else if(!Nv.enabled) 
		{
			DirLightGun.SetActive(false);
		}
	}
}
