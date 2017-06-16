using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerpoint : MonoBehaviour {

	public int PptCount;

	private Image img;
	private Sprite[] ppts;

	private int _current;
	public int Current {
		set{
			if (ppts [value] == null) {
				ppts [value] = Resources.Load<Sprite> ("ppts/"+value);
			}
			_current = value;
			img.sprite = ppts [value];
		}
		get{
			return _current;
		}
	}

	void Start(){
		ppts = new Sprite[PptCount];
		img = GetComponent<Image> ();
		Current = 0;
	}

	public void Next(){
		if (Current == ppts.Length - 1) {
			Current = 0;
		} else {
			Current++;
		}
	}

}
