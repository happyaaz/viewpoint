using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelection_Ctrl : Photon.MonoBehaviour {

	public Button firstPersViewChar_Button;
	public Button secondPersViewChar_Button;
	public Button thirdPersViewChar_Button;

	public Text firstPersViewChar_Button_Text;
	public Text secondPersViewChar_Button_Text;
	public Text thirdPersViewChar_Button_Text;

	public static int readyPlayersCounter_Int = 0;

	public static string chosenCharacter = "";

	public bool huy = false;

	public GameObject chosenCharacter_GO;
	public GameObject rus_GO;
	public GameObject by_GO;
	public GameObject gun_GO;
	public GameObject questionMark_GO;

	private bool testing_Bool = true;
//	private PhotonView photonView;


//	public NetworkPlayer player1;
//	public NetworkPlayer player2;
//	public NetworkPlayer player3;

	//Timer
	public GameObject countDown_Holder_GO;
	public Text countDown_Text;
	private bool startCountDown_Bool = false;
	private float countDownTime_F = 3f;

	//Debug UI
	public Text debugText;
	public Text debugText_2;
	private RoomInfo[] roomsList;


	void Start ()
	{
		chosenCharacter_GO = questionMark_GO;

		//For TESTING
		//Normally you need 3 players to start the game. Add here the amount of players you don't need.
		if(testing_Bool == true)
		{
			readyPlayersCounter_Int = 1;
		}
//		photonView = GetComponent<PhotonView>();
	}

	void Update ()
	{
		//Texts to show us some info about the room and other things
		if(PhotonNetwork.room.name == null || PhotonNetwork.playerList.Length == 0)
		{
			debugText_2.text = "Room name = null or playerList = 0";
		}

		debugText.text = "Hosting room name: " + PhotonNetwork.room.name + 
			"\n" + "Number of Players in Room: " + PhotonNetwork.playerList.Length.ToString ();

		//When set to true we start te countDown
		if(startCountDown_Bool == true)
		{
			CountDownToStartGame ();
		}
	}

	void OnGUI ()
	{		
		//Show the status of the connected player
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	//Counter that counts down until we spawn
	void CountDownToStartGame ()
	{
		countDownTime_F -= Time.deltaTime;

		if(countDownTime_F <= 0f)
		{
			countDownTime_F = 0f;

		}
		countDown_Text.text = countDownTime_F.ToString ("0");
	}

	//Run the counter for characterbuttons clicked for all network players.
	public void AddToReadyPlayerCounter_Int ()
	{
		GetComponent<PhotonView>().RPC("RPC_AddToReadyPlayerCounter_Int", PhotonTargets.AllBufferedViaServer);
	}

	//Adds one int to the counter for how many times the buttons have been clicked. If it reaches 3 then load the game scene.
	[PunRPC]
	private void RPC_AddToReadyPlayerCounter_Int ()
	{
		readyPlayersCounter_Int ++;
		if(readyPlayersCounter_Int >= 3)
		{
			countDown_Holder_GO.SetActive (true);
			startCountDown_Bool = true;
			StartCoroutine (StartGameDelay());
//			Application.LoadLevel( "MainScene" );
		}
	}

	//Character selection functions and their RPC twins. The character selection functions that do not need to be RPCd are in "Ready_Ctrl"
	public void ChooseFirstPersViewChar ()
	{
		photonView.RPC("RPC_ChooseFirstPersViewChar", PhotonTargets.OthersBuffered);
		firstPersViewChar_Button_Text.text = "Chosen";
//		huy = true;
		if(secondPersViewChar_Button_Text.text != "Not available")
		{
			secondPersViewChar_Button.interactable = true;
		}
		if(secondPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetSecondPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			secondPersViewChar_Button_Text.text = "Gunnar";
		}
		firstPersViewChar_Button.interactable = false;
		if(thirdPersViewChar_Button_Text.text != "Not available")
		{
			thirdPersViewChar_Button.interactable = true;
		}
		if(thirdPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetThirdPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			thirdPersViewChar_Button_Text.text = "Byong Yang";
		}

		chosenCharacter_GO = rus_GO;
	}

	[PunRPC]
	private void RPC_ChooseFirstPersViewChar ()
	{
		firstPersViewChar_Button_Text.text = "Not available";
//		if(secondPersViewChar_Button_Text.text == "Chosen")
//		{
//			secondPersViewChar_Button_Text.text = "Gunnar";
//		}
//		if(thirdPersViewChar_Button_Text.text == "Chosen")
//		{
//			thirdPersViewChar_Button_Text.text = "Byong Yang";
//		}
//		secondPersViewChar_Button_Text.text = "Gunnar";
//		thirdPersViewChar_Button_Text.text = "Byong Yang";

		firstPersViewChar_Button.interactable = false;
	}

	public void ChooseSecondPersViewChar ()
	{
		photonView.RPC("RPC_ChooseSecondPersViewChar", PhotonTargets.OthersBuffered);
		secondPersViewChar_Button_Text.text = "Chosen";
//		huy = true;
		if(firstPersViewChar_Button_Text.text != "Not available")
		{
			firstPersViewChar_Button.interactable = true;
		}
		if(firstPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetFirstPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			firstPersViewChar_Button_Text.text = "Russky";
		}
		secondPersViewChar_Button.interactable = false;
		if(thirdPersViewChar_Button_Text.text != "Not available")
		{
			thirdPersViewChar_Button.interactable = true;
		}
		if(thirdPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetThirdPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			thirdPersViewChar_Button_Text.text = "Byong Yang";
		}
		chosenCharacter_GO = gun_GO;
	}

	[PunRPC]
	private void RPC_ChooseSecondPersViewChar ()
	{
		secondPersViewChar_Button_Text.text = "Not available";
//		if(firstPersViewChar_Button_Text.text == "Chosen")
//		{
//			firstPersViewChar_Button_Text.text = "Russky";
//		}
//		if(thirdPersViewChar_Button_Text.text == "Chosene")
//		{
//			thirdPersViewChar_Button_Text.text = "Byong Yang";
//		}
//		firstPersViewChar_Button_Text.text = "Russky";
//		thirdPersViewChar_Button_Text.text = "Byong Yang";

		secondPersViewChar_Button.interactable = false;
	}

	public void ChooseThirdPersViewChar ()
	{
		photonView.RPC("RPC_ChooseThirdPersViewChar", PhotonTargets.OthersBuffered);
		thirdPersViewChar_Button_Text.text = "Chosen";
//		huy = true;
		if(firstPersViewChar_Button_Text.text != "Not available")
		{
			firstPersViewChar_Button.interactable = true;
		}
		if(firstPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetFirstPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			firstPersViewChar_Button_Text.text = "Russky";
		}
		thirdPersViewChar_Button.interactable = false;
		if(secondPersViewChar_Button_Text.text != "Not available")
		{
			secondPersViewChar_Button.interactable = true;
		}
		if(secondPersViewChar_Button_Text.text == "Chosen")
		{
			photonView.RPC("RPC_ResetSecondPersViewChar_Button_Text", PhotonTargets.AllBufferedViaServer);
//			secondPersViewChar_Button_Text.text = "Gunnar";
		}
		chosenCharacter_GO = by_GO;
	}

	[PunRPC]
	private void RPC_ChooseThirdPersViewChar ()
	{
		thirdPersViewChar_Button_Text.text = "Not available";
//		if(firstPersViewChar_Button_Text.text == "Chosen")
//		{
//			firstPersViewChar_Button_Text.text = "Russky";
//		}
//		if(secondPersViewChar_Button_Text.text == "Chosen")
//		{
//			secondPersViewChar_Button_Text.text = "Gunnar";
//		}
//		firstPersViewChar_Button_Text.text = "Russky";
//		secondPersViewChar_Button_Text.text = "Gunnar";

		thirdPersViewChar_Button.interactable = false;
	}

	[PunRPC]
	private void RPC_ResetFirstPersViewChar_Button_Text ()
	{
		firstPersViewChar_Button_Text.text = "Russky";
		firstPersViewChar_Button.interactable = true;
		readyPlayersCounter_Int --;
	}

	[PunRPC]
	private void RPC_ResetSecondPersViewChar_Button_Text ()
	{
		secondPersViewChar_Button_Text.text = "Gunnar";
		secondPersViewChar_Button.interactable = true;
		readyPlayersCounter_Int --;
	}

	[PunRPC]
	private void RPC_ResetThirdPersViewChar_Button_Text ()
	{
		thirdPersViewChar_Button_Text.text = "Byong Yang";
		thirdPersViewChar_Button.interactable = true;
		readyPlayersCounter_Int --;
	}

	private IEnumerator StartGameDelay ()
	{
		yield return new WaitForSeconds(3f);
		PhotonNetwork.LoadLevel( "MainScene" );
	}

	//Just an extra class that does nothing but needs to be here to prevent errors
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

	}

//	void OnPlayerConnected (NetworkPlayer _player) 
//	{
//		//  when a player joins, tell them to spawn
//		if (Network.connections.Length == 1) 
//		{
//			player2 = _player;
//		}
//		if (Network.connections.Length == 2) 
//		{
//			player3 = _player;
//		}
//	}
}
