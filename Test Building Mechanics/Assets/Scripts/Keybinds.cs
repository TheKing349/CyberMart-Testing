using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;

    public KeyCode toggleGridKey;
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;
    public KeyCode moveObjectKey;
    public KeyCode destroyObjectKey;
    public int buildBlueprintKey;
    public int deselectBlueprintKey;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            raycastBuildingScript.SelectBlueprint(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            raycastBuildingScript.SelectBlueprint(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            raycastBuildingScript.SelectBlueprint(2);
        }

        if (Input.GetKeyDown(toggleGridKey))
        {
            raycastBuildingScript.ToggleGridSnap();
        }

        if (Input.GetKeyDown(moveObjectKey))
        {
            raycastBuildingScript.MoveObject();
        }

        if (Input.GetKeyDown(destroyObjectKey))
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
}
