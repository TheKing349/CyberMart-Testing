using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsDataHandler : MonoBehaviour
{
    public KeybindsInitializer keybindsInitializerScript;
    public DefaultKeybinds defaultKeybindsScript;
    public ChangeKeybinds changeKeybindsScript;

    public MainMenuDataManager mainMenuDataManagerScript;
    public GameDataManager gameDataManagerScript;

    public TMP_Dropdown qualityDropdown;

    [HideInInspector] public SettingsData settingsData;
    [HideInInspector] public int qualityLevel;
    public Dictionary<string, KeyCode> keybindsDictionary = new Dictionary<string, KeyCode>();

    public void SaveSettings()
    {
        settingsData = new SettingsData
        {
            qualityLevel = qualityLevel,
            keybindsDictionary = keybindsDictionary
        };

        if (mainMenuDataManagerScript != null )
        {
            mainMenuDataManagerScript.WriteData();
        }
        else
        {
            gameDataManagerScript.WriteData();
        }

        LoadSettings();
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
        QualitySettings.SetQualityLevel(settingsData.qualityLevel, true);

        keybindsDictionary = settingsData.keybindsDictionary;
        foreach (Button button in changeKeybindsScript.keybindButtonsList)
        {
            keybindsInitializerScript.ChangeKeybindButtonsText(button);
        }
    }
}