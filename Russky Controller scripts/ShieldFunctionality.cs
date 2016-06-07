using UnityEngine;
using System.Collections;

public class ShieldFunctionality : Photon.MonoBehaviour {

    public AudioSource MySource;
    public AudioClip Clank;
    public AudioClip ClonkibonkiIdontWantThisShieldAnymore;
	public GameObject shield_go;
	public Transform camera_tr;
	public bool shieldIsActivated_bool = false;

	void Start () {

        MySource = this.gameObject.GetComponent<AudioSource>();
		DeactivateShield ();
	}
	

	void Update () {
	

		//Check first if we are the controller of the character or not, then allow us to control it
		if (photonView.isMine == true) 
		{
			if (shieldIsActivated_bool == true)
			{
				if (Input.GetMouseButtonDown (0))
				{
					if (shield_go.activeSelf == false)
					{
						ActivateShield ();
                        MySource.PlayOneShot(Clank);
					}
					else
					{
						DeactivateShield ();
                        MySource.PlayOneShot(ClonkibonkiIdontWantThisShieldAnymore);
					}
				}
			}
		}
	}


	public void ActivateShield () {
		GetComponent<PhotonView>().RPC("ActivateShield_RPC", PhotonTargets.All);
	}


	public void DeactivateShield () {
		GetComponent<PhotonView>().RPC("DeactivateShield_RPC", PhotonTargets.All);
	}


	[PunRPC]
	void ActivateShield_RPC () {
		
		shield_go.SetActive (true);
	}
	
	[PunRPC]
	void DeactivateShield_RPC () {
		
		shield_go.SetActive (false);
	}
}
