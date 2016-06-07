using UnityEngine;
using System.Collections;

public class CharController : Photon.MonoBehaviour {

	// --Network variables START--
	//The characters camera
	public GameObject secondPersCamera;	
	
	//Syncing movement accross the network
	private float syncTime = 0f;
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	public bool canMoveLift_bool;
	// --Network variables END--

	
	public BY_Lift byl_scr;

	[System.Serializable]
	public class MoveSettings 
	{
		public float forwardVelocity_fl = 12;
		public float rotationVelocity_fl = 100;
		public float jumpvelocity_fl = 25;
		public float distanceToBeGrounded_fl = 1;
		public LayerMask ground_lm;
	}

	[System.Serializable]
	public class PhysSettings 
	{
		//  how fast it will go down when not grounded
		public float downAccel_fl = 0.75f;
	}

	[System.Serializable]
	public class InputSettings 
	{
		//  won't move instantly once the input button is pressed
		public float inputDelay_fl = 0.1f;
	}


	public MoveSettings moveSettings_class = new MoveSettings ();
	public PhysSettings physSettings_class = new PhysSettings ();
	public InputSettings inputSettings_class = new InputSettings ();
    public AudioClip[] _footstepSounds;
    public AudioSource MySource;
    private float _stepCycle = 0f;
    private float _nextStep = 0f;
    private float _stepInterval;
    private bool playSounds;
    public bool isGrounded;

    //  since we are dealing with moving in all axises, we need a way to control things easier
    public Vector3 velocity_vt3 = Vector3.zero;

	private Quaternion targetRotation_qt;
	private Rigidbody _rb;
	private float forwardInput_fl, turnInput_fl, jumpInput_fl, strafing_fl;
//	public Animator ByongYangAnimator;


	public Quaternion TargetRotation_Field
	{
		get { return targetRotation_qt;}
	}


	private bool Grounded () {

		return Physics.Raycast (transform.position, Vector3.down, moveSettings_class.distanceToBeGrounded_fl, moveSettings_class.ground_lm);
	}


	void Awake () {

		targetRotation_qt = transform.rotation;
		_rb = GetComponent <Rigidbody> ();
		forwardInput_fl = turnInput_fl = jumpInput_fl = 0;
        MySource = this.gameObject.GetComponent<AudioSource>();
//		ByongYangAnimator = gameObject.GetComponent <Animator> ();


	}


	void Start () {

		if(photonView.isMine == true)
		{
			secondPersCamera.SetActive (true);
//			GameObject.Find ("Gunnar_UI_Holder").SetActive (false);
//			GameObject.Find ("Russki_Sight").SetActive (false);



		}	
	}


	void Update () {

		//		Singleplayer
		//		if (GameMaster.gm_scr.byongYangActive_bool == true)
		//		{
		
		//Check if this is the version of your cahracter you are supposed to control
		if(photonView.isMine == true)
		{
			GetInput ();
			DetectingStrafing ();
			Turn ();

			MoveLift ();
		}
		else
		{
			SyncedMovement();
		}
	}


	public void ChangeLift (string nameOfLift_str) {
	
		byl_scr = GameObject.Find (nameOfLift_str).GetComponent <BY_Lift> ();
		canMoveLift_bool = true;
	}


	void MoveLift () {

		if (Input.GetKeyDown (KeyCode.F))
		{
			if ( canMoveLift_bool == true)
			{
				if (byl_scr.gameObject.name.Contains ("_BY"))
				{
					if (byl_scr.isUp_bool == false)
					{
						Debug.Log ("Start up");
						PuzzleMaster.pm_scr.puzzle_Entrance_by_class.LiftUp ();
					}
					else
					{
						Debug.Log ("Start up");
						PuzzleMaster.pm_scr.puzzle_Entrance_by_class.LiftDown ();
					}
				}
				else
				{

					if (byl_scr.isUp_bool == false)
					{
						Debug.Log ("Start up");
						Debug.Log ("UP " + byl_scr.gameObject.transform.position);
						PuzzleMaster.pm_scr.insideElevators_class.LiftUp ();
					}
					else
					{
						Debug.Log ("Start down");
						Debug.Log ("DOWN " + byl_scr.gameObject.transform.position);
						PuzzleMaster.pm_scr.insideElevators_class.LiftDown ();
					}
				}
	//				PuzzleMaster.pm_scr.puzzle_Entrance_by_class.LiftUp ();
			}
		}
	}




