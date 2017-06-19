﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class ImagePad : MonoBehaviour {

	public string TextLeft;
	public string TextRight;
	public Sprite Image;

	private Transform canvases;
	private Transform playArea;

	void Start(){
		Image imageSmall = transform.GetChild (0).GetChild (1).GetComponent<Image> ();
		canvases = transform.GetChild (1);
		Image image = canvases.GetChild (0).GetChild (0).GetComponent<Image> ();
		Text left = canvases.GetChild (1).GetChild (0).GetComponent<Text> ();
		Text right = canvases.GetChild (2).GetChild (0).GetComponent<Text> ();
		playArea = VRTK_DeviceFinder.HeadsetTransform ();

		imageSmall.sprite = Image;
		image.sprite = Image;
		left.text = TextLeft;
		right.text = TextRight;
	}

	public void Toggle(){
		canvases.gameObject.SetActive (!canvases.gameObject.activeSelf);
		if(canvases.gameObject.activeSelf)
			canvases.LookAt (playArea);
	}
}
