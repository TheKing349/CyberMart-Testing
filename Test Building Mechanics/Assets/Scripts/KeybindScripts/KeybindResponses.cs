using UnityEngine;

public class KeybindResponses : MonoBehaviour
{
    public CurrentKeybinds currentKeybindsScript;
    public DefaultKeybinds defaultKeybindsScript;
    public BuildingButtons buildingButtonsScript;
    public RaycastBuilding raycastBuildingScript;
    public CanvasHandler canvasHandlerScript;
    public GameTimeScale gameTimeScaleScript;

    void Update()
    {
        #region BUILDING_PLACING
        if ((Input.GetKeyDown(currentKeybindsScript.buildingCanvasKey)) && (!raycastBuildingScript.isBlueprintFollowingCursor))
        {
            canvasHandlerScript.ToggleBuildingCanvas();
        }

        if (!gameTimeScaleScript.isGamePaused)
        {
            if (Input.GetKeyDown(currentKeybindsScript.blueprintGridKey))
            {
                raycastBuildingScript.ToggleGridSnap();
            }

            if (Input.GetKeyDown(currentKeybindsScript.blueprintMoveKey))
            {
                raycastBuildingScript.MoveObject();
            }
            else if (Input.GetKeyDown(currentKeybindsScript.blueprintDestroyKey))
            {
                raycastBuildingScript.DestroyObject();
            }

            if (Input.GetMouseButtonDown(defaultKeybindsScript.buildBlueprintKey))
            {
                raycastBuildingScript.BuildBlueprint();
            }
            else if (Input.GetMouseButtonDown(defaultKeybindsScript.deselectBlueprintKey))
            {
                raycastBuildingScript.DeselectBlueprint();
            }

            if (Input.GetKeyDown(currentKeybindsScript.buildingLeftKey))
            {
                buildingButtonsScript.RotateBuildingMenuLeft();
            }
            if (Input.GetKeyDown(currentKeybindsScript.buildingRightKey))
            {
                buildingButtonsScript.RotateBuildingMenuRight();
            }
        }
        #endregion

        #region PAUSE_RESUME
        if (Input.GetKeyDown(currentKeybindsScript.pauseResumeKey))
        {
            canvasHandlerScript.TogglePauseCanvas();
        }
        #endregion
    }
}