using UnityEngine;
using System.Collections;

public class BY_OpenDoor : MonoBehaviour {


	public GameObject door_go;


	void Start () {
		
//		try
//		{
//			door_go.GetComponent<Animation>().Play ();
//		}
//		catch (System.Exception e) 
//		{
//			Destroy (door_go);
//		}
	}


	void OnTriggerEnter (Collider col) {


		if (col.tag == "ByongYang") {

			try
			{
				door_go.GetComponent<Animation>().Play ();
			}
			catch (System.Exception e) 
			{
				Destroy (door_go);
			}
		}
	}
}
