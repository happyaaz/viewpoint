using UnityEngine;
using System.Collections;

public class SpawnPlayers : Photon.MonoBehaviour {

	public Transform firstPersViewChar_Spawn;
	public Transform secondPersViewChar_Spawn;
	public Transform thirdPersViewChar_Spawn;

	public GameObject firstPersViewChar_Pref;
	public GameObject secondPersViewChar_Pref;
	public GameObject thirdPersViewChar_Pref;

//	public GameObject gunnarsCameras;
	public GameObject gunnarsCamButton_Holder;
	public Options _options;

	//Spawning Text
	public GameObject spawning_Text;

	void Start ()
	{
		//Spawn the players at the start of the "Game" scene
		StartCoroutine (SpawnDelay());
//		SpawnAtStart ();

		//		if (Network.isServer)
		//		{
		//			//  server doesn't trigger OnPlayerConnected, manually spawn
		//			Network.Instantiate (player_1, spawnP1.position, Quaternion.identity, 0);
		//		}
	}

	//Spawns players through the network
	void SpawnAtStart () 
	{		
//		Debug.Log ("start spawning");
//		Debug.Log ("chosenCharacter: " + CharacterSelection_Ctrl.chosenCharacter);

		//Spawn the character prefab the player chose to play
		if(CharacterSelection_Ctrl.chosenCharacter == "firstPersViewChar")
		{
			GameObject pl = PhotonNetwork.Instantiate 
				(firstPersViewChar_Pref.name, firstPersViewChar_Spawn.position, Quaternion.identity, 0) as GameObject;
//			Debug.Log ("first perschar spawned");

//			gunnarsCameras.SetActive (false);
			gunnarsCamButton_Holder.SetActive (false);


			_options._CurPlayer="Russki";
			_options._currentPlayer_go = pl;
			_options._respawnPos = firstPersViewChar_Spawn.position;

			GameObject.Find ("StartUp_Camera01").SetActive (false);
			GameObject.Find ("Camera_Minimap").SetActive (false);


//			GetComponent<NetworkView> ().RPC ("net_DoSpawn", RPCMode.All, firstPersViewChar_Spawn.position);
//			Debug.Log ("run RPC for firstPersonViewChar");
		}
		else if(CharacterSelection_Ctrl.chosenCharacter == "secondPersViewChar")
		{
			GameObject pl = PhotonNetwork.Instantiate 
				(secondPersViewChar_Pref.name, secondPersViewChar_Spawn.position, Quaternion.identity, 0) as GameObject;
			_options._CurPlayer="Gunnar";
			_options._currentPlayer_go = pl.transform.FindChild ("GU").gameObject;
			_options._respawnPos = secondPersViewChar_Spawn.position;
			GameObject.Find ("UsedItem").SetActive (false);
			GameObject.Find ("Russki_Sight").SetActive (false);

//			GetComponent<NetworkView> ().RPC ("net_DoSpawn", RPCMode.All, secondPersViewChar_Spawn.position);
		}
		else if(CharacterSelection_Ctrl.chosenCharacter == "thirdPersViewChar")
		{
			GameObject pl = PhotonNetwork.Instantiate 
				(thirdPersViewChar_Pref.name, thirdPersViewChar_Spawn.position, Quaternion.identity, 0) as GameObject;
			_options._CurPlayer="BY";
//			gunnarsCameras.SetActive (false);
			gunnarsCamButton_Holder.SetActive (false);

			_options._currentPlayer_go = pl.transform.FindChild ("BY").gameObject;
			_options._respawnPos = thirdPersViewChar_Spawn.position;
			GameObject.Find ("StartUp_Camera01").SetActive (false);
			GameObject.Find ("Camera_Minimap").SetActive (false);
			GameObject.Find ("UsedItem").SetActive (false);
			GameObject.Find ("Russki_Sight").SetActive (false);


//			Debug.Log ("thirdPersViewChar_Spawn.position: " + thirdPersViewChar_Spawn.position);
//			GetComponent<NetworkView> ().RPC ("net_DoSpawn", RPCMode.All, thirdPersViewChar_Spawn.position);
		}
	}

	private IEnumerator SpawnDelay ()
	{
		spawning_Text.SetActive (true);
		yield return new WaitForSeconds(2f);
		SpawnAtStart();
		spawning_Text.SetActive (false);
	}


//	void OnPlayerConnected (NetworkPlayer _player) 
//	{		
//		//  when a player joins, tell them to spawn
//		if (Network.connections.Length == 1) {
//			GetComponent<NetworkView> ().RPC ("net_DoSpawn", _player, spawnP2.position);
//		} else if (Network.connections.Length == 2) {
//			GetComponent<NetworkView> ().RPC ("net_DoSpawn", _player, spawnP3.position);
//		}
//	}
//

//	//Spawns players through the network
//	[RPC]
//	void net_DoSpawn (Vector3 _position_vt3) 
//	{		
//
//		Debug.Log ("run net_DoSpawn");
//		// spawn the player based on the position passed
//
//		if (_position_vt3 == firstPersViewChar_Spawn.position) 
//		{
//			Network.Instantiate (firstPersViewChar_Pref, _position_vt3, Quaternion.identity, 0);
//			Debug.Log ("first person view char spawned");
//
//		}
//		else if (_position_vt3 == secondPersViewChar_Spawn.position) 
//		{
//			Network.Instantiate (secondPersViewChar_Pref, _position_vt3, Quaternion.identity, 0);
//		} 
//		else if (_position_vt3 == thirdPersViewChar_Spawn.position) 
//		{
//			Network.Instantiate (thirdPersViewChar_Pref, _position_vt3, Quaternion.identity, 0);
//		}
//	}
}
