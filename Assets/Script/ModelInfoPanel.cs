using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelInfoPanel : MonoBehaviour {

	private Text text;

	void Awake(){
		text = GetComponentInChildren<Text> ();
	}

	public void SetText(string text){
		this.text.text = text;
	}
}
