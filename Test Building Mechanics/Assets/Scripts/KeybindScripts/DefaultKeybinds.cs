using UnityEngine;

[CreateAssetMenu(fileName = "DefaultKeybinds", menuName = "Custom/DefaultKeybinds")]
public class DefaultKeybinds : ScriptableObject
{
    public KeyCode playerJumpKey;
    public KeyCode playerForwardKey;
    public KeyCode playerLeftKey;
    public KeyCode playerBackwardKey;
    public KeyCode playerRightKey;

    public KeyCode buildingCanvasKey;
    public KeyCode buildingLeftKey;
    public KeyCode buildingRightKey;

    public KeyCode blueprintGridKey;
    public KeyCode blueprintLeftKey;
    public KeyCode blueprintRightKey;

    public KeyCode blueprintMoveKey;
    public KeyCode blueprintDestroyKey;

    public KeyCode pauseResumeKey;

    public int buildBlueprintKey;
    public int deselectBlueprintKey;
}
