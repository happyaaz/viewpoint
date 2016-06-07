using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldDetection : MonoBehaviour {


	public GameObject lasetRefl_go;
	public GameObject laserEmitter_go;
	public GameObject laserDest_go;
	public LayerMask lm;
	public LineRenderer shield_lr;
	public LineRenderer laserEmitter_lr;
	public bool set_bool = false;
	public bool second_bool = false;

	private GameObject emitterOfRefl_go;

	void Awake () {
		
		laserEmitter_lr.SetWidth (0.02f, 0.02f);
		laserEmitter_lr.SetPosition (0, laserEmitter_go.transform.position);
		laserEmitter_lr.SetPosition (1, laserDest_go.transform.position);
	}


	void OnTriggerEnter (Collider col) {

		if (set_bool == false)
		{
			CreateLaserRefl ();
		}
	}


	void CreateLaserRefl () {

		Ray rayFromLaserEmitter = new Ray (laserEmitter_go.transform.position, laserDest_go.transform.position - laserEmitter_go.transform.position);
		RaycastHit hitFromEmitter;
		//Debug.DrawLine (laserEmitter_go.transform.position, (laserDest_go.transform.position - laserEmitter_go.transform.position) * 100, Color.blue, 100);
		
		if (Physics.Raycast (rayFromLaserEmitter, out hitFromEmitter, 100, lm)) {
			emitterOfRefl_go = Instantiate (lasetRefl_go, hitFromEmitter.point, hitFromEmitter.transform.rotation) as GameObject;
			emitterOfRefl_go.transform.parent = hitFromEmitter.transform;

			emitterOfRefl_go.AddComponent <LineRenderer> ();
			shield_lr = emitterOfRefl_go.GetComponent <LineRenderer> ();
			shield_lr.SetWidth (0.02f, 0.02f);
			set_bool = true;




			Debug.Log ("MYA");
		}
	}



	void OnTriggerStay (Collider col) {


		if (emitterOfRefl_go != null)
		{
			if (col.tag == "Shield")
			{
				Ray rayFromLaserEmitter = new Ray (laserEmitter_go.transform.position, laserDest_go.transform.position - laserEmitter_go.transform.position);
				RaycastHit hitFromEmitter;
				//Debug.DrawLine (laserEmitter_go.transform.position, laserEmitter_go.transform.forward * 100, Color.black, 100);

				if (Physics.Raycast (rayFromLaserEmitter, out hitFromEmitter, 15, lm))
				{
					laserEmitter_lr.SetPosition (1, hitFromEmitter.point);
					emitterOfRefl_go.transform.position = hitFromEmitter.point;
					Ray rayFromShield = new Ray (hitFromEmitter.point, col.transform.forward);
					RaycastHit hitFromShield;
					//  add LM for the walls
					if (Physics.Raycast (rayFromShield, out hitFromShield, 100))
					{
						if (hitFromShield.transform.tag == "Keypad")
						{
							hitFromShield.transform.gameObject.GetComponent <OpenGunnarPuzzle> ().DestroyFunction ();
						}
							//Debug.DrawLine (hitFromEmitter.point, hitFromShield.point * 100, Color.red, 100);
	//					Debug.Log (hitFromEmitter.point);
	//					Debug.Log (hitFromShield.point);
						shield_lr.SetPosition (0, emitterOfRefl_go.transform.position);
						shield_lr.SetPosition (1, hitFromShield.point);
						
					}
				}
			}
		}
		else
		{
			CreateLaserRefl ();
		}
	}


	void OnTriggerExit (Collider col) {

		if (col.tag == "Shield")
		{
			Destroy (emitterOfRefl_go);
			shield_lr = null;
			set_bool = false;
			Awake ();
		}
	}
}
