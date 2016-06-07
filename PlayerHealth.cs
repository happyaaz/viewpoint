using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {
	public float max_Health=100f;
	public float cur_Health=0f;
//	public GameObject HealthBar;
//	public GameObject GameOver;

	UI_Ctrl uI_Ctrl;
	public AudioSource MySource;
	public AudioClip HurtSound;
	public AudioClip DeathSound;

	// Use this for initialization
	void Awake () {
		cur_Health = max_Health;
//		GameOver = GameObject.Find ("Game Over Window");

//		GameOver.SetActive (false);
		//InvokeRepeating ("DecreaseHealth", 1f, 1f);
	}
//	void OnTriggerEnter(Collider col) {
//		
//		if (col.transform.tag == "Bullet") {
//			DecreaseHealth();
//			Debug.Log ("DecreaseHealth");
//			
//		}
//		else if (col.transform.tag == "Laser") {
//			DecreaseHealth();
//			Debug.Log ("DecreaseHealth");
//			
//		}
//	}

	void Start ()
	{
		uI_Ctrl = GameObject.Find ("_UI_Ctrl").GetComponent<UI_Ctrl> ();
	}

	void OnCollisionEnter (Collision col) {
		
		if (col.transform.tag == "Bullet") {
			DecreaseHealth();
			Debug.Log ("DecreaseHealth");

			
		}
		else if (col.transform.tag == "Laser") {
			DecreaseHealth();
			Debug.Log ("DecreaseHealth");
			
		}
	}

	void DecreaseHealth()
	{
		if(photonView.isMine == true)
		{
			if(cur_Health > 0)
			{
				MySource.PlayOneShot(HurtSound);
				cur_Health -= 5f;
				float calc_Health = cur_Health / max_Health;
				SetHealthBar (calc_Health);
			}
		}
	}
	 public void SetHealthBar(float myHealth)
	{
		uI_Ctrl.healthBar.transform.localScale = new Vector3 (myHealth, uI_Ctrl.healthBar.transform.localScale.y, uI_Ctrl.healthBar.transform.localScale.z);
		if (myHealth == 0) 
		{
			MySource.PlayOneShot(DeathSound);
			Debug.Log ("I died TAT");
//			Destroy (this.gameObject, 1);
//			GameOver.SetActive (true);
//			DeadState();
			photonView.RPC ("RPC_DeadState", PhotonTargets.All);
		}
	}

	[PunRPC]
	private void RPC_DeadState()
	{
		uI_Ctrl.GameOver.SetActive (true);
		//Either use timescale or create a dead_Bool that influences movement f.x.
		Time.timeScale = 0;
	}

}
