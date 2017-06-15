using System.Collections;
using UnityEngine;
using VRTK;

public class ModelInteractableObject : VRTK_InteractableObject {

	[Header("Model Interactable Object")]

	[Tooltip("移动面前的距离")]
	public float Distance = 3f;
	[Tooltip("移动面前的高度")]
	public float Height = 1f;
	[Tooltip("移动到目标位置的时间，以帧为单位")]
	public float SpeedFrame = 30.0f;
	[Tooltip("Pivot差值")]
	public float Pivot = 0;

	private enum State { IDLE, MOVING, PLACED }

	private State state;
	private Vector3 originPosition;
	private Vector3 pivot;
	private Vector3 scaleDelta;

	void Start(){
		state = State.IDLE;
		originPosition = transform.position;
		pivot = new Vector3 (0, Pivot, 0);
		scaleDelta = new Vector3 (0.5f, 0.5f, 0.5f) / SpeedFrame;
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

    private IEnumerator moveToPlayArea() {
        state = State.MOVING;
		Transform headset = VRTK_DeviceFinder.HeadsetTransform();
		Vector3 position = headset.position;
		position.y = Height;
		position += new Vector3(headset.forward.x, 0, headset.forward.z) * Distance;
		Vector3 dirDelta = (position - (transform.position + pivot)) / SpeedFrame;
		for(int i = 0; i < SpeedFrame; i++) {
			transform.Translate (dirDelta , Space.World);
			transform.localScale -= scaleDelta;
            yield return new WaitForEndOfFrame();
        }
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
		state = State.IDLE;
	}

}
