using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    // Scene states ie menu's.
    public enum SceneStates { Pause, End, NoState };
    public static SceneStates currentstate;

    // Scene objects.
    public GameObject pauseMenu;
    public GameObject endScreen;

    void Start() {
        currentstate = SceneStates.NoState;
    }

	void Update () {

        // Check is esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            currentstate = SceneStates.Pause;
        }

        // Checks current scene state.
        switch (currentstate) {

            case SceneStates.Pause:
                pauseMenu.SetActive(true);
                endScreen.SetActive(false);
                break;

            case SceneStates.End:
                pauseMenu.SetActive(false);
                endScreen.SetActive(true);
                break;

            case SceneStates.NoState:
                pauseMenu.SetActive(false);
                endScreen.SetActive(false);
                break;

        }
	
	}

    public void onResume() {
        currentstate = SceneStates.NoState;
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

}
