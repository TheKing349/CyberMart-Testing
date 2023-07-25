using TMPro;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
    public MainMenuDataManager mainMenuDataManagerScript;
    public GameDataManager gameDataManagerScript;

    public TMP_Dropdown qualityDropdown;

    [HideInInspector] public SettingsData settingsData;

    public void SaveSettings()
    {
        settingsData = new SettingsData
        {
            qualityLevel = qualityDropdown.value
        };

        if (mainMenuDataManagerScript != null )
        {
            mainMenuDataManagerScript.WriteData();
        }
        else
        {
            gameDataManagerScript.WriteData();
        }
    }

    public void LoadSettings()
    {
        if (mainMenuDataManagerScript != null)
        {
            mainMenuDataManagerScript.ReadData();
        }
        else
        {
            gameDataManagerScript.ReadData();
        }

        qualityDropdown.value = settingsData.qualityLevel;

        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
    }
}
