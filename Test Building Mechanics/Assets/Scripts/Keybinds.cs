using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;

    public KeyCode buildKey;
    public KeyCode toggleGridKey;
    public KeyCode placeBlueprintKey;
    public KeyCode deselectBlueprintKey;
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;

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

        if (Input.GetKeyDown(buildKey))
        {
            raycastBuildingScript.BuildBlueprint();
        }

        if (Input.GetKeyDown(toggleGridKey))
        {
            raycastBuildingScript.isGridSnap = !raycastBuildingScript.isGridSnap;
        }

        if (Input.GetKeyDown(placeBlueprintKey))
        {
            raycastBuildingScript.PlaceBlueprint();
        }
        else if (Input.GetKeyDown(deselectBlueprintKey))
        {
            raycastBuildingScript.DeselectBlueprint();
        }
    }
}
