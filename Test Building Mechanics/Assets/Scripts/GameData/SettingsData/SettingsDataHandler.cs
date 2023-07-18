using TMPro;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
    public SettingsDataManager settingsDataManagerScript;

    public TMP_Dropdown qualityDropdown;

    [HideInInspector] public SettingsData settingsData;

    public void SaveSettings()
    {
        settingsData = new SettingsData
        {
            qualityLevel = qualityDropdown.value
        };

        settingsDataManagerScript.WriteData();
    }

    public void LoadSettings()
    {
        settingsDataManagerScript.ReadData();

        qualityDropdown.value = settingsData.qualityLevel;

        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
    }
}
