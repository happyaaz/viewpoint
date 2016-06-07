using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyMaster : MonoBehaviour {


	public Text key1picked_txt;
	public Text key2picked_txt;
	public Text key3picked_txt;

	public int numberOfPickedKeys_int = 0; 

	public static KeyMaster km_scr;
	public AudioClip WeeIwasPickedUp;
	public AudioSource MySource;

	void Awake () {

		km_scr = this;
	}
	

	public void Key1picked () {
		MySource.PlayOneShot(WeeIwasPickedUp);
		key1picked_txt.text = "Key 1 PICKED";
		IncreaseNumberOfPickedKeys ();
	}


	public void Key2picked () {
		MySource.PlayOneShot(WeeIwasPickedUp);
		key2picked_txt.text = "Key 2 PICKED";
		IncreaseNumberOfPickedKeys ();
	}


	public void Key3picked () {
		MySource.PlayOneShot(WeeIwasPickedUp);
		key1picked_txt.text = "Key 3 PICKED";
		IncreaseNumberOfPickedKeys ();
	}


	private void IncreaseNumberOfPickedKeys () {

		numberOfPickedKeys_int ++;
	}
}
