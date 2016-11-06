using UnityEngine;
using System.Collections;

public class CircleAround : MonoBehaviour {

	public GameObject target;
	private bool orbitY;

	// Use this for initialization
	void Start () {
        orbitY = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.LookAt (target.transform);
			if (orbitY) {
				transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * 1);
			}
		}
	}
}