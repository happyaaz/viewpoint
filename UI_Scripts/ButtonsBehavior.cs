using UnityEngine;
using System.Collections;

public class ButtonsBehavior : MonoBehaviour {
	public GameObject OptionsMenu;
	public AudioClip ButtonHover;
	public AudioClip ButtonEnter;
	public GameObject exitBtt_GO;

	// Use this for initialization
	void Start () {
		OptionsMenu.SetActive (false);

		#if UNITY_STANDALONE
		exitBtt_GO.SetActive (true);
		#endif
//		GetComponent<AudioSource>() = GetComponent<AudioSource>();
	}

	public void OptionsOn(){
		OptionsMenu.SetActive (true);
	}
	public void OptionsOff(){
		OptionsMenu.SetActive (false);
	}

	//If standalone build then you can Quit/Close it by hitting the Quit button
	public void QuitApplication ()
	{
		Application.Quit ();
	}

	public void BttHover(){
		GetComponent<AudioSource>().PlayOneShot(ButtonHover, 0.7F);
	}

	public void BttEnter(){

		Debug.Log ("Button clicked");
		GetComponent<AudioSource>().PlayOneShot(ButtonEnter, 0.7F);
	}
}
