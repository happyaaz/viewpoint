using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldenBunnyVictory : MonoBehaviour {

	public GameObject VictoryWindow;



	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "ByongYang" || col.gameObject.tag == "Russky" || col.gameObject.tag == "Gunnar")
		{
		VictoryWindow.SetActive (true);
		}
	}
}
