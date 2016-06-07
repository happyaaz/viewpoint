using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;


public class ControllingCameraButtonsMaster : MonoBehaviour {


	public List <GameObject> neededCameras_list = new List <GameObject> ();
	public GameObject cameraButton_pref;
	public GameObject canvasForButtons_cv;

	void Start () {

		foreach (GameObject go in neededCameras_list)
		{
			//  just in case, disable all the cameras
			go.SetActive (false);
		}

		if (this.name.Contains ("Outside"))
		{
			//  if we are outside, enable the very first camera
			neededCameras_list [0].SetActive (true);
			EnableNeededCameras ();
		}
	}


	void Update () {

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			EnableNeededCameras ();
		}
	}


	void OnTriggerEnter (Collider col) {
		
		if (col.tag == "Gunnar"  && col.GetComponent<PhotonView> ().isMine == true )
		{
			//  destroy all the camera buttons for the previous room
			List <GameObject> cameraButtons_list = GameObject.FindGameObjectsWithTag ("CameraButtons").ToList ();
			if (cameraButtons_list.Count > 0)
			{
				ChangingCameras.activeGunnarsCamera_go.SetActive (false);

				foreach (GameObject go in cameraButtons_list) 
				{
					Destroy (go);
				}
			}
			//  enable the very first camera in the next room
			neededCameras_list [0].SetActive (true);
			EnableNeededCameras ();
		}
	}
	
	
	public void EnableNeededCameras () {

		for (int i = 0; i < neededCameras_list.Count; i++) 
		{
			GameObject newCamButton_go = Instantiate (cameraButton_pref);
			newCamButton_go.GetComponent <ChangingCameras> ().GetAccessToListOfCamerasInRoom (this);
			newCamButton_go.name = i.ToString ();
			newCamButton_go.transform.GetChild (0).GetComponent <Text> ().text = (i + 1).ToString ();
			newCamButton_go.transform.parent = canvasForButtons_cv.transform;
			RectTransform rt = newCamButton_go.GetComponent <RectTransform> ();
			rt.localPosition = new Vector3 (-349 + 60 * i, -212, 0);
		}
	}
}