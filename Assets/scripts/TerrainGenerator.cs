﻿using UnityEngine;
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
    public GameObject grassAsset;

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

                if (tileType[row, col] == 0) {
                    // Create additional grass.
                    GameObject grass = (GameObject)Instantiate(grassAsset, new Vector3(
                        terrainBlock.transform.position.x + Random.Range(-5, 5) * 0.1f,
                        terrainBlock.transform.position.y,
                        terrainBlock.transform.position.z + Random.Range(-5, 5) * 0.1f), 
                        Quaternion.Euler(0, Random.Range(0, 365), 0));

                    // Add the grass asset as a child
                    grass.transform.parent = terrainBlock.transform;
                }

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

                // Here check if the tile is a straight fence, if it is then rotate it correctly.
                if (tileType[row, col] == 3) {
                    if (tileType[row - 1, col] >= 3 || tileType[row + 1, col] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }

                // Fence end.
                if (tileType[row, col] == 4) {
                    if (tileType[row - 1, col] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, -90, 0));
                    }
                    if (tileType[row + 1, col] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }

                // Fence corner.
                if (tileType[row, col] == 5) {
                    if (tileType[row - 1, col] >= 3 && tileType[row, col - 1] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, -90, 0));
                    }
                    if (tileType[row, col + 1] >= 3 && tileType[row - 1, col] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, 180, 0));
                    }
                    if (tileType[row, col + 1] >= 3 && tileType[row + 1, col] >= 3) {
                        terrainBlock.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }
            }
        }
    }

}
