using UnityEngine;
using System.Collections;

public class GridCreation : MonoBehaviour {

    public GameObject tile;
    public float offSet = 0;

    // Use this for initialization.
    void Start () {
        int gridWidth = 80;
        int gridHeight = 80;

        // Create the grid.
        for (int row = 0; row < gridHeight; row++) {
            for (int col = 0; col < gridWidth; col++) {
                // Set the position.
                Vector3 pos = new Vector3(col * offSet, 0, row * offSet);

                // Create cube.
                GameObject terrainBlock = (GameObject)Instantiate(tile, pos, tile.transform.rotation);

                // Create random (green) color.
                float red = ((float)Random.Range(0, 80) / 100);
                float green = ((float)Random.Range(40, 100) / 100);
                float blue = ((float)Random.Range(0, 10) / 100);

                // Apply random color.
                terrainBlock.GetComponent<Renderer>().material.color = new Color(red, green, blue); //Random.ColorHSV();
            }
        }
    }

}
