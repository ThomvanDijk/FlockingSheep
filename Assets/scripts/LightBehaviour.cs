using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        float rotation = map(ScoreManager.countdown, 0, 100, 0, 180);
        transform.localRotation = Quaternion.Euler(rotation, rotation/2, 0);
    }

    // map(973, 0, 1023, 0, 255); // returns: 242
    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
