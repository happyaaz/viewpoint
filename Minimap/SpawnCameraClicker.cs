using UnityEngine;
using System.Collections;

public class SpawnCameraClicker : MonoBehaviour {

	public GameObject cameraClicker_pref;
	public string layerName_str;

	void Start () {
	
		GameObject newCam_go = Instantiate (cameraClicker_pref, 
		             new Vector3 (this.transform.position.x, cameraClicker_pref.transform.position.y, this.transform.position.z),
		             Quaternion.identity) as GameObject;
		this.transform.GetChild (0).GetChild (0).transform.parent = newCam_go.transform;
		newCam_go.transform.parent = this.transform.GetChild (0);

		newCam_go.layer = LayerMask.NameToLayer (layerName_str);
	}
}
