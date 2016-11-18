using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    // Scene states ie menu's.
    public enum SceneStates { Main, Credits, Levels };
    public static SceneStates currentstate;

    // Scene objects.
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject levelsMenu;

	// Update is called once per frame
	void Update () {

        // Checks current scene state.
        switch (currentstate) {

            case SceneStates.Main:
                creditsMenu.SetActive(false);
                levelsMenu.SetActive(false);
                mainMenu.SetActive(true);
                break;

            case SceneStates.Credits:
                mainMenu.SetActive(false);
                levelsMenu.SetActive(false);
                creditsMenu.SetActive(true);
                break;

            case SceneStates.Levels:
                mainMenu.SetActive(false);
                creditsMenu.SetActive(false);
                levelsMenu.SetActive(true);
                break;

        }
	
	}

    public void onStartGame() {
        SceneManager.LoadScene("game");
    }

    public void onCredits() {
        currentstate = SceneStates.Credits;
    }

    public void onLevels() {
        currentstate = SceneStates.Levels;
    }

    public void onQuit() {
        Application.Quit();
    }

    public void onMainMenu() {
        currentstate = SceneStates.Main;
    }

}
