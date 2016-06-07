using UnityEngine;
using System.Collections;

public class MinimapTrigger : MonoBehaviour {


	void OnTriggerEnter (Collider col) {
		
		if (col.tag == "Gunnar" && col.GetComponent<PhotonView> ().isMine == true)
		{
			
			if (this.name.Contains ("Basement"))
			{
				CameraClickerManager.ccm_scr.ChangeCameraLayerVisibility (true, false, false);
//				MinimapFunctionality.mf_scr.BasementMap ();
			}
			else if (this.name.Contains ("FirstFloor"))
			{
				CameraClickerManager.ccm_scr.ChangeCameraLayerVisibility (false, true, false);
//				MinimapFunctionality.mf_scr.FirstFloorMap ();
			}
			else if (this.name.Contains ("SecondFloor"))
			{
				CameraClickerManager.ccm_scr.ChangeCameraLayerVisibility (false, false, true);
//				MinimapFunctionality.mf_scr.SecondFloorFloorMap ();
			}
		}
	}
}
