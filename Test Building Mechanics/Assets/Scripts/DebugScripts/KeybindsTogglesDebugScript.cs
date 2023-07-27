using TMPro;
using UnityEngine;

public class KeybindsTogglesDebugScript : MonoBehaviour
{
    public PlayerMovment playerMovementScript;
    public Keybinds keybindsScript;
    public RaycastBuilding raycastBuildingScript;

    public TMP_Text keybindsText;
    public TMP_Text togglesText;

    private void Start()
    {
        keybindsText.text =
            "Keybinds:\r\n" +
            "playerJumpkey: " + keybindsScript.playerJumpKey + "\r\n" +
            "toggleBuildingCanvasKey: " + keybindsScript.toggleBuildingCanvasKey + "\r\n" +
            "toggleGridKey: " + keybindsScript.toggleGridKey + "\r\n" +
            "rotateBlueprintLeftKey: " + keybindsScript.rotateBlueprintLeftKey + "\r\n" +
            "rotateBlueprintRightKey: " + keybindsScript.rotateBlueprintRightKey + "\r\n" +
            "moveObjectKey: " + keybindsScript.moveObjectKey + "\r\n" +
            "rotateBuildingMenuLeftKey: " + keybindsScript.rotateBuildingMenuLeftKey + "\r\n" +
            "rotateBuildingMenuRightKey: " + keybindsScript.rotateBuildingMenuRightKey + "\r\n" +
            "destroyObjectKey: " + keybindsScript.destroyObjectKey;
    }
    private void FixedUpdate()
    {
        togglesText.text =
            "Toggles:\r\n" +
            "isGridSnap: " + raycastBuildingScript.isGridSnap + "\r\n" +
            "isBlueprintFollowingCursor: " + raycastBuildingScript.isBlueprintFollowingCursor;
    }
}
