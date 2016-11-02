using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FlockManager : MonoBehaviour {

    public GameObject sheepPrefab;
    public GameObject sheepdog;
    public Transform sheepSpawn;

    public List<GameObject> sheepList;

    // Use this for initialization.
    void Start () {
        int numberOfSheep = 40;
        sheepList = new List<GameObject>();

        // Here the flock is created.
        for (int i = 0; i < numberOfSheep; i++) {
            addSheep();
        }
    }
	
	// Update is called once per frame.
	void Update () {
        foreach (var sheep in sheepList) {
            SheepBehaviour sheepScript = (SheepBehaviour)sheep.GetComponent(typeof(SheepBehaviour));

            // Passing the entire list of sheep to each sheep individually.
            sheepScript.updateSheep(sheepList, sheepdog);
        }
	}

    private void addSheep() {
        // Create sheep wit random position.
        GameObject sheep = (GameObject)Instantiate(sheepPrefab, new Vector3(
            sheepSpawn.position.x + Random.Range(-5, 5), 
            sheepSpawn.position.y,
            sheepSpawn.position.z + Random.Range(-5, 5)),
            sheepPrefab.transform.rotation);

        // Add sheep to list.
        sheepList.Add(sheep);
    }

}
