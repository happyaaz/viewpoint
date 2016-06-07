using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GettingKeysDown : MonoBehaviour {

	public List <string> hackingCode_list = new List <string> (); 
	public int durationOfPuzzle_int;
	public bool canType_bool = true;
	public Text timer_txt;
	private int numberOfKeysPressed_int;
	public Text code_txt;
	public Text payAttention_txt;
	private bool timeToPressEnter_bool = false;
	private int defaultTimerValue_int;


	public OpenGunnarPuzzle ogp_scr;
	public GameObject hacking_Canvas_GO;

	public static bool iAmHacking = false;

	public Text debug_Txt_1;
	public Text debug_Txt_2;
	public Text debug_Txt_3;
	public Text debug_Txt_4;


	void Start () {
		
		DefaultEverything ();
		//StartHacking ();
	}


	public void ItsTimeToStartHacking (OpenGunnarPuzzle _ogp_scr) {
	
		ogp_scr = _ogp_scr;
		StartHacking ();
	}



	void DefaultEverything () {
	
		timer_txt.text = string.Empty;
		payAttention_txt.text = string.Empty;
		code_txt.text = string.Empty;
		defaultTimerValue_int = durationOfPuzzle_int;
		canType_bool = false;
	}


	//  
	public void StartHacking () {

		debug_Txt_1.text = "StartHacking";

		iAmHacking = true;
		DefaultEverything ();
		StartCoroutine (TimerRanOut ());
		timer_txt.text = "" + defaultTimerValue_int;
		StartCoroutine ("ItsTimeToPressEnter");
		StartCoroutine ("DecreaseTimer");
		numberOfKeysPressed_int = 0;
		canType_bool = true;

		//  Gunnar shouldn't be able to move at all.
		//  Figure out why some of the buttons get selected when certain buttons get pressed
	}


	void Update () {

		if (Input.anyKeyDown && canType_bool == true) 
		{
			if (timeToPressEnter_bool == true)
			{
				if (Input.GetKeyDown (KeyCode.Return))
				{
					timeToPressEnter_bool = false;
					payAttention_txt.text = string.Empty;
				}
			}
			else
			{
				if (numberOfKeysPressed_int < hackingCode_list.Count)
				{
					if (hackingCode_list [numberOfKeysPressed_int] == string.Empty)
					{
						code_txt.text += System.Environment.NewLine;

						Debug.Log ("adding a line of code");
						debug_Txt_2.text = "adding a line of code";
					}
					else
					{
						if (hackingCode_list [numberOfKeysPressed_int].Contains ("{"))
						{
							code_txt.text += System.Environment.NewLine;
						}
						code_txt.text += hackingCode_list [numberOfKeysPressed_int];
						if (hackingCode_list [numberOfKeysPressed_int].Contains (";") || hackingCode_list [numberOfKeysPressed_int].Contains ("}"))
						{
							code_txt.text += System.Environment.NewLine;
						}
					}
					numberOfKeysPressed_int ++;
					Debug.Log ("typing!!!!!");
					Debug.Log ("numberOfKeysPressed_int: " + numberOfKeysPressed_int);
					debug_Txt_3.text = "numberOfKeysPressed_int: " + numberOfKeysPressed_int.ToString ();

				}
				else
				{
					YouHackedTheDoor ();
				}
			}
		}
	}


	private void YouHackedTheDoor () {
	
		// You won
		//youHackedTheShit_txt.text = "U DA MASTER";
		//canType_bool = false;
		StopAllCoroutines ();
		DefaultEverything ();

		Debug.Log ("Hacked");
		//StartHacking ();
		//  open the door, byatch!!!
		ogp_scr.DestroyFunction ();
		hacking_Canvas_GO.SetActive (false);
		iAmHacking = false;
	}


	private void YouLost () {
		
		
		DefaultEverything ();
		//canType_bool = false;
		StopAllCoroutines ();
		//timer_txt.text = "" + defaultTimerValue_int --;
		//  you should restart the hacking game, I think

		StartHacking ();
		Debug.Log ("Lost");
		//debug_Txt_4.text = "Lost";
	}



	IEnumerator TimerRanOut () {

		yield return new WaitForSeconds (defaultTimerValue_int);

		YouLost ();
	}


	IEnumerator DecreaseTimer () {

		while (true) 
		{
			timer_txt.text = "" + defaultTimerValue_int --;
			yield return new WaitForSeconds (1);

			if (defaultTimerValue_int < 0)
			{
				StopAllCoroutines ();
				defaultTimerValue_int = 20;
				StartHacking ();
			}
		}
	}


	IEnumerator ItsTimeToPressEnter () {

		while (true)
		{
			yield return new WaitForSeconds (Random.Range (2.0f, 3.5f));
			payAttention_txt.text = "Press ENTER!";
			timeToPressEnter_bool = true;
		}
	}
}
