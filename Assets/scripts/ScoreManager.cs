using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text countdownText;
    public static int score;
    public static float countdown;

    // Use this for initialization
    void Start () {
        score = 0;
        countdown = 600; // 10 minutes.
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
        countdownText.text = "Time left: " + (int)countdown;
        countdown -= Time.deltaTime;
    }
}
