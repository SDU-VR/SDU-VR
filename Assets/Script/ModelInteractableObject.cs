﻿using System.Collections;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class ModelInteractableObject : VRTK_InteractableObject {

	[Header("Model Interactable Object")]

	[Tooltip("移动面前的距离")]
	public float Distance = 3f;
	[Tooltip("移动面前的高度")]
	public float Height = 1f;
	[Tooltip("移动面前的Scale")]
	public float Scale = 0.5f;
	[Tooltip("移动到目标位置的时间，以帧为单位")]
	public float SpeedFrame = 30.0f;
	[Tooltip("Pivot差值")]
	public float Pivot = 0;
	[Tooltip("FloorInfoPanel用于显示楼层信息")]
	public ModelInfoPanel FloorInfoPanel;
	[Tooltip("FloorInfoPanel中显示的信息")]
	public string Info;

	private enum State { IDLE, MOVING, PLACED }

	private State state;
	private Vector3 originPosition;
	private Vector3 pivot;
	private Vector3 scaleDelta;
	private Transform headset;
	//add
	private GameObject textPanel;
	//private Quaternion targetRotaion;
	//add end

	private static ModelInteractableObject[] _models;
	private static ModelInteractableObject[] models {
		get {
			if (_models == null) {
				_models = FindObjectsOfType<ModelInteractableObject> ();
			}
			return _models;
		}
	}

	void Awake(){
		if(FloorInfoPanel == null)
			Debug.LogError("["+name+"] FloorInfoPanel没有设置！");
	}

	void Start(){
		state = State.IDLE;
		originPosition = transform.position;
		pivot = new Vector3 (0, Pivot, 0);
		scaleDelta = new Vector3 (Scale, Scale, Scale) / SpeedFrame;
		headset = VRTK_DeviceFinder.HeadsetTransform();
		//add
		//targetRotaion = GameObject.Find("ceiling1.4").transform.rotation;
		textPanel = this.transform.GetChild (0).FindChild("Text").gameObject;
		//add end
	}

    public override void StartUsing(GameObject currentUsingObject) {
        base.StartUsing(currentUsingObject);
        switch (state) {
            case State.IDLE: {
                    StartCoroutine(moveToPlayArea());
                }
                break;
            case State.MOVING:
                break;
		case State.PLACED: {
					StartCoroutine(moveBack());
				}
                break;
        }
    }

	public override void OnInteractableObjectTouched (InteractableObjectEventArgs e) {
		if (state == State.PLACED || state == State.MOVING) {
			ToggleHighlight (false);
		}
		base.OnInteractableObjectTouched (e);
	}

	public override void StartTouching (GameObject currentTouchingObject) {
		base.StartTouching (currentTouchingObject);
		if (state == State.IDLE) {
			FloorInfoPanel.gameObject.SetActive (true);
			FloorInfoPanel.SetText (Info);
		}

		VRTK_ControllerActions action = currentTouchingObject.GetComponent<VRTK_ControllerActions> ();
		action.ToggleHighlightTrigger (true, Color.yellow);
		action.SetControllerOpacity (0.5f);
	}

	public override void StopTouching (GameObject previousTouchingObject) {
		base.StopTouching (previousTouchingObject);
		FloorInfoPanel.gameObject.SetActive (false);

		VRTK_ControllerActions action = previousTouchingObject.GetComponent<VRTK_ControllerActions> ();
		action.ToggleHighlightTrigger (false);
		action.SetControllerOpacity (1f);
	}

    private IEnumerator moveToPlayArea() {
		foreach (ModelInteractableObject o in models) {
			if (o.state == State.PLACED) {
				StartCoroutine (o.moveBack ());
			} else if (o.state == State.MOVING) {
				yield return new WaitWhile(() => o.state == State.MOVING);
			}
		}
		state = State.MOVING;
		Vector3 position = headset.position;
		position.y = Height;
		position += new Vector3(headset.forward.x, 0, headset.forward.z) * Distance;
		Vector3 dirDelta = (position - (transform.position + pivot)) / SpeedFrame;
		for(int i = 0; i < SpeedFrame; i++) {
			transform.Translate (dirDelta , Space.World);
			transform.localScale -= scaleDelta;
            yield return new WaitForEndOfFrame();
        }
		//add
		//GetComponentInChildren<Animator>().enabled = false;
		//this.transform.GetChild (0).rotation = Quaternion.identity;
		textPanel.SetActive(!textPanel.activeSelf);
		//add end
		state = State.PLACED;
    }

	private IEnumerator moveBack() {
		state = State.MOVING;
		Vector3 dirDelta = (originPosition - transform.position) / SpeedFrame;
		for(int i = 0; i < SpeedFrame; i++) {
			transform.Translate (dirDelta , Space.World);
			transform.localScale += scaleDelta;
			yield return new WaitForEndOfFrame();
		}
		//add
		//GetComponentInChildren<Animator>().enabled = true;
		//this.transform.GetChild (0).rotation = targetRotaion;
		textPanel.SetActive(!textPanel.activeSelf);
		//add end
		state = State.IDLE;
	}

}
