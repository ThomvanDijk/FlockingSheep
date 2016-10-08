using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DogBehaviour : MonoBehaviour {

    public GameObject targetPrefab;
    public GameObject raycastLayer;
    public float scaleSpeed = 1.5f;
    public float fadeSpeed = 0.1f;
    public float maxTargetScale = 2.5f;

    private Vector2 target;
    private List<GameObject> targetList;

    // Use this for initialization
    void Start () {
        target = this.transform.position;
        targetList = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
        
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
