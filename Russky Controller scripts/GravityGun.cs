using UnityEngine;
using System.Collections;

public class GravityGun : Photon.MonoBehaviour {


    public AudioClip Sucking;
    public AudioClip Blowing;
    private AudioSource Mysource;
	public GameObject pulledObject_go;
	Rigidbody poRB_rb;
	public LayerMask lm_lm;
	public Camera cam;
	public int force_int;
	public GameObject gravGunPos_go;

	private bool pulling_Bool = false;
	public bool ReadyToSuck = true;
	private float cloneForce_Float = 0;
	
	public bool ggFeatureIsActivated_bool = true;
	//The clone doesn't have a camera so simply RPCing the code that uses the cameras isn't going to work.

	public GameObject Pulling_Particles;
	public GameObject Shooting_Particles;

	void Start ()
	{
		cloneForce_Float = force_int;
        Mysource = this.gameObject.GetComponent<AudioSource>();
	}

	void Update () {
	
		if (ggFeatureIsActivated_bool == true)
		{
			if (Input.GetMouseButtonDown (1) && pulledObject_go == null)
			{
				pulling_Bool = false;

				Ray ray = cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 100, lm_lm))
				{
					Debug.Log ("Hit object: " + hit.transform.gameObject.name);
					pulledObject_go = hit.transform.gameObject;

					Debug.Log ("PulledObject: " + pulledObject_go);
					poRB_rb = pulledObject_go.GetComponent <Rigidbody> ();
					GetComponent<PhotonView>().RPC("TryToPullWithGun", PhotonTargets.All, pulledObject_go.name);
				}
				//Debug.DrawRay (this.transform.position, this.transform.forward * 100, Color.green, 10);


			}
			else if (Input.GetMouseButton (1) && pulledObject_go != null && !Input.GetMouseButtonDown (0)) 
			{
				GetComponent<PhotonView>().RPC("PullObjectTowardsGun", PhotonTargets.All);

				Debug.Log ("pulling_Bool: " + pulling_Bool);
			}
			else if (Input.GetMouseButtonUp (1)) 
			{
				pulling_Bool = false;
				DropHeldObject ();

			}
			else if (Input.GetMouseButtonDown (0) && pulledObject_go != null && Input.GetMouseButton (1)) 
			{
				pulling_Bool = false;
				GetComponent<PhotonView>().RPC("ShootObjectAway", PhotonTargets.All);
                Mysource.PlayOneShot(Blowing);
			}

			//BUG: It keeps running stuff inside the bool even if it should be false
			if(pulling_Bool == true)
			{
				//Pulling_Particles.SetActive(false);
				Debug.Log ("Pulling");
				
				if (Vector3.Distance (this.transform.position, pulledObject_go.transform.position) > 2)
				{
	//				Debug.Log ("Distance is more than 2");
	//				Debug.Log ("PulledObject: " + pulledObject_go.name);

					if(photonView.isMine != true)
					{
						cloneForce_Float = cloneForce_Float + 0.1f;
						poRB_rb.AddForce (-this.transform.forward * cloneForce_Float, ForceMode.Acceleration);
					}
					else
					{
						poRB_rb.AddForce (-this.transform.forward * force_int, ForceMode.Acceleration);
						Debug.Log ("Adding the force");
					}
				}
				else
				{
					poRB_rb.useGravity = false;
					poRB_rb.isKinematic = true;
					pulledObject_go.transform.position = gravGunPos_go.transform.position + Vector3.up * 0.9f;
					pulledObject_go.transform.rotation = gravGunPos_go.transform.rotation;
					poRB_rb.transform.parent = this.transform;
				}
			}
		}
	}

	public void DropHeldObject () {

		if (pulledObject_go != null) 
		{
			GetComponent<PhotonView>().RPC("DropPulledObject", PhotonTargets.All);
		}
	}


	[PunRPC]
	void TryToPullWithGun (string _pulledObject_go_string)
	{
		pulling_Bool = false;

		pulledObject_go = GameObject.Find (_pulledObject_go_string);   //Really bad way of doing it. The object name needs to be unique. Need something better.
		poRB_rb = pulledObject_go.GetComponent <Rigidbody> ();
	}

	[PunRPC]
	void PullObjectTowardsGun ()
	{
		if(Mysource.isPlaying == false && ReadyToSuck == true)
		{
			StartCoroutine (SuckParticles ());
			Mysource.PlayOneShot(Sucking);
			ReadyToSuck = false;
		}


		pulling_Bool = true;
	}

	[PunRPC]
	void DropPulledObject ()
	{

		pulling_Bool = false;

		poRB_rb.useGravity = true;
		poRB_rb.isKinematic = false;
		poRB_rb.transform.parent = null;
		//  it should fall down
		pulledObject_go = null;
		poRB_rb = null;

		ReadyToSuck = true;
	}

	[PunRPC]
	void ShootObjectAway ()
	{
		StartCoroutine (BlowParticles ());
		pulling_Bool = false;

		poRB_rb.useGravity = true;
		poRB_rb.isKinematic = false;
		poRB_rb.transform.parent = null;
		
		poRB_rb.AddForce (this.transform.forward * force_int * 0.3f, ForceMode.Impulse);
		pulledObject_go = null;
		poRB_rb = null;

		if(Mysource.isPlaying == false && ReadyToSuck == false)
		{
			Mysource.PlayOneShot(Sucking);
			ReadyToSuck = true;
		}

//		ReadyToSuck = true;
	}

	private IEnumerator SuckParticles ()
	{
		Pulling_Particles.SetActive(true);
		yield return new WaitForSeconds (3f);
		Pulling_Particles.SetActive(false);
		Shooting_Particles.SetActive(false);
		
	}
	private IEnumerator BlowParticles ()
	{
		Shooting_Particles.SetActive(true);
		yield return new WaitForSeconds (3f);
		Pulling_Particles.SetActive(false);
		Shooting_Particles.SetActive(false);
		
	}
}