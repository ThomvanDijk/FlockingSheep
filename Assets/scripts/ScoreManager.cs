using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public GameObject scoreObject;
    public static int score;
    public static float countdown;

    // Use this for initialization
    void Start () {
        score = 0;
        countdown = 100;
    }
	
	// Update is called once per frame
	void Update () {
        scoreObject.GetComponent<Text>().text = "" + score;
        countdown -= Time.deltaTime;
    }
}
