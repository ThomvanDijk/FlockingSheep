using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DogBehaviour : MonoBehaviour {

    public GameObject targetPrefab;
    public GameObject raycastLayer;
    public float scaleSpeed = 0;
    public float fadeSpeed = 0;
    public float maxTargetScale = 0;

    public Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;
    private Vector2 direction;

    private Vector2 target;
    private List<GameObject> targetList;
    private float maxSpeed;
    private float maxForce;
    private float rotation;
    private float arriveRange;

    // Use this for initialization
    void Start () {
        position = new Vector2(this.transform.position.x, this.transform.position.z);
        velocity = new Vector2(0, 0);
        acceleration = new Vector2(0, 0);
        direction = new Vector2(0, 0);

        target = new Vector2(this.transform.position.x, this.transform.position.z);
        targetList = new List<GameObject>();
        maxSpeed = 0.14f;
        maxForce = 0.011f;
        rotation = 0;
        arriveRange = 1;
    }
	
	// Update is called once per frame
	void Update () {

        setTarget();
        clickFeedback(); // Shows feedback when new target is set.

        arrive(); // Where the dog arrives.

        velocity = velocity.add(acceleration);
        velocity = velocity.limit(maxSpeed);
        position = position.add(velocity);
        acceleration = acceleration.multS(0);

        if (velocity.mag() > 0) {
            // Rotate dog in it's direction.
            rotation = velocity.getAngle();
            rotation *= -1;
            rotation += 90;
            this.transform.rotation = Quaternion.Euler(-89.96101f, rotation, 0);
        }

        // Translate Vector2 position to dog.
        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    private void applyForce(Vector2 force) {
        acceleration = acceleration.add(force);
        
    }

    private void arrive() {
        Vector2 desired = target.sub(position);

        float distance = desired.mag(); // The distance is the magnitude of the vector pointing from location to target.
        desired = desired.normalize();

        if (distance < arriveRange) { // If we are closer than 100...
            float m = map(distance, 0, 100, 0, maxSpeed); // ...set the magnitude according to how close we are.
            desired = desired.multS(m);
        }
        else {
            desired = desired.multS(maxSpeed); // Otherwise, proceed at maximum speed.
        }

        Vector2 steer = desired.sub(velocity); // The usual steering = desired - velocity
        steer = steer.limit(maxForce);
        applyForce(steer);
    }

    // map(973, 0, 1023, 0, 255); // returns: 242
    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private void setTarget() {
        // Save the place where the mouse clicked right.
        if (Input.GetMouseButtonDown(1)) {

            RaycastHit hit; // Position where mouse hits the raycast.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (raycastLayer.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {

                // Greate target point for visuals.
                GameObject targetIndicator = (GameObject)Instantiate(targetPrefab, hit.point, targetPrefab.transform.rotation);
                targetList.Add(targetIndicator);

                target = new Vector2(hit.point.x, hit.point.z);
            }
        }
    }

    private void clickFeedback() {
        int count = targetList.Count;
        // Reverse iteration trough list to remove items.
        for (int i = count - 1; i >= 0; i--) {

            // Object scaling over time.
            float newScale = targetList[i].GetComponent<Transform>().localScale.x + scaleSpeed * Time.deltaTime;
            targetList[i].GetComponent<Transform>().localScale = new Vector3(newScale, newScale, newScale);

            // Color transformation over time.
            float red = targetList[i].GetComponent<MeshRenderer>().material.color.r;
            float green = targetList[i].GetComponent<MeshRenderer>().material.color.g;
            float blue = targetList[i].GetComponent<MeshRenderer>().material.color.b;
            float alpha = targetList[i].GetComponent<MeshRenderer>().material.color.a - fadeSpeed * Time.deltaTime;

            targetList[i].GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(red, green, blue, alpha));

            // Delete target when no longer needed.
            if (newScale > maxTargetScale) {
                Destroy(targetList[i]);
                targetList.RemoveAt(i);
            }
        }
    }

}
