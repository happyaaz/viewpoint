using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameMaster : MonoBehaviour {


	//  singleton
	public static GameMaster gm_scr;

	//  that's how we disable the movement of the characters
	public bool byongYangActive_bool;
	public bool russkyActive_bool;
	public bool gunnarActive_bool;


	//  still useful to store the references
	public GameObject byongYang_go;
	public GameObject russky_go;
	//public GameObject gunnar_go;

	public List <GameObject> gunnarsCameras_list = new List <GameObject> ();
	public List <GameObject> cameraButtons_list = new List <GameObject> ();

	//  for switching between players
//	private int indexOfPlayers_int = 0;


	void Awake () {

//		foreach (GameObject go in gunnarsCameras_list)
//		{
//			go.SetActive (false);
//		}
//		gm_scr.enabled = false;

		gm_scr = this;
	}


	void Start () {

		byongYang_go = GameObject.Find ("BY_Holder");
		russky_go = GameObject.Find ("RU_Holder");
		//gunnar_go = GameObject.Find ("GU_Holder");

		//List <GameObject> gunnarsCamerasNotSorted_list = GameObject.FindGameObjectsWithTag ("GunnarsCamera").ToList ();
		//gunnarsCameras_list = GameObject.FindGameObjectsWithTag ("GunnarsCamera").ToList ();
		//  default
		SwitchCameraState (byongYang_go, true);
		SwitchCameraState (russky_go, false);
		//gunnar_go.SetActive (false);
		//  turn off gunnars cameras
	}



	void ByongYangActive () {

		byongYangActive_bool = true;
		russkyActive_bool = false;
		gunnarActive_bool = false;

		//  hide GUI for the cameras
		//  enable the camera +
		SwitchCameraState (byongYang_go, true);
		SwitchCameraState (russky_go, false);
		//gunnar_go.SetActive (false);
	}


	void RusskyActive () {

		byongYangActive_bool = false;
		russkyActive_bool = true;
		gunnarActive_bool = false;

		//  turn off BY's camera +
		SwitchCameraState (byongYang_go, false);
		//  enable the camera +
		SwitchCameraState (russky_go, true);
		//gunnar_go.SetActive (false);
	}


	void GunnarActive () {

		byongYangActive_bool = false;
		russkyActive_bool = false;
		gunnarActive_bool = true;

		//  turn off R's camera
		SwitchCameraState (russky_go, false);
	
	}


	void SwitchCameraState (GameObject _char, bool value) {

		GameObject cam = _char.transform.FindChild ("Main Camera").gameObject;
		cam.SetActive (value);
//		Debug.Log (byongYangActive_bool);
	}
}
