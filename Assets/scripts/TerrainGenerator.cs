using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

    public TextAsset level1;
    
    public GameObject grassTile;
    public GameObject tree1Tile;
    public GameObject tree2Tile;
    public GameObject fenceMid;
    public GameObject fenceEnd;
    public GameObject fenceCorner;

    public Material grassLight;
    public Material grassMed;
    public Material grassDark;

    private float offSet;
    private List<GameObject> tileList;

    // Use this for initialization.
    void Start () {
        tileList = new List<GameObject>();

        tileList.Add(grassTile);
        tileList.Add(tree1Tile);
        tileList.Add(tree2Tile);
        tileList.Add(fenceMid);
        tileList.Add(fenceEnd);
        tileList.Add(fenceCorner);

        offSet = 2;

        // Here the actual grid/terrain is generated.
        gridCreator();
    }

    private void gridCreator() {
        string levelString = level1.text;
        List<string> levelRow = new List<string>();
        List<string> levelColumn = new List<string>();

        // Adding all rows.
        levelRow.AddRange(levelString.Split("\n"[0]));
        levelColumn.AddRange(levelRow[0].Split(","[0]));

        int rowCount = levelRow.Count;
        int columnCount = levelColumn.Count;

        int[,] tileType = new int[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++) {
            levelColumn.Clear();
            levelColumn.AddRange(levelRow[i].Split(","[0]));
            for (int j = 0; j < columnCount; j++) {
                tileType[i, j] = int.Parse(levelColumn[j]) - 1;
            }
        }

        // Create the grid.
        for (int row = 0; row < rowCount; row++) {
            for (int col = 0; col < columnCount; col++) {
                // Set the position.
                Vector3 pos = new Vector3(col * offSet, 0, row * offSet);

                // Create cube.
                GameObject terrainBlock = (GameObject)Instantiate(tileList[tileType[row, col]], pos, tileList[0].transform.rotation);

                int random = Random.Range(0, 3);

                // Tiletype 0 is grass.
                if (tileType[row, col] == 0) {
                    switch (random) {
                        case 0:
                            terrainBlock.transform.GetChild(0).GetComponent<Renderer>().material = grassLight;
                            break;
                        case 1:
                            terrainBlock.transform.GetChild(0).GetComponent<Renderer>().material = grassMed;
                            break;
                        case 2:
                            terrainBlock.transform.GetChild(0).GetComponent<Renderer>().material = grassDark;
                            break;
                    }
                }
                else {
                    switch (random) {
                        case 0:
                            terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material = grassLight;
                            break;
                        case 1:
                            terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material = grassMed;
                            break;
                        case 2:
                            terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material = grassDark;
                            break;
                    }

                    float green = 0.6f;

                    float red = terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material.color.r;
                    float blue = terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material.color.b;

                    terrainBlock.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color(red, green, blue);
                }
            }
        }
    }

}
