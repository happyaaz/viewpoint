using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	/*  you still might be able to see through the wall
	 * Possible solution - change the point to look at or change the rotation of the camera
	*/

	public Transform target_tr;

	[System.Serializable]
	public class PositionSettings
	{
		//  should change it later - Y pos
		public Vector3 targetPosOffset_vt3 = new Vector3 (0, 0.5f, 0);
		//  how smooth the camera is gonna move
		public float lookSmooth_fl = 100f;
		public float zoomSmooth_fl = 100f;
		public float distanceFromTarget_fl = -8;
		public float maxZoom_fl = -2;
		public float minZoom = -15;


		public bool smoothFollow_bool = true;
		public float smooth_fl = 0.05f;


		[HideInInspector]
		public float newDistance_fl = -8;
		[HideInInspector]
		public float adjustmentDistance_fl = -8;
	}


	[System.Serializable]
	public class OrbitSettings
	{
		public float xRotation_fl = -20;
		public float yRotation_fl = -180;
		//  don't want the camera to go below the floor or above the character
		public float maxXRotation_fl = 25;
		public float minXRotation_fl = -85;
		//  how smooth the camera is gonna rotate
		public float vOrbitSmooth_fl = 150;
		public float hOrbitSmooth_fl = 150;
	}


	[System.Serializable]
	public class InputSettings
	{
		//  snap back to the CC's back
		public string ORBIT_HORIZONTAL_SNAP_STR = "OrbitHorizontalSnap";
		public string ORBIT_HORIZONTAL = "OrbitHorizontal";
		public string ORBIT_VERTICAL = "OrbitVertical";
		public string ZOOM = "Mouse ScrollWheel";
	}


	[System.Serializable]
	public class DebugSettings
	{
		public bool drawDesiredCollisionLines = true;
		public bool drawAdjustedCollisionLines = true;
	}



	public PositionSettings position_class = new PositionSettings ();
	public OrbitSettings orbit_class = new OrbitSettings ();
	public InputSettings input_class = new InputSettings ();
	public DebugSettings debug_class = new DebugSettings ();
	public CollisionHandler collision_class = new CollisionHandler ();

	Vector3 targetPos_vt3 = Vector3.zero;
	//  camera not colliding - use this
	Vector3 destination_vt3 = Vector3.zero;
	//  camera colliding - use that
	Vector3 adjustedDestination_vt3 = Vector3.zero;
	Vector3 camVel_vt3 = Vector3.zero;
	CharController cc_scr;
	float vOrbitInput_fl, hOrbitInput_fl, zoomInput_fl, hOrbitSnapInput_fl;

	bool rightMouseButtonWasPressed_bool = false;


	void Start () {
		
		SetCameraTarget (target_tr);
		//  to make sure the camera is in the correct position in the beginning
		MoveToTarget ();

		collision_class.Initialize (Camera.main);
		collision_class.UpdateCameraClipPoints (transform.position, transform.rotation, ref collision_class.adjustedCameraClipPoints_vt3arr);
		collision_class.UpdateCameraClipPoints (destination_vt3, transform.rotation, ref collision_class.desiredCameraClipPoints_vt3arr);

	}
	

	void FixedUpdate () {
		//  moving
		MoveToTarget ();
		//  rotating
		LookAtTarget ();

		OrbitTarget ();
		//  we constantly need to update them
		collision_class.UpdateCameraClipPoints (transform.position, transform.rotation, ref collision_class.adjustedCameraClipPoints_vt3arr);
		collision_class.UpdateCameraClipPoints (destination_vt3, transform.rotation, ref collision_class.desiredCameraClipPoints_vt3arr);
		for (int i = 0; i < 5; i ++)
		{
			if (debug_class.drawDesiredCollisionLines == true)
			{
				Debug.DrawLine (targetPos_vt3, collision_class.desiredCameraClipPoints_vt3arr [i], Color.white);
			}
			if (debug_class.drawAdjustedCollisionLines == true)
			{
				Debug.DrawLine (targetPos_vt3, collision_class.adjustedCameraClipPoints_vt3arr [i], Color.black);
			}
		}
		
		//  modifies the colliding bool
		collision_class.CheckColliding (targetPos_vt3); //  using raycasts
		//  how far from the camera we need to move the camera
		position_class.adjustmentDistance_fl = collision_class.GetAdjustedDistanceWithRay (targetPos_vt3);
	}


	void Update () {

		ZoomInOnTarget ();
		GetInput ();
	}


	void GetInput () {

		//vOrbitInput_fl = Input.GetAxisRaw (input_class.ORBIT_VERTICAL);
		if (Input.GetMouseButtonDown (1))
		{
			rightMouseButtonWasPressed_bool = true;
		}
		if (Input.GetMouseButton (1))
		{
			//hOrbitInput_fl = Input.GetAxisRaw (input_class.ORBIT_HORIZONTAL);
			hOrbitInput_fl = Input.GetAxisRaw ("Mouse X");
			vOrbitInput_fl = -Input.GetAxisRaw ("Mouse Y");
		}
		else if (Input.GetMouseButtonUp (1)) 
		{
			//  snap back
			orbit_class.yRotation_fl = -180;
			orbit_class.xRotation_fl = 2;
			rightMouseButtonWasPressed_bool = false;
		}
		//hOrbitSnapInput_fl = Input.GetAxisRaw (input_class.ORBIT_HORIZONTAL_SNAP_STR);
		zoomInput_fl = Input.GetAxisRaw (input_class.ZOOM);
	}


	//  might want to use it for other targets as well
	void SetCameraTarget (Transform _t) {
		
		target_tr = _t;
		
		if (target_tr != null) 
		{
			if (target_tr.GetComponent <CharController> ())
			{
				cc_scr = target_tr.GetComponent <CharController> ();
			}
			else
			{
				Debug.LogError ("The camera's target needs a CC");
			}
		}
		else
		{
			Debug.LogError ("Your camera needs a target");
		}
	}


	void MoveToTarget () {

		targetPos_vt3 = target_tr.position + position_class.targetPosOffset_vt3;
		//  behind, thats why "- ... * ..."
		destination_vt3 = Quaternion.Euler (orbit_class.xRotation_fl, orbit_class.yRotation_fl + target_tr.eulerAngles.y, 0) * -Vector3.forward * position_class.distanceFromTarget_fl;

		destination_vt3 += targetPos_vt3;
		//transform.position = destination_vt3;


		if (collision_class.colliding_bool == true)
		{
			adjustedDestination_vt3 = Quaternion.Euler (orbit_class.xRotation_fl, orbit_class.yRotation_fl + target_tr.eulerAngles.y, 0) * Vector3.forward * position_class.adjustmentDistance_fl;
			adjustedDestination_vt3 += targetPos_vt3;

			if (position_class.smoothFollow_bool == true)
			{
				//  use smooth damp function
				transform.position = Vector3.SmoothDamp (transform.position, adjustedDestination_vt3, ref camVel_vt3, position_class.smooth_fl);
			}
			else
			{
				transform.position = adjustedDestination_vt3;
			}
		}
		else
		{
			if (position_class.smoothFollow_bool == true)
			{
				//  use smooth damp function
				transform.position = Vector3.SmoothDamp (transform.position, destination_vt3, ref camVel_vt3, position_class.smooth_fl);

			}
			else
			{
				transform.position = destination_vt3;
			}
		}
	}


	void LookAtTarget () {

		Quaternion _targetRotation_vt3 = Quaternion.LookRotation (targetPos_vt3 - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, _targetRotation_vt3, position_class.lookSmooth_fl * Time.deltaTime);	
	
	}


	void OrbitTarget () {

		if (rightMouseButtonWasPressed_bool == true)
		{

			orbit_class.xRotation_fl += -vOrbitInput_fl * orbit_class.vOrbitSmooth_fl * Time.deltaTime;
			orbit_class.yRotation_fl += -hOrbitInput_fl * orbit_class.hOrbitSmooth_fl * Time.deltaTime;


			//  don't let go beyong the ground
			if (orbit_class.xRotation_fl > orbit_class.maxXRotation_fl)
			{
				orbit_class.xRotation_fl = orbit_class.maxXRotation_fl;
			}
			if (orbit_class.xRotation_fl < orbit_class.minXRotation_fl)
			{
				orbit_class.xRotation_fl = orbit_class.minXRotation_fl;
			}
		}
	}


	void ZoomInOnTarget () {

		position_class.distanceFromTarget_fl += zoomInput_fl * position_class.zoomSmooth_fl * Time.deltaTime;
		if (position_class.distanceFromTarget_fl > position_class.maxZoom_fl)
		{
			position_class.distanceFromTarget_fl = position_class.maxZoom_fl;
		}
		if (position_class.distanceFromTarget_fl < position_class.minZoom)
		{
			position_class.distanceFromTarget_fl = position_class.minZoom;
		}
	}



	[System.Serializable]
	public class CollisionHandler
	{
		//  will collide with
		public LayerMask collisionLayer_lm;

		[HideInInspector]
		public bool colliding_bool = false;
		[HideInInspector]
		//  clip points surrounding the camera at the current position
		public Vector3 [] adjustedCameraClipPoints_vt3arr;
		[HideInInspector]
		//  clip points surrounding the camera at the desired position
		public Vector3 [] desiredCameraClipPoints_vt3arr;

		Camera camera_cam;


		public void Initialize (Camera _cam) {

			camera_cam = _cam;
			adjustedCameraClipPoints_vt3arr = new Vector3 [5];
			desiredCameraClipPoints_vt3arr = new Vector3[5];
		}
		
		
		public void UpdateCameraClipPoints (Vector3 _cameraPosition, Quaternion _atRotation, ref Vector3 [] _intoArray) {

			if (!camera_cam)
				return;

			//  new clip points every frame, so clear the array
			_intoArray = new Vector3 [5];

			// xyz coordinates for clip points
			float z = camera_cam.nearClipPlane;
			//  modify the value for increasing the size of the collision space
			float x = Mathf.Tan (camera_cam.fieldOfView / 3.41f) * z;
			float y = x / camera_cam.aspect;

			//  find the clip points and assign to the array

			//  top left
			//  added and rotated the point relative to the camera
			_intoArray [0] = (_atRotation * new Vector3 (-x, y, z)) + _cameraPosition;
			//  top right
			_intoArray [1] = (_atRotation * new Vector3 (x, y, z)) + _cameraPosition;
			//  bottom left
			_intoArray [2] = (_atRotation * new Vector3 (-x, -y, z)) + _cameraPosition;
			//  bottom right
			_intoArray [3] = (_atRotation * new Vector3 (x, -y, z)) + _cameraPosition;
			//  camera's position
			//  "forward" - because it gives more room behind the camera to collide with
			_intoArray [4] = _cameraPosition - camera_cam.transform.forward;
		}


		bool CollisionDetectedAtClipPoints (Vector3 [] _clipPoints, Vector3 _fromPosition) {

			for (int i = 0; i < _clipPoints.Length; i ++)
			{
				Ray ray = new Ray (_fromPosition, _clipPoints [i] - _fromPosition);
				float distance = Vector3.Distance (_clipPoints [i], _fromPosition);
				if (Physics.Raycast (ray, distance, collisionLayer_lm))
				{
					return true;
				}
			}

			return false;
		}


		//  returns the distance the camera need to be away from the target
		public float GetAdjustedDistanceWithRay (Vector3 _from) {

			float distance_fl = -1;

			for (int i = 0; i < desiredCameraClipPoints_vt3arr.Length; i ++)
			{
				//  find the shortest distance between the colliding points
				Ray ray = new Ray (_from, desiredCameraClipPoints_vt3arr [i] - _from);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit))
				{
					if (distance_fl == -1)
					{
						distance_fl = hit.distance;
					}
					else
					{
						if (hit.distance < distance_fl)
						{
							distance_fl = hit.distance;
						}
					}
				}
			}

			//  if still -1 - no collision
			if (distance_fl == -1) 
			{
				return 0;
			}
			else
			{
				return distance_fl;
			}
		}

		//  update the colliding boolean, check if colliding is true or false and determines
		//  how and where we will move the camera at a particular moment of time
		public void CheckColliding (Vector3 _targetPosition) {

			if (CollisionDetectedAtClipPoints (desiredCameraClipPoints_vt3arr, _targetPosition)) 
			{
				colliding_bool = true;
			} 
			else 
			{
				colliding_bool = false;
			}
		}
	}

}
