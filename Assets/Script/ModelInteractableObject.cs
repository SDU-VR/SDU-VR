using System.Collections;
using UnityEngine;
using VRTK;

public class ModelInteractableObject : VRTK_InteractableObject {

    private enum State { IDLE, MOVING, PLACED }
    private State state = State.IDLE;

    public override void StartUsing(GameObject currentUsingObject) {
        base.StartUsing(currentUsingObject);
        switch (state) {
            case State.IDLE: {
                    StartCoroutine(moveToPlayArea());
                }
                break;
            case State.MOVING:
                break;
            case State.PLACED:
                break;
        }
    }

    private IEnumerator moveToPlayArea() {
        state = State.MOVING;
        Vector3 playArea = VRTK_DeviceFinder.PlayAreaTransform().position;
        Vector3 dir = playArea - transform.position;
        Vector3 horizon = new Vector3(dir.x, 0, dir.y);
        for(int i = 0; i < 100; i++) {
            transform.Translate(horizon * 0.01f);
            yield return new WaitForEndOfFrame();
        }
        Vector3 vertical = new Vector3(0, dir.y, 0);
        for (int i = 0; i < 40; i++) {
            transform.Translate(vertical * 0.025f);
            yield return new WaitForEndOfFrame();
        }
        state = State.PLACED;
    }

}
