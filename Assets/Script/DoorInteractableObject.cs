using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(DoorDestinationMarker))]
public class DoorInteractableObject : VRTK_InteractableObject {

	[Header("Door Interactable Object")]

	[Tooltip("传送相关的房间")]
	public GameObject Room;
	[Tooltip("这个门是返回大厅的门吗")]
	public bool IsOut;

	private DoorDestinationMarker marker;
	private Transform controller;

	void Awake(){
		if(Room == null)
			Debug.LogError("["+name+"] Room没有设置！");
	}

	void Start(){
		marker = GetComponent<DoorDestinationMarker> ();
		controller = VRTK_DeviceFinder.DeviceTransform (VRTK_DeviceFinder.Devices.Right_Controller);
	}

	public override void StartUsing (GameObject currentUsingObject) {
		base.StartUsing (currentUsingObject);

		if (!IsOut) {
			Room.SetActive (true);
		}
		marker.Teleport (currentUsingObject);
		if (IsOut) {
			Room.SetActive (false);
		}
	}

	public override void StartTouching (GameObject currentTouchingObject) {
		base.StartTouching (currentTouchingObject);

		if ((controller.position - transform.position).magnitude < 0.5) {
			VRTK_ControllerActions action = currentTouchingObject.GetComponent<VRTK_ControllerActions> ();
			action.ToggleHighlightGrip (true, Color.yellow);
			action.SetControllerOpacity (0.5f);
		}
	}

	public override void StopTouching (GameObject previousTouchingObject) {
		base.StopTouching (previousTouchingObject);

		VRTK_ControllerActions action = previousTouchingObject.GetComponent<VRTK_ControllerActions> ();
		action.ToggleHighlightGrip (false);
		action.SetControllerOpacity (1f);
	}

}
