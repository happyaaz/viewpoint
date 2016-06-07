using UnityEngine;
using System.Collections;

public class GunnarController_New : Photon.MonoBehaviour {
	
//	private Transform turret_tr;
//	private float curSpeed_fl, targetSpeed_fl, rotSpeed_fl;
//	private float maxForwardSpeed_fl = 9.0f;
//	private float maxBackwardSpeed_fl = -9.0f;
////	public Animator GunnarAnimator;
//
//	
//	//Bullet shooting rate
//	protected float shootRate_fl = 0.5f;
//	protected float elapsedTime_fl;
//
//	// --Network variables START--
//	//Syncing movement accross the network
//	private float syncTime = 0f;
//	private Vector3 correctPlayerPos;
//	private Quaternion correctPlayerRot;
//	// --Network variables END--

	//New
	public float speed = 10f;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Rigidbody myRigidbody;
	private float rotSpeed_fl;
	private Vector3 eulerAngleVelocity = Vector3.zero;
	private Quaternion syncStartRotation;
	private Quaternion syncEndRotation;

	void Start() 
	{

		myRigidbody = GetComponent<Rigidbody>();

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
				InputMovement();
			}
		}
		else
		{
			SyncedMovement();
		}
	}
	
	
	void InputMovement()
	{
		if (Input.GetKey(KeyCode.W))
			myRigidbody.MovePosition(myRigidbody.position + transform.forward * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.S))
			myRigidbody.MovePosition(myRigidbody.position - transform.forward * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.D))
		{
			Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity + Vector3.up * rotSpeed_fl * Time.deltaTime);
			myRigidbody.MoveRotation (myRigidbody.rotation * deltaRotation);
//			myRigidbody.MovePosition(myRigidbody.position + Vector3.right * speed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity - Vector3.up * rotSpeed_fl * Time.deltaTime);
			myRigidbody.MoveRotation (myRigidbody.rotation * deltaRotation);
//			myRigidbody.MovePosition(myRigidbody.position - Vector3.right * speed * Time.deltaTime);
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		//Position Sync:  Not very smooth tbh.
		myRigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		//Rotation Sync:  Works but to smooth it it might need Euler stuff(?). Also try MovePosition for even smoother rotation? Needed?
		myRigidbody.rotation = Quaternion.Slerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);   
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(myRigidbody.position);
			stream.SendNext(myRigidbody.velocity);
			stream.SendNext(myRigidbody.rotation);
		}
		else
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			Vector3 syncVelocity = (Vector3)stream.ReceiveNext();
			Quaternion syncRotation = (Quaternion)stream.ReceiveNext();
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = myRigidbody.position;

			syncEndRotation = syncRotation;
			syncStartRotation  = myRigidbody.rotation;
		}
	}
	
//	void UpdateControl() {
//
//		
//		//  if we press any MOVE keys - move. Either - always set it to zero
//		if (Input.GetKey(KeyCode.W))
//		{
//			targetSpeed_fl = maxForwardSpeed_fl;
////			GunnarAnimator.SetInteger("IsDriving",1);
//
//		}
//		else if (Input.GetKey(KeyCode.S))
//		{
//			targetSpeed_fl = maxBackwardSpeed_fl;
////			GunnarAnimator.SetInteger("IsDriving",1);
//		}
//		else
//		{
//			targetSpeed_fl = 0;
////			GunnarAnimator.SetInteger("IsDriving",0);
//		}
//		
//		//  rotate
//		if (Input.GetKey(KeyCode.A))
//		{
//			transform.Rotate(0, -rotSpeed_fl * Time.deltaTime, 0.0f);
////			GunnarAnimator.SetInteger("IsDriving",1);
//		}
//		else if (Input.GetKey(KeyCode.D))
//		{
//			transform.Rotate(0, rotSpeed_fl * Time.deltaTime, 0.0f);
////			GunnarAnimator.SetInteger("IsDriving",1);
//		}
//		
//		curSpeed_fl = Mathf.Lerp (curSpeed_fl, targetSpeed_fl, 7.0f * Time.deltaTime);
//
//		//  FIX Gunnar!!! It should fall down when needed
//		if (curSpeed_fl != 0)
//		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed_fl);
//	}

//	//Next two functions are to sync the movement accross the network
//	void SyncedMovement () 
//	{		
//		syncTime += Time.deltaTime;
//		transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
//		transform.rotation = Quaternion.Slerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
//	}	
//	
//	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		if (stream.isWriting)
//		{
//			stream.SendNext(transform.position);
//			stream.SendNext(transform.rotation);
//		}
//		else
//		{
//			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
//			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
//		}
//	}
}