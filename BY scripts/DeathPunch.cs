using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DeathPunch : Photon.MonoBehaviour {

	public bool deathPunch_bool;
	public GameObject enemyForDeathPunch_go;

	public Text byCanDeathPunch_txt;

	public GameObject Stun_Particles1;
	public GameObject Stun_Particles2;

	void Update () {
	
		//Check first if we are the controller of the character or not, then allow us to control it
		if (photonView.isMine == true) 
		{
			if (Input.GetKeyDown (KeyCode.F) && deathPunch_bool == true)
			{
				photonView.RPC("StunPunch", PhotonTargets.All);
			}
		}
	}


	void OnTriggerStay (Collider col) {

		if (col.tag == "Enemy") {

			deathPunch_bool = true;
			enemyForDeathPunch_go = col.gameObject;
			byCanDeathPunch_txt.text = "Can kill the enemy";
		}
	}


	void OnTriggerExit (Collider col) {
		
		if (col.tag == "Enemy") {
			
			deathPunch_bool = false;
			enemyForDeathPunch_go = null;
			byCanDeathPunch_txt.text = string.Empty;
		}
	}

	[PunRPC]
	public void StunPunch ()
	{
		StartCoroutine (StunnBlast ());
		Destroy (enemyForDeathPunch_go);
		//  death punch
		deathPunch_bool = false;
	}

	private IEnumerator StunnBlast ()
	{
		Stun_Particles1.SetActive(true);
		Stun_Particles2.SetActive(true);
		yield return new WaitForSeconds (3f);
		Stun_Particles1.SetActive(false);
		Stun_Particles2.SetActive(false);

	}
	
}
