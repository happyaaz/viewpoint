using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class CameraClickerManager : MonoBehaviour {

	public Camera minimapCamera_cam;
	public static CameraClickerManager ccm_scr;
	public LayerMask layerMask_lm;
	public List <GameObject> gunnarsCameras_list = new List <GameObject> ();
	public GameObject currentCamera_go;


	void Awake () {
	
		ccm_scr = this;
	}


	void Start () {
	
		ChangeCameraLayerVisibility (false, true, false);
		//ChangeCameraLayerVisibility (false, false, true);
		//ChangeCameraLayerVisibility (true, false, false);
	}


	void Update () {
	
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = minimapCamera_cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, layerMask_lm))
			{
				Debug.Log ("ASDASD " + hit.transform.name);
				Debug.Log ("DSADSA " + hit.transform.GetChild (0).gameObject.name);
				if (hit.transform.name.Contains ("CameraClicker"))
				{
					foreach (GameObject go in gunnarsCameras_list)
					{
						go.SetActive (false);
					}
					Debug.Log (hit.transform.GetChild (0).name);
					hit.transform.GetChild (0).gameObject.SetActive (true);
				}
			}
		}
	}


	public void ChangeCameraLayerVisibility (bool _basement_bool, bool _firstFloor_bool, bool _secondFloor_bool) {

		//  control CULLING mask
		if (_basement_bool == true)
		{
			minimapCamera_cam.cullingMask |= 1 << LayerMask.NameToLayer("Basement_Camera");
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Floor1_Camera"));
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Floor2_Camera"));
		}
		else if (_firstFloor_bool == true)
		{
			minimapCamera_cam.cullingMask |= 1 << LayerMask.NameToLayer("Floor1_Camera");
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Basement_Camera"));
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Floor2_Camera"));
		}
		else if (_secondFloor_bool == true)
		{
			minimapCamera_cam.cullingMask |= 1 << LayerMask.NameToLayer("Floor2_Camera");
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Basement_Camera"));
			minimapCamera_cam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Floor1_Camera"));
		}
	}
}
