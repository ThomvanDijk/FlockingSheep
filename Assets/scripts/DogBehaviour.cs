using UnityEngine;
using System.Collections;

public class DogBehaviour : MonoBehaviour {

    private Vector2 target;

    public GameObject targetPrefab;
    public GameObject raycastLayer;

    // Use this for initialization
    void Start () {
        target = this.transform.position;
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
                target = new Vector2(hit.point.x, hit.point.z);
            }

        }
	}
}
