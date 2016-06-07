using UnityEngine;
using System.Collections;

public class GoIntoTutorial_Ctrl : MonoBehaviour {

	public Transform joiningTutorial_SpawnFP;
	public Transform joiningTutorial_SpawnSP;
	public Transform joiningTutorial_SpawnTP;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Gunnar")
		{
			col.transform.position  = joiningTutorial_SpawnFP.position;
			col.transform.rotation  = joiningTutorial_SpawnFP.rotation;
		}
		
		else if(col.tag == "ByongYang")
		{
			col.transform.position = joiningTutorial_SpawnSP.position;
			col.transform.rotation = joiningTutorial_SpawnSP.rotation;
		}
		else if(col.tag == "Russky")
		{
			col.transform.parent.position = joiningTutorial_SpawnTP.position;
			col.transform.parent.rotation = joiningTutorial_SpawnTP.rotation;
		}
	}
}
