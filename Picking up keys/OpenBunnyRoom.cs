using UnityEngine;
using System.Collections;

public class OpenBunnyRoom : MonoBehaviour {


	public GameObject door_go;

	void Start () {

		//OpenDoor ();
	}


	void OnTriggerEnter (Collider col) {
		
		if (col.tag == "Gunnar" || col.tag == "Russky" || col.tag == "ByongYang")
		{
			OpenDoor ();
		}
	}


	void OpenDoor () {

		if (KeyMaster.km_scr.numberOfPickedKeys_int == 3)
		{
			try
			{
				door_go.GetComponent<Animation>().Play ();
			}
			catch (System.Exception e) 
			{
				Destroy (door_go);
			}
		}
		else
		{
			Debug.Log (KeyMaster.km_scr.numberOfPickedKeys_int);
		}
	}
}
