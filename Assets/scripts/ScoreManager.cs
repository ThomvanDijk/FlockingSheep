using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public GameObject scoreObject;
    public static int score;

    // Use this for initialization
    void Start () {
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        scoreObject.GetComponent<Text>().text = "" + score;
    }
}
