using UnityEngine;
using System.Collections;

public class PickUpKey : MonoBehaviour {

    public AudioClip WeeIwasPickedUp;
    public AudioSource MySource;
	public GameObject Star_Particles;


	void Start () {

        MySource = this.gameObject.GetComponent<AudioSource>();
	
	}


	void OnTriggerEnter (Collider col) {
		
		if (col.tag == "Gunnar" || col.tag == "Russky" || col.tag == "ByongYang")
		{

			if (this.name.Contains ("1"))
			{
                MySource.PlayOneShot(WeeIwasPickedUp);
                KeyMaster.km_scr.Key1picked ();
			}
			else if (this.name.Contains ("2"))
			{
                MySource.PlayOneShot(WeeIwasPickedUp);
                KeyMaster.km_scr.Key2picked ();
			}
			else if (this.name.Contains ("3"))
			{
                MySource.PlayOneShot(WeeIwasPickedUp);
                KeyMaster.km_scr.Key3picked ();
			}

			GameObject.Find("Stars_Par").SetActive(false);
			Destroy (this.gameObject);

		}
	}
}
