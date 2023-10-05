using TMPro;
using UnityEngine;

public class TogglesDebugChanger : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public CurrentKeybinds currentKeybindsScript;
    public RaycastBuilding raycastBuildingScript;

    public TMP_Text togglesText;

    private void FixedUpdate()
    {
        togglesText.text =
            "Toggles:\r\n" +
            "isGridSnap: " + raycastBuildingScript.isGridSnap + "\r\n" +
            "isBlueprintFollowingCursor: " + raycastBuildingScript.isBlueprintFollowingCursor;
    }
}
