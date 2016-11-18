using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

    public Transform lookAt;
    public bool objective;
    public bool start;

    private Transform initialPos;
    private Vector3 offset;
    private float smoothSpeed;

    void Start() {
        objective = false;
        start = false;

        initialPos = new GameObject().transform; // Create gameobject with transform.
        initialPos.position = transform.position; // Copy the standard position.
        offset = new Vector3(0, 0, 0);
        smoothSpeed = 0.025f;
    }

    void LateUpdate() {
        if (objective) {
            Vector3 desiredPos = lookAt.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        }
        if (start) {
            Vector3 desiredPos = initialPos.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        }
    }

}
