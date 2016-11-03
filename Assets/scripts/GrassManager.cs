using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GrassManager : MonoBehaviour {

    private float grassValue; // 0% is no grass.
    private float stayedLongEnough;
    private float onGrassTimer;
    private bool gaveScore;
    private float grazeSpeed;
    private float r;
    private float g;
    private float b;

    // Use this for initialization
    void Start () {
        grassValue = 100; // 100%.
        stayedLongEnough = 3; // 3 seconds.
        onGrassTimer = 0;
        gaveScore = false;
        grazeSpeed = 2;
    }

    void OnTriggerStay(Collider other) {
        //other.GetInstanceID

        // Check if the other object is indeed a sheep.
        if (other.CompareTag("Sheep")) {

            if (onGrassTimer < stayedLongEnough) {
                onGrassTimer += Time.deltaTime;
            }
            if (onGrassTimer >= stayedLongEnough) {

                if (grassValue > 0) {
                    grassValue -= grazeSpeed * Time.deltaTime;

                    // Optionally I need a system where you take two color values and change it according to the percentage level.

                    float greenValue = map(grassValue, 0, 100, 0, 255) - 80; // Convert percentage value to value between 0 and 255.
                    greenValue = map(greenValue, 0, 255, 0, 1) ;    // Convert value between 0 and 255 to value between 0 and 1.

                    g = map(80, 0, 255, 0, 1) + greenValue - map(35, 0, 255, 0, 1); // Convert minimal value of 80 to value between 0 and 1. Now add the 
                    // greenvalue, this way the grass gets a brown or green color dependent on the green value.

                    r = transform.GetChild(0).GetComponent<Renderer>().material.color.r;
                    b = transform.GetChild(0).GetComponent<Renderer>().material.color.b;

                    transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(r, g, b);
                }
                if (grassValue <= 0 && !gaveScore) {
                    ScoreManager.score += 1;
                    gaveScore = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        onGrassTimer = 0;
    }

    // map(973, 0, 1023, 0, 255); // returns: 242
    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

}
