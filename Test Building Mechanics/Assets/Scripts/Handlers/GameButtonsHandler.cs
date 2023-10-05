using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonsHandler : MonoBehaviour
{
    public GameDataManager gameDataManagerScript;
    public GameTimeScale gameTimeScaleScript;

    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;

    [HideInInspector] public List<GameObject> buildingTabButtons;

    public void ResumeGame()
    {
        gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
    }

    public void Settings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

    public void QuitToMenu()
    {
        gameDataManagerScript.WriteData();

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        gameDataManagerScript.WriteData();

        Application.Quit();
    }
}