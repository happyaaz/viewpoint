using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RusskyChooseFeature : Photon.MonoBehaviour {
	

	public GravityGun gg_scr;
	public ShieldFunctionality sf_scr;
	public Text usedItem_txt;

	void Start () {

		usedItem_txt = GameObject.Find ("UsedItem").GetComponent <Text> ();
	}


	void Update () {
	
		if (photonView.isMine == true)
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
			{
				usedItem_txt.text = "Gravity gun is chosen";
				sf_scr.DeactivateShield ();
				sf_scr.shieldIsActivated_bool = false;
				gg_scr.ggFeatureIsActivated_bool = true;
				gg_scr.DropHeldObject ();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
			{
				usedItem_txt.text = "Shield is chosen";
				gg_scr.DropHeldObject ();
				gg_scr.ggFeatureIsActivated_bool = false;
				sf_scr.shieldIsActivated_bool = true;
			}
		}
	}
	
}
