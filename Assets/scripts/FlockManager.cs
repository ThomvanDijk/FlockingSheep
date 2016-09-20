using UnityEngine;
using System.Collections;

public class FlockManager : MonoBehaviour {

    public int numberOfSheep = 10;
    public GameObject sheepPrefab;
    public Transform sheepSpawn;

    // Use this for initialization
    void Start () {

        // Here the flock is created.
        for (int i = 0; i < numberOfSheep; i++) {
            addSheep();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void addSheep() {
        // Create sheep
        GameObject sheep = (GameObject)Instantiate(sheepPrefab, sheepSpawn.position, sheepSpawn.rotation);
    }
}
