using UnityEngine;
using System.Collections;

public class GridCreation : MonoBehaviour {

    public GameObject underground;

    // Use this for initialization.
    void Start () {
        int gridWidth = 50;
        int gridHeight = 50;

        // Create the grid.
        for (int row = 0; row < gridHeight; row++) {
            for (int col = 0; col < gridWidth; col++) {
                // Set the position.
                Vector3 pos = new Vector3(col, 0, row);

                // Create cube.
                GameObject terrainBlock = (GameObject)Instantiate(underground, pos, transform.rotation);

                // Set random color.
                terrainBlock.GetComponent<Renderer>().material.color = Random.ColorHSV();

            }
        }
    }
	
	// Update is called once per frame.
	void Update () {
	
	}
}
