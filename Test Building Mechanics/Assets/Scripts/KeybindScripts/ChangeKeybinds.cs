using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKeybinds : MonoBehaviour
{
    public KeybindsDictionarySwitcher keybindsDictionarySwitcherScript;
    public KeybindsInitializer keybindsInitializerScript;
    public DefaultKeybinds defaultKeybindsScript;

    public SettingsDataHandler settingsDataHandlerScript;

    public WarningMenuHandler warningMenuHandlerScript;

    public GameObject keybindsScrollViewContent;
    [HideInInspector] public List<Button> keybindButtonsList;

    public Dictionary<string, KeyCode> keybindsDictionary = new Dictionary<string, KeyCode>();

    void Start()
    {
        keybindsDictionary = keybindsDictionarySwitcherScript.CopyFirstToSecond(settingsDataHandlerScript.keybindsDictionary, keybindsDictionary);
        keybindButtonsList = keybindsScrollViewContent.GetComponentsInChildren<Button>().ToList();
        foreach (Button button in keybindButtonsList)
        {
            button.onClick.AddListener(() =>
            {
                StartCoroutine(ClickKeybindButton(true, button.GetComponentInChildren<TMP_Text>().text, button.transform.parent.GetChild(1).GetComponent<TMP_Text>().text));
            });
            
            keybindsInitializerScript.ChangeKeybindButtonsText(button);
        }
    }

    public IEnumerator ClickKeybindButton(bool wait, string currentKeyCodeString, string newKeyCodeName)
    {
        while (wait)
        {
            if (Input.anyKeyDown)
            {
                KeyCode newKeyCode = KeyCode.None;
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if ((Input.GetKey(keyCode)) && (keyCode != KeyCode.Escape))
                    {
                        newKeyCode = keyCode;
                        break;
                    }
                }

                //overlapping keyCodes
                if (keybindsDictionary.ContainsValue(newKeyCode))
                {
                    string currentKeyCodeName = keybindsDictionary.FirstOrDefault(kvp => kvp.Value == newKeyCode).Key;

                    foreach (Button button in keybindButtonsList)
                    {
                        string buttonParentName = LowercaseFirst(button.transform.parent.name.Replace("Keybind", ""));
                        if (currentKeyCodeName == buttonParentName)
                        {
                            currentKeyCodeName = button.transform.parent.GetChild(1).GetComponent<TMP_Text>().text;
                            break;
                        }
                    }
                    warningMenuHandlerScript.EnableWarningMenu("overlap", newKeyCode, currentKeyCodeName, newKeyCodeName);
                }
                else
                {
                    foreach (Button button in keybindButtonsList)
                    {
                        if (currentKeyCodeString == button.GetComponentInChildren<TMP_Text>().text)
                        {
                            string buttonParentName = LowercaseFirst(button.transform.parent.name.Replace("Keybind", ""));
                            keybindsDictionary[buttonParentName] = newKeyCode;
                            button.GetComponentInChildren<TMP_Text>().text = newKeyCode.ToString();
                            break;
                        }
                    }
                }
                wait = false;
            }
            yield return null;
        }
    }

    public string LowercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToLower(a[0]);
        return new string(a);
    }
}
