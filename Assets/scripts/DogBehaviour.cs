using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DogBehaviour : MonoBehaviour {

    public GameObject targetPrefab;
    public GameObject raycastLayer;
    public float scaleSpeed = 0;
    public float fadeSpeed = 0;
    public float maxTargetScale = 0;
    public float maxspeed = 0;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;
    private Vector2 direction;

    private Vector2 target;
    private List<GameObject> targetList;
    private float rotation;

    // Use this for initialization
    void Start () {
        position = new Vector2(this.transform.position.x, this.transform.position.z);
        velocity = new Vector2(0, 0);
        acceleration = new Vector2(0, 0);
        direction = new Vector2(0, 0);

        target = new Vector2(this.transform.position.x, this.transform.position.y);
        targetList = new List<GameObject>();
        rotation = 0;
    }
	
	// Update is called once per frame
	void Update () {

        setTarget();
        clickFeedback(); // Shows feedback when new target is set.

        //direction = target.sub(position);

        Vector2 temp = new Vector2(1, 1);
        direction = temp.sub(position);

        direction.normalize();
        direction.multS(0.5f);
        acceleration = direction;

        velocity = velocity.add(acceleration);
        velocity = velocity.limit(maxspeed);

        Debug.Log(velocity);
        Debug.Log(" limit: " + velocity.limit(maxspeed) + " maxspeed: " + maxspeed);

        position = position.add(velocity);

        // Translate Vector2 position to the dog.
        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    private void setTarget() {
        // Save the place where the mouse clicked right.
        if (Input.GetMouseButtonDown(1)) {

            RaycastHit hit; // Position where mouse hits the raycast.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (raycastLayer.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {

                // Greate target point for visuals.
                GameObject targetIndicator = (GameObject)Instantiate(targetPrefab, hit.point, transform.rotation);
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
