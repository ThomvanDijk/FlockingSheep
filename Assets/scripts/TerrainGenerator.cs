using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

    public float offSet = 0;
    public int gridHeight = 0;
    public int gridWidth = 0;
    public TextAsset level1;
    
    public GameObject grassTile;
    public GameObject tree1Tile;
    public GameObject tree2Tile;
    public GameObject fenceMid;
    public GameObject fenceEnd;
    public GameObject fenceCorner;

    public static List<GameObject> grassList;

    private List<GameObject> tileList;

    // Use this for initialization.
    void Start () {
        grassList = new List<GameObject>();
        tileList = new List<GameObject>();

        tileList.Add(grassTile);
        tileList.Add(tree1Tile);
        tileList.Add(tree2Tile);
        tileList.Add(fenceMid);
        tileList.Add(fenceEnd);
        tileList.Add(fenceCorner);

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

                // Check here if the gameobject is grass (1).
                if (tileType[row, col] == 1) {
                    grassList.Add(terrainBlock);
                }

                // Create random (green) color.
                float red = ((float)Random.Range(0, 80) / 100);
                float green = ((float)Random.Range(40, 100) / 100);
                float blue = ((float)Random.Range(0, 10) / 100);

                // Apply random color.
                //terrainBlock.GetComponent<Renderer>().material.color = new Color(red, green, blue); //Random.ColorHSV();
            }
        }
    }

}
