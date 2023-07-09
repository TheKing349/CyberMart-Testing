using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public CanvasHandler canvasHandlerScript;
    public RaycastBuilding raycastBuildingScript;
    public GameTimeScale gameTimeScaleScript;

    public KeyCode playerJumpKey;

    public KeyCode toggleBuildingCanvasKey;
    public KeyCode toggleGridKey;
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;
    public KeyCode moveObjectKey;
    public KeyCode destroyObjectKey;

    public KeyCode pauseResumeKey;

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
