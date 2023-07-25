using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsHandler : MonoBehaviour
{
    public SettingsDataHandler settingsDataHandlerScript;

    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    #region Main_Menu_Buttons
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        mainMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
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

        mainMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
    }
    #endregion
}
