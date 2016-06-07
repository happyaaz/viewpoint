using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : Photon.MonoBehaviour {

//	private const string roomName = "RoomName";
	private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
	private RoomInfo[] roomsList;
//	public GameObject player;

	public static string name1;
	public string name2;

	//Debug UI
	public Text debug_Text;
//	private List<string> networkPlayers_List = new List<string>();


	//---RoomList variables---
	//Declare String for room lobby names
	private string roomName = "Room01";
//	//Variables for max amount of players 0-20 "you can change this inside gui"
//	private int maxPlayer = 3;
//	private string maxPlayerString = "1";
	//Declare list of Room array.
	private Room[] game;	
	//For GUI Scroll Bar *for large room list*
	private Vector2 scrollPosition;
	private bool hostingGame_Bool = false;
	private bool findingGame_Bool = false;
		
	// ---Connect To Master Server settings variables---
	public InputField iP_Inf;
	public Text old_IP_Txt;
	public string myMasterServerAddress_String;
	public int myPort_Int;
	public Text old_Port_Txt;
	public GameObject masterServerMenu_Holder_GO;
	private string myAppID_String = "44b062f5-46a7-4268-b80d-c1bdf4b43f03";
	private string myGameVersion_String = "v4.2";
	private bool tryingToConnectToMasterServer_Bool = false;

	public GameObject mainMenuHolder_GO;

	void Start () {

		old_IP_Txt.text = myMasterServerAddress_String;
		old_Port_Txt.text = myPort_Int.ToString ();

//		PhotonNetwork.ConnectUsingSettings("v4.2");
//		PhotonNetwork.automaticallySyncScene = true; 
	}
	
	void Update () {

		name2 = name1;

		if(PhotonNetwork.inRoom == true)
		{
			Debug.Log ("I'm in a room");
			debug_Text.text = "";
		}
		
		if(PhotonNetwork.connected == true)
		{
//			Debug.Log ("I have connected to server");
			debug_Text.text = "I have connected to server";
			
		}
		else if(PhotonNetwork.connected == false && tryingToConnectToMasterServer_Bool == true)
		{
			Debug.Log ("Connecting...");
			debug_Text.text = "Connecting...";			
		}

//		if (!PhotonNetwork.connected)
//		{
//			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
//		}
	}

	void OnGUI ()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		//		if (!PhotonNetwork.connected)
		//		{
		//			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		//		}

		if(hostingGame_Bool == true || findingGame_Bool == true)
		{
			//RoomsList
			//If I'm connected and inside lobby
			if (hostingGame_Bool == true || findingGame_Bool == true)
			{				
				//Display the lobby connection list and room creation.
				GUI.Box(new Rect(Screen.width/2.5f, Screen.height/3, 400, 550),"");
				GUILayout.BeginArea(new Rect(Screen.width/2.5f, Screen.height /3f, 400, 500));
				GUI.color = Color.red;
				GUILayout.Box("Lobby");
				GUI.color = Color.white;

				if(hostingGame_Bool == true)
				{
					GUILayout.Label("Room Name:");
					roomName = GUILayout.TextField(roomName); //For network room name ask and recieve
				
		//			GUILayout.Label("Max Amount of player 1-20:");
		//			maxPlayerString = GUILayout.TextField(maxPlayerString,2); //How man players with a max character set of 2 allowing no more then 2 digit player size.
		//			
		//			if (maxPlayerString != ""){ // if there is a character of max players
		//				
		//				maxPlayer = int.Parse(maxPlayerString); // parse the max player text field into a string.
		//				
		//				if (maxPlayer > 20) maxPlayer = 20; // if I enter above 20 reset the max to 20 .
		//				if (maxPlayer == 0) maxPlayer =1; // if i'm below 1 reset min of 1
		//				
		//			}
		//			else
		//			{
		//				maxPlayer =1; // 1
		//			}
					
					if (GUILayout.Button("Create Room") ){
						
						if (roomName != "") // if the room name has a name and max players are larger then 0
						{
							PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 3, isOpen = true, isVisible = true}, lobbyName); // then create a photon room visible , and open with the maxplayers provide by user.
						}
					}
				}

				if(hostingGame_Bool == false)
				{
					GUILayout.Space(20);
					GUI.color = Color.red;
					GUILayout.Box("Game Rooms");
					GUI.color = Color.white;
					GUILayout.Space(20);
					
					scrollPosition = GUILayout.BeginScrollView(scrollPosition, false ,true, GUILayout.Width(400), GUILayout.Height(300));
					
					foreach ( RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
					{
						
						GUI.color = Color.green;
						GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20
						GUI.color = Color.white;
						
						if (GUILayout.Button("Join Room") ){
							
							PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.
						}
					}					
					GUILayout.EndScrollView();
				}
				GUILayout.EndArea();				
			}
		}

	}

