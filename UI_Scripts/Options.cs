using UnityEngine;
using System.Collections;

public class Options : Photon.MonoBehaviour {

	public GameObject _OptionsMenu;
	public bool Cousr_or;
	public float soundCtrl;
	public bool OnOffOpt;
	public GameObject RusInfo;
	public GameObject GunInfo;
	public GameObject BYInfo;
	public string _CurPlayer;
	public Vector3 _respawnPos;
	public GameObject _currentPlayer_go;

	// Use this for initialization
	void Start () {
		RusInfo.SetActive (false);
		GunInfo.SetActive (false);
		BYInfo.SetActive (false);
		_OptionsMenu.SetActive (false);
		StartCoroutine (FixCurs ());

	}

	IEnumerator FixCurs() {

		yield return new WaitForSeconds (5);
		if (_CurPlayer == "Russki") {
			Screen.lockCursor = true;
			Cursor.visible = false;
		}	
	}



	public void OptionsMenuOn(){
		_OptionsMenu.SetActive (true);
		Cousr_or = true;
		Screen.lockCursor = false;
		Cursor.visible =true;
		
		if (_CurPlayer=="Russki") {
			RusInfo.SetActive(true);

		}

		else if (_CurPlayer == "Gunnar") {

			GunInfo.SetActive (true);

		} else if (_CurPlayer == "BY") {
			BYInfo.SetActive(true);

		}
	}

	public void OptionsMenuOff(){
		_OptionsMenu.SetActive (false);


		if (_CurPlayer=="Russki") {
			Cousr_or = false;
			Screen.lockCursor = true;
			Cursor.visible =false;			
			
		}
		
	}
	public void OnMainMenu(){
		Application.LoadLevel (0);
	}
	public void OnRestart()
	{
		photonView.RPC ("RPC_OnRestart", PhotonTargets.All);
	}

	[PunRPC]
	private void RPC_OnRestart ()
	{
		PhotonNetwork.LoadLevel (Application.loadedLevel);
	}

	public void OnRespawn(){
//		Debug.Log ("_respawnPos: " + _respawnPos);
		_currentPlayer_go.transform.position = _respawnPos;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnOffOpt=!OnOffOpt;

		}
		if (OnOffOpt == true) {
			OptionsMenuOn ();


		} else if (OnOffOpt == false) {
			OptionsMenuOff();

		}	
	}
}
