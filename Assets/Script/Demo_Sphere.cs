using UnityEngine;
using VRTK;

public class Demo_Sphere : VRTK_InteractableObject {

    public override void StartUsing(GameObject currentUsingObject) {
        base.StartUsing(currentUsingObject);
        Debug.Log("Using Object!");
    }

    public override void StopUsing(GameObject currentUsingObject) {
        base.StopUsing(currentUsingObject);
        Debug.Log("Stop Using Object!");
    }
}
