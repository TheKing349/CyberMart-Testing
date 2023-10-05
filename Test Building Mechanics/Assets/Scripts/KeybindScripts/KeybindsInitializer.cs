using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsInitializer : MonoBehaviour
{
    public SettingsDataHandler settingsDataHandlerScript;
    public DefaultKeybinds defaultKeybindsScript;
    public ChangeKeybinds changeKeybindsScript;

    void Awake()
    {
        changeKeybindsScript.keybindButtonsList = changeKeybindsScript.keybindsScrollViewContent.GetComponentsInChildren<Button>().ToList();

        PopulateKeybindsDictionary();
    }

    public void PopulateKeybindsDictionary()
    {
        bool hasAlreadyPopulatedDictionary = false;
        FieldInfo[] fields = typeof(DefaultKeybinds).GetFields();

        if (settingsDataHandlerScript.keybindsDictionary.ContainsKey(fields[0].Name.Replace("Key","")))
        {
            hasAlreadyPopulatedDictionary = true;
        }

        foreach (FieldInfo field in fields)
        {
            string fieldName = field.Name;
            object fieldValue = field.GetValue(defaultKeybindsScript);

            if (fieldValue is KeyCode keyCode)
            {
                if (hasAlreadyPopulatedDictionary)
                    settingsDataHandlerScript.keybindsDictionary[fieldName.Replace("Key", "")] = keyCode;
                else
                    settingsDataHandlerScript.keybindsDictionary.Add(fieldName.Replace("Key", ""), keyCode);
            }
        }
    }

    public void ChangeKeybindButtonsText(Button currentButton)
    {
        string buttonParentName = changeKeybindsScript.LowercaseFirst(currentButton.transform.parent.name.Replace("Keybind", ""));
        if (settingsDataHandlerScript.keybindsDictionary.ContainsKey(buttonParentName))
        {
            currentButton.GetComponentInChildren<TMP_Text>().text = settingsDataHandlerScript.keybindsDictionary[buttonParentName].ToString();
        }
    }
}