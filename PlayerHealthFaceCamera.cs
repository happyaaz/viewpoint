using UnityEngine;
using System.Collections;

public class PlayerHealthFaceCamera : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }
}
