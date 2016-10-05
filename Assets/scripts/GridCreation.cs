using UnityEngine;
using System.Collections;

public class GridCreation : MonoBehaviour {

    public GameObject underground;

    // Use this for initialization.
    void Start () {
        int gridWidth = 100;
        int gridHeight = 100;

        // Create the grid.
        for (int row = 0; row < gridHeight; row++) {
            for (int col = 0; col < gridWidth; col++) {
                // Set the position.
                Vector3 pos = new Vector3(col, 0, row);

                // Create cube.
                GameObject terrainBlock = (GameObject)Instantiate(underground, pos, transform.rotation);

                // Set random color.
                //terrainBlock.GetComponent<Renderer>().material.color = Random.ColorHSV();
                terrainBlock.GetComponent<Renderer>().material.color = Color.black;

            }
        }
    }
	
	// Update is called once per frame.
	void Update () {
	
	}
}
