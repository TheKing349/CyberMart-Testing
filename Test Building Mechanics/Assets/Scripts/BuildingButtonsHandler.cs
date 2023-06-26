using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonsHandler : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;

    public Button buildingCubeButton;
    public Button buildingSphereButton;
    public Button buildingWallButton;

    private void Start()
    {
        buildingCubeButton.onClick.AddListener(() => raycastBuildingScript.SelectBlueprint(0));
        buildingSphereButton.onClick.AddListener(() => raycastBuildingScript.SelectBlueprint(1));
        buildingWallButton.onClick.AddListener(() => raycastBuildingScript.SelectBlueprint(2));
    }
}
