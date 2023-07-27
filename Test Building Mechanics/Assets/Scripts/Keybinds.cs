using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public BuildingButtons buildingButtonsScript;
    public RaycastBuilding raycastBuildingScript;
    public CanvasHandler canvasHandlerScript;
    public GameTimeScale gameTimeScaleScript;

    public KeyCode playerJumpKey;

    public KeyCode toggleBuildingCanvasKey;
    public KeyCode toggleGridKey;
    public KeyCode rotateBlueprintLeftKey;
    public KeyCode rotateBlueprintRightKey;
    public KeyCode moveObjectKey;
    public KeyCode destroyObjectKey;

    public KeyCode pauseResumeKey;

    public KeyCode rotateBuildingMenuLeftKey;
    public KeyCode rotateBuildingMenuRightKey;

    public int buildBlueprintKey;
    public int deselectBlueprintKey;

    void Update()
    {
        #region BUILDING_PLACING
        if ((Input.GetKeyDown(toggleBuildingCanvasKey)) && (!raycastBuildingScript.isBlueprintFollowingCursor))
        {
            canvasHandlerScript.ToggleBuildingCanvas();
        }

        if (!gameTimeScaleScript.isGamePaused)
        {
            if (Input.GetKeyDown(toggleGridKey))
            {
                raycastBuildingScript.ToggleGridSnap();
            }

            if (Input.GetKeyDown(moveObjectKey))
            {
                raycastBuildingScript.MoveObject();
            }
            else if (Input.GetKeyDown(destroyObjectKey))
            {
                raycastBuildingScript.DestroyObject();
            }

            if (Input.GetMouseButtonDown(buildBlueprintKey))
            {
                raycastBuildingScript.BuildBlueprint();
            }
            else if (Input.GetMouseButtonDown(deselectBlueprintKey))
            {
                raycastBuildingScript.DeselectBlueprint();
            }

            if (Input.GetKeyDown(rotateBuildingMenuLeftKey))
            {
                buildingButtonsScript.RotateBuildingMenuLeft();
            }
            if (Input.GetKeyDown(rotateBuildingMenuRightKey))
            {
                buildingButtonsScript.RotateBuildingMenuRight();
            }
        }


        #endregion

        #region PAUSE_RESUME
        if (Input.GetKeyDown(pauseResumeKey))
        {
            canvasHandlerScript.TogglePauseCanvas();
        }
        #endregion
    }
}
