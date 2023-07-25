using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsButtonsHandler : MonoBehaviour
{
    public SettingsDataHandler settingsDataHandlerScript;

    public Button backButton;
    public TMP_Dropdown qualityDropdown;

    public void BackButton()
    {
        settingsDataHandlerScript.SaveSettings();
        SceneManager.LoadScene(PreviousSceneManager.instance.prevScene);
    }

    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }
}