using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class PuzzleMaster : Photon.MonoBehaviour {

	public static PuzzleMaster pm_scr;



	[System.Serializable]
	public class SpawningKeys {

		public GameObject key_1_go;
		public GameObject key_2_go;
		public GameObject key_3_go;

		public Transform key_1_sp_tr;
		public Transform key_2_sp_tr;
		public Transform key_3_sp_tr;

		public AudioSource MySource;
		public AudioClip KeySpawnSound;


		public void SpawnKey1 () {

			MySource.PlayOneShot(KeySpawnSound);
			Instantiate (key_1_go, key_1_sp_tr.position, key_1_go.transform.rotation);
		}
		
		public void SpawnKey2 () {
			MySource.PlayOneShot(KeySpawnSound);
			Instantiate (key_2_go, key_2_sp_tr.position, key_2_go.transform.rotation);
		}
		
		public void SpawnKey3 () {
			MySource.PlayOneShot(KeySpawnSound);
			Instantiate (key_3_go, key_3_sp_tr.position, key_3_go.transform.rotation);
		}
	}


	[System.Serializable]
	public class InsideElevators {

		public GameObject lift_go;
		public GameObject buttonLiftUp_go;
		public GameObject buttongLiftDown_go;
		public BY_Lift byLift_scr;



		public void EnableButtons () {
			
			buttonLiftUp_go.SetActive (true);
			buttongLiftDown_go.SetActive (true);
		}
		
		
		public void DisableButtons () {
			
			buttonLiftUp_go.SetActive (false);
			buttongLiftDown_go.SetActive (false);
		}
		
		
		public void LiftUp () {
			
			//			Vector3 endPos = lift_go.transform.position + Vector3.up * 4;
			//			
			//			byLift_scr.dest_vt3 = endPos;
			//			byLift_scr.timeToMove_bool = true;
			
			byLift_scr.Lift_Platform_Up ();
			Initialize ();
		}
		
		
		public void LiftDown () {
			
			//			Vector3 endPos = lift_go.transform.position - Vector3.up * 4;
			//
			//			byLift_scr.dest_vt3 = endPos;
			//			byLift_scr.timeToMove_bool = true;
			
			byLift_scr.Lift_Platform_Down ();
			Initialize ();
		}
		
		
		public void Initialize () {
			
			DisableButtons ();
		}
	}



	[System.Serializable]
	public class Puzzle_Entrance_ru {

		//  game objects that should take part

		public GameObject basementDoor2_go;
		public GameObject gunnar_go;
		public Transform spawnGunnarThere_tr;


		public void OpenDoor () {

			MinimapFunctionality.mf_scr.SecondFloorFloorMap ();
			Debug.Log ("kdkfkfkfjdh");
//			Destroy (basementDoor2_go);
//			gunnar_go.transform.position = spawnGunnarThere_tr.position;

		}
	}


	[System.Serializable]
	public class Puzzle_Entrance_by {
		
		//  game objects that should take part

		public GameObject lift_go;
		public GameObject buttonLiftUp_go;
		public GameObject buttongLiftDown_go;
		public BY_Lift byLift_scr;


		public void EnableButtons () {
	
			buttonLiftUp_go.SetActive (true);
			buttongLiftDown_go.SetActive (true);
		}


		public void DisableButtons () {

			buttonLiftUp_go.SetActive (false);
			buttongLiftDown_go.SetActive (false);
		}


		public void LiftUp () {

//			Vector3 endPos = lift_go.transform.position + Vector3.up * 4;
//			
//			byLift_scr.dest_vt3 = endPos;
//			byLift_scr.timeToMove_bool = true;

			byLift_scr.Lift_Platform_Up ();
			MinimapFunctionality.mf_scr.SecondFloorFloorMap ();
			Initialize ();
		}


		public void LiftDown () {
	
//			Vector3 endPos = lift_go.transform.position - Vector3.up * 4;
//
//			byLift_scr.dest_vt3 = endPos;
//			byLift_scr.timeToMove_bool = true;

			byLift_scr.Lift_Platform_Down ();
			MinimapFunctionality.mf_scr.FirstFloorMap ();
			Initialize ();
		}


		public void Initialize () {

			DisableButtons ();
		}
	}


	[System.Serializable]
	public class Puzzle_Room3 {

		private int numberOfMatchedKeypads_int = 0;

		public GameObject glassDoor_go;


		public void IncreaseNumberOfMatchedKeypads () {

			numberOfMatchedKeypads_int ++;
			//  if four - do smth
			if (numberOfMatchedKeypads_int == 4)
			{
				OpenGlassDoor ();
			}
		}
		private void OpenGlassDoor () {
		
			try
			{
				glassDoor_go.GetComponent<Animation>().Play ();
			}
			catch (System.Exception e) 
			{
				Destroy (glassDoor_go);
			}

			//  spawn the key

			pm_scr.spawningKeys_class.SpawnKey1 ();
		}


		public void DecreaseNumberOfMatchedKeypads () {
			
			numberOfMatchedKeypads_int --;
		}


		public int GetNumberOfMatchedKeypads () {
	
			return numberOfMatchedKeypads_int;
		}
	}


	[System.Serializable]
	public class Puzzle_Room5 {
		
		//  game objects that should take part
		
		public List <int> combination_list = new List <int> ();
		public List <int> numbersTyped_list = new List <int> ();
		public int numberOfMatches_int = 0;
		public TextMesh currentCombination;
		private bool passwordIsReady_bool = false;

		public GameObject door_go;

		public List <GameObject> numbersText_list = new List <GameObject> ();
		
		public void AddNumber (int _index_int, int _numberToAdd_int) 
		{
//			Debug.Log ("AddNumber run");
//			Debug.Log ("_numberToAdd_int: " + _numberToAdd_int);
//			combination_list [_index_int] = _numberToAdd_int;
//			Debug.Log ("combination_list[0]: " + combination_list[0]);
		}
		
		
		public void CheckNumberInCombination (int _number_int) {
			
			if (passwordIsReady_bool == false) 
			{
				return;
			}
			//			Debug.Log (_number_int);
			
			if (!numbersTyped_list.Contains (_number_int))
			{
				currentCombination.text += _number_int.ToString ();
				numbersTyped_list.Add (_number_int);
				numberOfMatches_int ++;
				if (numberOfMatches_int == 3)
				{
					CheckAllNumbers ();
				}
			}
			else
			{
				Failed ();
			}
			
		}
		
		
		private void CheckAllNumbers () {
			
			int numbersMatched_int = 0;
			Debug.Log ("Ahahah");
			
			foreach (int number_int in numbersTyped_list)
			{
				if (combination_list.Contains (number_int))
				{
					numbersMatched_int ++;
					Debug.Log ("ASDASDASDASDkilsajdfa");
				}
				else
				{
					Debug.Log (numbersMatched_int + ", " + combination_list.Contains (number_int));
				}
			}
			
			if (numbersMatched_int == 3)
				PuzzleSolved ();
			else
				Failed ();
		}
		
		
		private void Failed () {
			
			numberOfMatches_int = 0;
			currentCombination.text = string.Empty;
			numbersTyped_list.Clear ();
		}
		
		
		private void PuzzleSolved () {
			
			//  do smth

			try
			{
				door_go.GetComponent<Animation>().Play ();
			}
			catch (System.Exception e) 
			{
				Destroy (door_go);
			}

			//  spawn the key
			
			pm_scr.spawningKeys_class.SpawnKey2 ();

			Debug.Log ("FUCK YEAH");
		}
		
		
		public void PasswordIsReady () {
			
			passwordIsReady_bool = true;
			//			Debug.Log ("DONE");
			
			foreach (GameObject go in numbersText_list) 
			{
				go.SetActive (true);
			}
		}


		public void Initialize () {
			
			foreach (GameObject go in numbersText_list) 
			{
				go.SetActive (false);
			}
		}

	}
	
	[System.Serializable]
	public class Puzzle_Room6 {
		
		public int numberOfHitButtons_int = 0;
		public GameObject keypad_go;
		
		public void ButtonHit (GameObject _button_go) {
			
			numberOfHitButtons_int ++;
			Destroy (_button_go);
			
			if (numberOfHitButtons_int == 2)
				EnableKeypad ();
		}
		
		
		public void Initialize () {
			
			keypad_go.SetActive (false);
		}
		
		
		private void EnableKeypad () {
			


			//  spawn the key
			
			pm_scr.spawningKeys_class.SpawnKey3 ();
		}
	}
	
	
	
	public InsideElevators insideElevators_class = new InsideElevators ();


	public Puzzle_Entrance_ru puzzle_Entrance_ru_class = new Puzzle_Entrance_ru ();
	public Puzzle_Entrance_by puzzle_Entrance_by_class = new Puzzle_Entrance_by ();
	public Puzzle_Room3 puzzle_Room3_class = new Puzzle_Room3 ();
	
	public Puzzle_Room5 puzzle_Room5_class = new Puzzle_Room5 ();
	public Puzzle_Room6 puzzle_Room6_class = new Puzzle_Room6 ();
	public SpawningKeys spawningKeys_class = new SpawningKeys ();


	void Awake () {

		pm_scr = this;
		puzzle_Entrance_by_class.Initialize ();
		insideElevators_class.Initialize ();

		puzzle_Room5_class.Initialize ();
		//puzzle_Room6_class.Initialize ();


		/*
		pm_scr.spawningKeys_class.SpawnKey1 ();
		pm_scr.spawningKeys_class.SpawnKey2 ();
		pm_scr.spawningKeys_class.SpawnKey3 ();
		*/

	}
}
