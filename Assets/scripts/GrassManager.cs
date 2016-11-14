using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GrassManager : MonoBehaviour {

    public float grassValue; // 0% is no grass.

    private float stayedLongEnough;
    private float onGrassTimer;
    private bool gaveScore;
    private float grazeSpeed;
    private float red;
    private float green;
    private float blue;

    // Use this for initialization
    void Start () {
        grassValue = 100; // 100%.
        stayedLongEnough = 3; // 3 seconds.
        onGrassTimer = 0;
        gaveScore = false;
        grazeSpeed = 8;
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

                    float greenValue = map(grassValue, 0, 100, 0, 0.4f); 

                    green = 0.6f + greenValue; 

                    red = transform.GetChild(0).GetComponent<Renderer>().material.color.r;
                    blue = transform.GetChild(0).GetComponent<Renderer>().material.color.b;

                    transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(red, green, blue);

                    // Scale the grass on the tiles, do x 0.01 because grassvalue is 100.
                    transform.GetChild(1).GetComponent<Transform>().localScale = new Vector3(1, grassValue * 0.01f, 1);
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