	void FixedUpdate () {

		//		Singleplayer
		//		if (GameMaster.gm_scr.byongYangActive_bool == true)
		//		{
		if(photonView.isMine == true)
		{
			Run ();
			Strafing ();
			Jump ();
			_rb.velocity = transform.TransformDirection (velocity_vt3);
		}
		else
		{
			SyncedMovement();
		}
	}

    private void PlayFootStepAudio()
    {
        if (isGrounded) return;
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, _footstepSounds.Length);
        GetComponent<AudioSource>().clip = _footstepSounds[n];
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        // move picked sound to index 0 so it's not picked next time
        _footstepSounds[n] = _footstepSounds[0];
        _footstepSounds[0] = GetComponent<AudioSource>().clip;
    }
    private void ProgressStepCycle(float speed)
    {

        if (!(_stepCycle > _nextStep)) return;

            _nextStep = _stepCycle + _stepInterval;
			if (playSounds == true) {
                PlayFootStepAudio();
}
        }


    void DetectingStrafing () {
		
		strafing_fl = Input.GetAxis ("Strafing");
	}
	
	
	void Strafing () {
		
		if (Mathf.Abs (strafing_fl) > inputSettings_class.inputDelay_fl) 
		{
            PlayFootStepAudio();
            ProgressStepCycle(1f);
            velocity_vt3.x = moveSettings_class.forwardVelocity_fl * strafing_fl;
		}
		else
		{
			velocity_vt3.x = 0;
		}
	}



	void GetInput () {

	
		//  positive value - forward, negative - back
		forwardInput_fl = Input.GetAxis ("Vertical");


		//  positive value - right, negative - back
		turnInput_fl = Input.GetAxis ("Horizontal");
		

		jumpInput_fl = Input.GetAxisRaw ("Jump");  //  not=interpolated values - either -1, 0, 1, because jumping is not gonna have any smoothing.If > 0, space bar is pressed
	}


	void Run () {

		//  that's how we can check if we waited this deadzone
		if (Mathf.Abs (forwardInput_fl) > inputSettings_class.inputDelay_fl) 
		{
            PlayFootStepAudio();
            ProgressStepCycle(2f);
            velocity_vt3.z = moveSettings_class.forwardVelocity_fl * forwardInput_fl;
            
		}
		else
		{
			velocity_vt3.z = 0;
		}

//		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
//			ByongYangAnimator.SetInteger("IsMoving",1);
//		else
//			ByongYangAnimator.SetInteger("IsMoving",0);
	}


	void Turn () {

		if (Mathf.Abs (turnInput_fl) > inputSettings_class.inputDelay_fl) 
		{
            PlayFootStepAudio();
            ProgressStepCycle(1f);

            //  without * - if RV = 5, then it will only rotate by 5 degrees.
            //  with * - constant rotation
            targetRotation_qt *= Quaternion.AngleAxis (moveSettings_class.rotationVelocity_fl * turnInput_fl * Time.deltaTime, Vector3.up);

		}
		transform.rotation = targetRotation_qt;
	}


	void Jump () {

		if (jumpInput_fl > 0 && Grounded () == true)
		{
			//  jump
			velocity_vt3.y = moveSettings_class.jumpvelocity_fl;

		}
		else if (jumpInput_fl == 0 && Grounded () == true)
		{
			//  zero out velocity.y
			velocity_vt3.y = 0;
		}
		else
		{
			//  in the air and don't press the space bar
			//  decrease velocity.y
			velocity_vt3.y -= physSettings_class.downAccel_fl;

		}
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
