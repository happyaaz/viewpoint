using UnityEngine;
using System.Collections;

public class GunnarController : Photon.MonoBehaviour {
	
	private Transform turret_tr;
	private float curSpeed_fl, targetSpeed_fl, rotSpeed_fl;
	private float maxForwardSpeed_fl = 9.0f;
	private float maxBackwardSpeed_fl = -9.0f;
//	public Animator GunnarAnimator;

	
	//Bullet shooting rate
	protected float shootRate_fl = 0.5f;
	protected float elapsedTime_fl;

	// --Network variables START--
	//Syncing movement accross the network
	private float syncTime = 0f;
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	// --Network variables END--

	void Start() 
	{
		//Tank Settings
		rotSpeed_fl = 150.0f;
//		GunnarAnimator = gameObject.GetComponent <Animator> ();

		//Check if this is the version of your character you are supposed to control
		if(photonView.isMine == true)
		{
//			GameObject.Find ("Gunnar_UI_Holder").SetActive (true);
//			GameObject.Find ("Russki_Sight").SetActive (false);
		}
		else if(photonView.isMine == false)
		{
			GameObject.Find ("Gunnar_UI_Holder").SetActive (false);
		}

	}
	
	
	void Update () {

		//		Singleplayer
		//		if (GameMaster.gm_scr.gunnarActive_bool == true)
		//		{
		
		//Check if this is the version of your character you are supposed to control
		if(photonView.isMine == true)
		{
			if(GettingKeysDown.iAmHacking == false)
			{
				UpdateControl();
			}
		}
		else
		{
			SyncedMovement();
		}
	}
	
	

	
	
	void UpdateControl() {

		
		//  if we press any MOVE keys - move. Either - always set it to zero
		if (Input.GetKey(KeyCode.W))
		{
			targetSpeed_fl = maxForwardSpeed_fl;
//			GunnarAnimator.SetInteger("IsDriving",1);

		}
		else if (Input.GetKey(KeyCode.S))
		{
			targetSpeed_fl = maxBackwardSpeed_fl;
//			GunnarAnimator.SetInteger("IsDriving",1);
		}
		else
		{
			targetSpeed_fl = 0;
//			GunnarAnimator.SetInteger("IsDriving",0);
		}
		
		//  rotate
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0, -rotSpeed_fl * Time.deltaTime, 0.0f);
//			GunnarAnimator.SetInteger("IsDriving",1);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0, rotSpeed_fl * Time.deltaTime, 0.0f);
//			GunnarAnimator.SetInteger("IsDriving",1);
		}
		
		curSpeed_fl = Mathf.Lerp (curSpeed_fl, targetSpeed_fl, 7.0f * Time.deltaTime);

		//  FIX Gunnar!!! It should fall down when needed
		if (curSpeed_fl != 0)
		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed_fl);
	}

	//Next two functions are to sync the movement accross the network
	void SyncedMovement () 
	{		
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
		transform.rotation = Quaternion.Slerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
	}	
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}