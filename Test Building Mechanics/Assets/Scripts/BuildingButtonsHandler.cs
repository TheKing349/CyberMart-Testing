using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonsHandler : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;

    public Transform buildingMenu;

    private void Start()
    {
        for (int i = 0; i < buildingMenu.childCount; i++)
        {
            int closureIndex = i;
            Button child = buildingMenu.GetChild(i).GetComponent<Button>();
            child.onClick.AddListener( () => raycastBuildingScript.SelectBlueprint(closureIndex));
        }
    }
}