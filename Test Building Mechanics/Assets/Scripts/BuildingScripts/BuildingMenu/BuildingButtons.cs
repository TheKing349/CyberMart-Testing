using UnityEngine;
using UnityEngine.UI;

public class BuildingButtons : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;
    public GameTimeScale gameTimeScaleScript;
    public Sprites spritesScript;

    public Image previousPrefabImage;
    public Image currentPrefabImage;
    public Image nextPrefabImage;

    [HideInInspector] public int currentPrefabInt = 0;

    private int prefabBlueprintCount;

    private void Start()
    {
        prefabBlueprintCount = raycastBuildingScript.prefabBlueprints.Count - 1;
    }

    public void RotateBuildingMenuLeft()
    {
        raycastBuildingScript.DeselectBlueprint();

        currentPrefabInt--;
        if (currentPrefabInt == -1)
        {
            currentPrefabInt = prefabBlueprintCount;
        }

        SelectBlueprint(currentPrefabInt);
    }

    public void RotateBuildingMenuRight()
    {
        raycastBuildingScript.DeselectBlueprint();

        currentPrefabInt++;
        if (currentPrefabInt > prefabBlueprintCount)
        {
            currentPrefabInt = 0;
        }

        SelectBlueprint(currentPrefabInt);
    }

    public void SelectBlueprint(int prefabInt)
    {
        int previousPrefabInt = prefabInt - 1;
        int nextPrefabInt = prefabInt + 1;
        if (previousPrefabInt < 0)
        {
            previousPrefabInt = prefabBlueprintCount;
        }
        if (nextPrefabInt > prefabBlueprintCount)
        {
            nextPrefabInt = 0;
        }
        previousPrefabImage.sprite = spritesScript.spritesList[previousPrefabInt];
        currentPrefabImage.sprite = spritesScript.spritesList[prefabInt];
        nextPrefabImage.sprite = spritesScript.spritesList[nextPrefabInt];
        raycastBuildingScript.SelectBlueprint(prefabInt);
    }

    public void DeselectBlueprint()
    {
        raycastBuildingScript.DeselectBlueprint();
    }
}