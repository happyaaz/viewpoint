using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MinimapFunctionality : MonoBehaviour {


	public Image minimap_img;
	public Sprite floor0_spr;
	public Sprite floor1_spr;
	public Sprite floor2_spr;

	public static MinimapFunctionality mf_scr;
	
	
	public void BasementMap () {
		
		minimap_img.sprite = floor0_spr;
	}
	
	
	public void FirstFloorMap () {
		
		minimap_img.sprite = floor1_spr;
	}
	
	
	public void SecondFloorFloorMap () {
		
		minimap_img.sprite = floor1_spr;
	}


	void Awake () {

		mf_scr = this;
	}
}
