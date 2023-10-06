using System.Reflection;
using UnityEngine;

public class CurrentKeybinds : MonoBehaviour
{
    public DefaultKeybinds defaultKeybindsScript;
    public SettingsDataHandler settingsDataHandlerScript;

    FieldInfo[] currentKeybindsFields;

    [HideInInspector] public KeyCode playerJumpKey;
    [HideInInspector] public KeyCode playerForwardKey;
    [HideInInspector] public KeyCode playerLeftKey;
    [HideInInspector] public KeyCode playerBackwardKey;
    [HideInInspector] public KeyCode playerRightKey;

    [HideInInspector] public KeyCode buildingCanvasKey;
    [HideInInspector] public KeyCode buildingLeftKey;
    [HideInInspector] public KeyCode buildingRightKey;

    [HideInInspector] public KeyCode blueprintGridKey;
    [HideInInspector] public KeyCode blueprintLeftKey;
    [HideInInspector] public KeyCode blueprintRightKey;

    [HideInInspector] public KeyCode blueprintMoveKey;
    [HideInInspector] public KeyCode blueprintDestroyKey;

    [HideInInspector] public KeyCode pauseResumeKey;

    void Start()
    {
        currentKeybindsFields = typeof(CurrentKeybinds).GetFields();
        UpdateCurrentKeybinds();
    }

    public void UpdateCurrentKeybinds()
    {
        foreach (var field in currentKeybindsFields)
        {
            string fieldName = field.Name.Replace("Key", "");
            if (settingsDataHandlerScript.keybindsDictionary.ContainsKey(fieldName))
            {
                field.SetValue(this, settingsDataHandlerScript.keybindsDictionary[fieldName]);
            }
        }
    }
}