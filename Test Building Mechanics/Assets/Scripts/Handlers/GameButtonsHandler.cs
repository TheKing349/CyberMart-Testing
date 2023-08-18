using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonsHandler : MonoBehaviour
{
    public GameDataManager gameDataManagerScript;
    public GameTimeScale gameTimeScaleScript;
    public RaycastBuilding raycastBuildingScript;
    public SettingsDataHandler settingsDataHandlerScript;

    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;
    public GameObject modsMenuCanvas;

    #region Pause_Menu_Buttons
    public void ResumeGame()
    {
        gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
    }

    public void Settings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

    public void Mods()
    {
        pauseMenuCanvas.SetActive(false);
        modsMenuCanvas.SetActive(true);
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
    #endregion

    #region Settings_Menu_Buttons
    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(settingsDataHandlerScript.qualityDropdown.value);
    }

    public void BackButton()
    {
        settingsDataHandlerScript.SaveSettings();

        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
    }
    #endregion
}