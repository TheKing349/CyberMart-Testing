using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonsHandler : MonoBehaviour
{
    public KeybindsDictionarySwitcher keybindsDictionarySwitcherScript;
    public SettingsDataHandler settingsDataHandlerScript;
    public KeybindsInitializer keybindsInitializerScript;
    public CurrentKeybinds currentKeybindsScript;
    public ChangeKeybinds changeKeybindsScript;

    public WarningMenuHandler warningMenuHandlerScript;

    public GameObject mainMenuCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;

    public TMP_Dropdown qualityDropdown;

    public void SaveAndApplyButton()
    {
        settingsDataHandlerScript.qualityLevel = settingsDataHandlerScript.qualityDropdown.value;

        settingsDataHandlerScript.keybindsDictionary = keybindsDictionarySwitcherScript.CopyFirstToSecond(changeKeybindsScript.keybindsDictionary, settingsDataHandlerScript.keybindsDictionary);
        settingsDataHandlerScript.SaveSettings();

        currentKeybindsScript.UpdateCurrentKeybinds();

        QualitySettings.SetQualityLevel(settingsDataHandlerScript.qualityLevel, false);
    }

    public void DiscardButton()
    {
        settingsDataHandlerScript.qualityDropdown.value = settingsDataHandlerScript.qualityLevel;

        changeKeybindsScript.keybindsDictionary = keybindsDictionarySwitcherScript.CopyFirstToSecond(settingsDataHandlerScript.keybindsDictionary, changeKeybindsScript.keybindsDictionary);
        UpdateKeybindButtonText();
    }

    public void DefaultWarningButton()
    {
        warningMenuHandlerScript.EnableWarningMenu("default");
    }

    public void DefaultButton()
    {
        settingsDataHandlerScript.qualityLevel = 0;
        settingsDataHandlerScript.qualityDropdown.value = 0;

        keybindsInitializerScript.PopulateKeybindsDictionary();
        changeKeybindsScript.keybindsDictionary = keybindsDictionarySwitcherScript.CopyFirstToSecond(settingsDataHandlerScript.keybindsDictionary, changeKeybindsScript.keybindsDictionary);

        UpdateKeybindButtonText();
        SaveAndApplyButton();
    }

    public void CheckSavedBackButton()
    {
        if ((settingsDataHandlerScript.qualityDropdown.value != settingsDataHandlerScript.qualityLevel) || (!settingsDataHandlerScript.keybindsDictionary.SequenceEqual(changeKeybindsScript.keybindsDictionary)))
        {
            warningMenuHandlerScript.EnableWarningMenu("unsaved");
        }
        else
        {
            BackButton();
        }
    }

    public void BackButton()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            pauseMenuCanvas.SetActive(true);
        }
        settingsMenuCanvas.SetActive(false);
    }

    public void ConfirmButton()
    {
        if (warningMenuHandlerScript.warningKeyword == "overlap")
        {
            if (changeKeybindsScript.keybindsDictionary.ContainsValue(warningMenuHandlerScript.newKeyCode))
            {
                string currentKeyCodeDictionaryName = changeKeybindsScript.keybindsDictionary.FirstOrDefault(kvp => kvp.Value == warningMenuHandlerScript.newKeyCode).Key;
                changeKeybindsScript.keybindsDictionary[currentKeyCodeDictionaryName] = KeyCode.None;

                foreach (Button button in changeKeybindsScript.keybindButtonsList)
                {
                    if (button.transform.parent.GetChild(1).GetComponent<TMP_Text>().text == warningMenuHandlerScript.newKeyCodeNameText)
                    {
                        string buttonParentName = changeKeybindsScript.LowercaseFirst(button.transform.parent.name.Replace("Keybind", ""));
                        changeKeybindsScript.keybindsDictionary[buttonParentName] = warningMenuHandlerScript.newKeyCode;
                    }
                }
            }
            SaveAndApplyButton();
        }
        else if (warningMenuHandlerScript.warningKeyword == "default")
        {
            DefaultButton();
        }
        else if (warningMenuHandlerScript.warningKeyword == "unsaved")
        {
            SaveAndApplyButton();
            BackButton();
        }
        warningMenuHandlerScript.warningMenuCanvas.SetActive(false);
    }

    public void DenyButton()
    {
        if (warningMenuHandlerScript.warningKeyword == "unsaved")
        {
            DiscardButton();
            BackButton();
        }
        warningMenuHandlerScript.warningMenuCanvas.SetActive(false);
    }

    public void UpdateKeybindButtonText()
    {
        foreach (Button button in changeKeybindsScript.keybindButtonsList)
        {
            keybindsInitializerScript.ChangeKeybindButtonsText(button);
        }
    }
}