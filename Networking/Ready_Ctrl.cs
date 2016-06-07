using UnityEngine;
using System.Collections;

public class Ready_Ctrl : MonoBehaviour {


	CharacterSelection_Ctrl characterSelection_Ctrl;
	public GameObject highlightOnCharacter ;



	void Start ()
	{
		highlightOnCharacter.SetActive (false);

		characterSelection_Ctrl = GameObject.Find ("CharacterSelection_Ctrl").GetComponent<CharacterSelection_Ctrl>();
	}

	//Register the number of chosen characters.
	public void ReadyButtonClicked ()
	{
		characterSelection_Ctrl.AddToReadyPlayerCounter_Int ();
//		Debug.Log (CharacterSelection_Ctrl.readyPlayersCounter_Int);
	}

	//After you click a button with your character that character info gets set into a chosenCharacter variable and used to spawn your chosen character.
	public void ChooseCharacter ()
	{
		if (this.name.Contains ("FirstPersViewChar_Button")) 
		{
			characterSelection_Ctrl.ChooseFirstPersViewChar();

			CharacterSelection_Ctrl.chosenCharacter = "firstPersViewChar";
			highlightOnCharacter.SetActive(true);

//			characterSelection_Ctrl.secondPersViewChar_Button.interactable = false;
//			characterSelection_Ctrl.thirdPersViewChar_Button.interactable = false;

//			Debug.Log ("firstPersViewChar selected");
		}
		
		else if (this.name.Contains ("SecondPersViewChar_Button")) 
		{
			characterSelection_Ctrl.ChooseSecondPersViewChar();

			CharacterSelection_Ctrl.chosenCharacter = "secondPersViewChar";
//			characterSelection_Ctrl.secondPersViewChar_Button_Text.text = "Chosen";
			highlightOnCharacter.SetActive(true);
		
//			characterSelection_Ctrl.firstPersViewChar_Button.interactable = false;
//			characterSelection_Ctrl.thirdPersViewChar_Button.interactable = false;
		}
		
		else if (this.name.Contains ("ThirdPersViewChar_Button")) 
		{
			characterSelection_Ctrl.ChooseThirdPersViewChar ();

			highlightOnCharacter.SetActive(true);

			CharacterSelection_Ctrl.chosenCharacter = "thirdPersViewChar";
//			characterSelection_Ctrl.thirdPersViewChar_Button_Text.text = "Chosen";
//			characterSelection_Ctrl.firstPersViewChar_Button.interactable = false;
//			characterSelection_Ctrl.secondPersViewChar_Button.interactable = false;
		}
	}
}
