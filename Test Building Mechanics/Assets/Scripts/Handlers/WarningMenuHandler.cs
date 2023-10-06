using TMPro;
using UnityEngine;

public class WarningMenuHandler : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text contentText;

    public GameObject warningMenuCanvas;

    [HideInInspector] public KeyCode newKeyCode;

    [HideInInspector] public string warningKeyword = "";
    [HideInInspector] public string newKeyCodeNameText = "";

    public void EnableWarningMenu(string keyword, KeyCode keyCode = KeyCode.None, string currKeyCodeName = "", string newKeyCodeName = "")
    {
        warningKeyword = keyword;
        newKeyCode = keyCode;
        newKeyCodeNameText = newKeyCodeName;

        warningMenuCanvas.SetActive(true);

        if (keyword == "overlap")
        {
            titleText.text = "MAPPED KEYBINDS";

            contentText.text = keyCode.ToString() + " is currently mapped to " + currKeyCodeName + ". Remove " + currKeyCodeName + " and map it to " + newKeyCodeName + "?";

            //yes --> Remove [CURRENT MAPPED KEYBIND] and map [KEYBIND] to [NEW KEYBIND NAME]
            //no  --> keep current config
        }
        else if (keyword == "default")
        {
            titleText.text = "FACTORY DEFAULTS";
            contentText.text = "Reset ALL settings to the defaults?";

            //yes --> apply defaults
            //no  --> keep current config
        }
        else if (keyword == "unsaved")
        {
            titleText.text = "UNSAVED CHANGES DETECTED";
            contentText.text = "Would you like to apply your changes?";

            //yes --> apply changes
            //no  --> discard changes
        }
    }
}