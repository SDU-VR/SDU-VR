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

	void Awake(){
		if(Room == null)
			Debug.LogError("["+name+"] Room没有设置！");
	}

	void Start(){
		marker = GetComponent<DoorDestinationMarker> ();
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

}
