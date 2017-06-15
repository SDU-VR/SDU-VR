using UnityEngine;
using VRTK;

public class ModelInteractableObject : VRTK_InteractableObject {

    private enum State { IDLE, MOVING, PLACED }

    public override void StartUsing(GameObject currentUsingObject) {
        base.StartUsing(currentUsingObject);
        Debug.Log("using");
    }

}
