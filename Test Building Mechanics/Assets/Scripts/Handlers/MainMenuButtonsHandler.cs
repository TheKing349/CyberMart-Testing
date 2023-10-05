using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsHandler : MonoBehaviour
{
    public CurrentKeybinds currentKeybindsScript;
    public SettingsButtonsHandler settingsButtonsHandlerScript;

    public GameObject mainMenuCanvas;
    public GameObject settingsMenuCanvas;

    #region Main_Menu_Buttons
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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

    #region Back_Settings
    void Update()
    {
        if (Input.GetKeyDown(currentKeybindsScript.pauseResumeKey) && (settingsMenuCanvas.activeSelf))
        {
            settingsButtonsHandlerScript.SaveAndApplyButton();
            settingsMenuCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
    }
    #endregion
}