/*	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 3, isOpen = true, isVisible = true}, lobbyName);
			}
			
			// Join Room
			if (roomsList != null) 
			{
				for (int i = 0; i < roomsList.Length; i++) 
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name)) 
					{
						PhotonNetwork.JoinRoom(roomsList[i].name);
					}
				}
			}
		}
	}
*/

	public void ConnectUsingPresetSettings ()
	{
		PhotonNetwork.ConnectUsingSettings("v4.2");
		PhotonNetwork.automaticallySyncScene = true;
	}

	public void ConnectToTheMasterServerAndAutoSyncScene ()
	{
		old_IP_Txt.text = iP_Inf.text;
		myMasterServerAddress_String = old_IP_Txt.text;
		
		PhotonNetwork.ConnectToMaster (myMasterServerAddress_String, myPort_Int, myAppID_String, myGameVersion_String);
		PhotonNetwork.automaticallySyncScene = true; 
		
		tryingToConnectToMasterServer_Bool = true;
	}

	public void ClickToHostRoom ()
	{
		hostingGame_Bool = true;

		// Create Room
//		PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 3, isOpen = true, isVisible = true}, lobbyName);

	}

	public void ClickToFindRoom ()
	{
		findingGame_Bool = true;

//		Debug.Log ("room name: " + roomsList[0].name);
//		Debug.Log ("roomsList length: " + roomsList.Length);


//		// Join Room
//		if (roomsList != null) 
//		{
//			PhotonNetwork.JoinRoom(roomsList[0].name);
//		}
	}
	
	void OnConnectedToMaster() 
	{
//		Debug.Log ("Joined Master Server");
		PhotonNetwork.JoinLobby(lobbyName);

		masterServerMenu_Holder_GO.SetActive (false);
		mainMenuHolder_GO.SetActive (true);
	}
	
	void OnReceivedRoomListUpdate()
	{
//		Debug.Log ("Room was created");
		roomsList = PhotonNetwork.GetRoomList();

		if(roomsList.Length == 0)
		{
			debug_Text.text = "No rooms available";
		}
		else
		{
			debug_Text.text = "Hosting room name: " + roomsList[0].name + "\n" +  "Hosting roomsList length: " + roomsList.Length + 
				"\n" + "Number of Players in Room: " + PhotonNetwork.playerList.Length.ToString ();

//			foreach (PhotonPlayer player in PhotonNetwork.otherPlayers)
//			{
//				playerList.Append(player.name + "\n");
//			}
//			
//			m_Text.text = playerList.ToString();
		}




//		Debug.Log ("hosting room name: " + roomsList[0].name);
//		Debug.Log ("hosting roomsList length: " + roomsList.Length);
	}
	
	void OnJoinedLobby () {
//		Debug.Log ("Joined Lobby");
	}
	
	void OnJoinedRoom ()
	{
//		Debug.Log("Connected to Room");
		
		PhotonNetwork.LoadLevel ("CharacterSelection");
		/*
		PhotonNetwork.Instantiate(player.name, Vector3.left * Random.Range (1, 10), Quaternion.identity, 0);
		if (PhotonNetwork.countOfPlayers == 2) 
		{
			Debug.Log ("Good shiet");
		}
		
		Debug.Log (PhotonNetwork.player.ID);
		name1 = PhotonNetwork.player.ID.ToString ();
		*/
	}
}
