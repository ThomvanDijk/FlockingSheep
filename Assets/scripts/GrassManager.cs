using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GrassManager : MonoBehaviour {

    private float grassValue; // 0% is no grass.
    private float stayedLongEnough;
    private float onGrassTimer;
    private bool gaveScore;
    private float grazeSpeed;

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
                }
                if (grassValue <= 0 && !gaveScore) {
                    ScoreManager.score += 1;
                    gaveScore = true;
                }

                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(100, 100, 100);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        //onGrassTimer = 0;
    }

}
