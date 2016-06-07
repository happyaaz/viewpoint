using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelectionButtons : MonoBehaviour {
	public GameObject lightGun;
	public GameObject lightRus;
	public GameObject lightBy;
	public AudioClip ButtonHover;
	public AudioClip ButtonEnter;
	public GameObject Rus;
	public GameObject By;
	public GameObject Gun;
	public bool RusRot=false;
	public bool GunRot=false;
	public bool ByRot=false;
	public GameObject questionMark;
	
	public Ready_Ctrl rc_scr;
	CharacterSelection_Ctrl characterSelection_Ctrl;
	
	// Use this for initialization
	void Start () 
	{

		characterSelection_Ctrl = GetComponent <CharacterSelection_Ctrl>();

		lightGun.SetActive(false);
		lightBy.SetActive(false);
		lightRus.SetActive(false);

		Rus.SetActive (false);
		Gun.SetActive (false);
		By.SetActive (false);
		questionMark.SetActive (true);

	}
	//lighted Russky
	public void LightedRusOn()
	{
//		if (characterSelection_Ctrl.huy == false) {
		lightRus.SetActive (true);
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (false);
		Rus.SetActive (true);
		GetComponent<AudioSource>().PlayOneShot(ButtonHover, 0.7F);
		RusRot = true;
//			questionMark.SetActive (false);
//		}
	}
	public void LightedRusOff()
	{
//		if (characterSelection_Ctrl.huy == false) {
		if(Rus != null)
		{
			Rus.SetActive (false);
		}
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (true);
		lightRus.SetActive (false);
		RusRot = false;
//		}
	}
	//lighted gunnar
	public void LightedGunnarON()
	{
//		if (characterSelection_Ctrl.huy == false) {
		lightGun.SetActive (true);
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (false);
		Gun.SetActive (true);
		GunRot = true;
//			questionMark.SetActive (false);
		GetComponent<AudioSource>().PlayOneShot(ButtonHover, 0.7F);

//			Debug.Log ("Showing Gunnar");
//		}
	}
	
	
	public void LightedGunnarOFF()
	{
//		if (characterSelection_Ctrl.huy == false) {
			
		Gun.SetActive (false);
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (true);
		lightGun.SetActive (false);
		GunRot = false;
//			questionMark.SetActive (true);

//			Debug.Log ("Hiding Gunnar");
//		}
	}
	//lighted by
	public void LightedByOn()
	{
//		if (characterSelection_Ctrl.huy == false) {
		lightBy.SetActive (true);
		GetComponent<AudioSource>().PlayOneShot(ButtonHover, 0.7F);
//			questionMark.SetActive (false);
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (false);
		ByRot = true;
		By.SetActive (true);
//		}
	}
	public void LightedByOff()
	{
//		if (characterSelection_Ctrl.huy == false) {
		lightBy.SetActive (false);
//			questionMark.SetActive (true);
		By.SetActive (false);
		characterSelection_Ctrl.chosenCharacter_GO.SetActive (true);			
		ByRot = false;
//		}
	}
	
	public void BackButton(){
		Application.LoadLevel (0);

	}
	
	// Update is called once per frame
	void Update () {
		questionMark.transform.Rotate (0, 40 * Time.deltaTime, 0);
		if (RusRot) {
			Rus.transform.Rotate (0, 40 * Time.deltaTime, 0);
		}
		if (GunRot) {
			Gun.transform.Rotate (0, 40 * Time.deltaTime, 0);
		}
		if (ByRot) {
			By.transform.Rotate (0, 40 * Time.deltaTime, 0);
			
		}
	}
}
