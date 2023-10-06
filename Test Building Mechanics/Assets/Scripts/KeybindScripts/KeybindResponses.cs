using UnityEngine;

public class KeybindResponses : MonoBehaviour
{
    public CurrentKeybinds currentKeybindsScript;
    public DefaultKeybinds defaultKeybindsScript;
    public BuildingButtons buildingButtonsScript;
    public RaycastBuilding raycastBuildingScript;
    public GameCanvasHandler gameCanvasHandlerScript;
    public GameTimeScale gameTimeScaleScript;

    void Update()
    {
        #region BUILDING_PLACING
        if ((Input.GetKeyDown(currentKeybindsScript.buildingCanvasKey)) && (!raycastBuildingScript.isBlueprintFollowingCursor))
        {
            gameCanvasHandlerScript.ToggleBuildingCanvas();
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
                raycastBuildingScript.isGridSnap = raycastBuildingScript.prevIsGridSnap;
                raycastBuildingScript.DeselectBlueprint();
                gameCanvasHandlerScript.ToggleBuildingCanvas();
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
            gameCanvasHandlerScript.TogglePauseCanvas();
        }
        #endregion
    }
}