// Deze MenuManager scrollt heel makkelijk door menu's
// Het kostte me ongeveer 2,5 uur om te maken.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MenuManager : MonoBehaviour {

    // Scene states ie menu's.
    public enum SceneStates { Main, Credits, GameOver };
    public static SceneStates currentstate;

    // Scene objects.
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject gameOver;

	// Update is called once per frame
	void Update () {

        // Checks current scene state.
        switch (currentstate) {

            case SceneStates.Main:
                creditsMenu.SetActive(false);
                gameOver.SetActive(false);
                mainMenu.SetActive(true);
                break;

            case SceneStates.Credits:
                mainMenu.SetActive(false);
                gameOver.SetActive(false);
                creditsMenu.SetActive(true);
                break;

            case SceneStates.GameOver:
                mainMenu.SetActive(false);
                creditsMenu.SetActive(false);
                gameOver.SetActive(true);
                break;

        }
	
	}

    public void onStartGame() {
        SceneManager.LoadScene("game");
    }

    public void onCredits() {
        currentstate = SceneStates.Credits;
    }

    public void onQuit() {
        Application.Quit();
    }

    public void onMainMenu() {
        currentstate = SceneStates.Main;
    }

}
