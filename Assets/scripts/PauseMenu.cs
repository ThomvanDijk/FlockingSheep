using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    // Scene states ie menu's.
    public enum SceneStates { Pause, End, Intro0, Intro1, Intro2, GameOver };
    public static SceneStates currentstate;

    // Scene objects.
    public GameObject pauseMenu;
    public GameObject endScreen;
    public GameObject intro0;
    public GameObject intro1;
    public GameObject intro2;
    public GameObject gameOver;
    public GameObject mainCamera; // Here we can acces the SmoothCamera script.

    private List<GameObject> sceneList;

    void Start() {
        sceneList = new List<GameObject>();

        // Put all scenes in a list so we can easily turn them off.
        sceneList.Add(pauseMenu);
        sceneList.Add(endScreen);
        sceneList.Add(intro0);
        sceneList.Add(intro1);
        sceneList.Add(intro2);
        sceneList.Add(gameOver);

        currentstate = SceneStates.Intro0;
    }

    void Update () {
        // Check is esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentstate = SceneStates.Pause;
        }

        // Checks current scene state.
        switch (currentstate) {

            case SceneStates.Pause:
                disableAllScenes();
                pauseMenu.SetActive(true);
                break;

            case SceneStates.End:
                disableAllScenes();
                endScreen.SetActive(true);
                break;

            case SceneStates.Intro0:
                disableAllScenes();
                intro0.SetActive(true);
                break;

            case SceneStates.Intro1:
                disableAllScenes();
                intro1.SetActive(true);
                break;

            case SceneStates.Intro2:
                disableAllScenes();
                intro2.SetActive(true);
                break;

            case SceneStates.GameOver:
                disableAllScenes();
                gameOver.SetActive(true);
                break;

        }
	}

    // Disable all scenes.
    private void disableAllScenes() {
        foreach (var scene in sceneList) {
            scene.SetActive(false);
        }
    }

    public void onResume() {
        mainCamera.GetComponent<SmoothCamera>().start = false;
        disableAllScenes();
    }

    public void onRestart() {
        SceneManager.LoadScene("game");
    }

    public void onMain() {
        SceneManager.LoadScene("menu");
    }

    public void onQuit() {
        Application.Quit();
    }

    public void onNext0() {
        mainCamera.GetComponent<SmoothCamera>().objective = true;
        currentstate = SceneStates.Intro1;
    }

    public void onNext1() {
        mainCamera.GetComponent<SmoothCamera>().objective = false;
        mainCamera.GetComponent<SmoothCamera>().start = true;
        currentstate = SceneStates.Intro2;
    }

}
