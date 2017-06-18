using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DoorDestinationMarker : VRTK_DestinationMarker {
	
	private Transform destination;

	void Awake(){
		destination = transform.GetChild (0);
		if(destination == null)
			Debug.LogError("["+name+"] 没有Destination子物体！");
	}

	public void Teleport(GameObject controller){
		var distance = Vector3.Distance(transform.position, destination.position);
		var controllerIndex = VRTK_DeviceFinder.GetControllerIndex(controller.gameObject);
		OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerIndex));
	}

}
