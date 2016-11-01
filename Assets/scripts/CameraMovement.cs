using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float scrollSpeed;
    private float diagonalSpeed;

    // Use this for initialization.
    void Start() {
        scrollSpeed = 14;
        // The diagonal speed is so that the camera moves always with 
        // the same speed in one of the 8 directions.
        diagonalSpeed = Mathf.Sqrt(Mathf.Pow(scrollSpeed, 2) / 2);
    }

    // Update is called once per frame.
    void Update() {
        float t = Time.deltaTime;

        // Here all the 8 key combinations are checked.
        if (Input.GetKey("w") && Input.GetKey("a")) {
            this.transform.Translate(Vector3.forward * scrollSpeed * t, Space.World);
        }
        if (Input.GetKey("w") && Input.GetKey("d")) {
            this.transform.Translate(Vector3.right * scrollSpeed * t, Space.World);
        }
        if (Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("d")) {
            this.transform.Translate(Vector3.forward * diagonalSpeed * t, Space.World);
            this.transform.Translate(Vector3.right * diagonalSpeed * t, Space.World);
        }
        if (Input.GetKey("s") && Input.GetKey("a")) {
            this.transform.Translate(Vector3.left * scrollSpeed * t, Space.World);
        }
        if (Input.GetKey("s") && Input.GetKey("d")) {
            this.transform.Translate(Vector3.back * scrollSpeed * t, Space.World);
        }
        if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d")) {
            this.transform.Translate(Vector3.back * diagonalSpeed * t, Space.World);
            this.transform.Translate(Vector3.left * diagonalSpeed * t, Space.World);
        }
        if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s")) {
            this.transform.Translate(Vector3.forward * diagonalSpeed * t, Space.World);
            this.transform.Translate(Vector3.left * diagonalSpeed * t, Space.World);
        }
        if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s")) {
            this.transform.Translate(Vector3.back * diagonalSpeed * t, Space.World);
            this.transform.Translate(Vector3.right * diagonalSpeed * t, Space.World);
        }
    }
}
