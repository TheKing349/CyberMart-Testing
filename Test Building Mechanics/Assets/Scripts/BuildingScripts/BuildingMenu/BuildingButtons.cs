using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtons : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;
    public GameButtonsHandler gameButtonsHandlerScript;

    [HideInInspector] public int previousBuildingType = -1;

    [HideInInspector] public Object buildingButtonPrefab;

    void Start()
    {
        buildingButtonPrefab = Resources.Load("Prefabs/Button/BuildingButton");
        BuildingTabsOnClick(0);
    }

    public void BuildingTabsOnClick(int tabIndex)
    {
        DestroyPreviousBuildingButtons();
        for (int i = 0; i < gameButtonsHandlerScript.buildingTabButtons.Count; i++)
        {
            ColorBlock buildingTabColors = gameButtonsHandlerScript.buildingTabButtons[i].GetComponent<Button>().colors;
            buildingTabColors = ColorBlock.defaultColorBlock;
            if (i == tabIndex)
            {
                buildingTabColors.normalColor = Color.white;
                SortBuildingButtons(i);
            }
            else
            {
                buildingTabColors.normalColor = new Color32(173, 173, 173, 255);
            }
            gameButtonsHandlerScript.buildingTabButtons[i].GetComponent<Button>().colors = buildingTabColors;
        }
    }

    public void DestroyPreviousBuildingButtons()
    {
        int children = gameButtonsHandlerScript.scrollViewContent.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            Destroy(gameButtonsHandlerScript.scrollViewContent.transform.GetChild(i).gameObject);
        }
    }

    public void SortBuildingButtons(int buildingType)
    {
        for (int i = 0; i < raycastBuildingScript.prefabBlueprints.Count; i++)
        {
            int closureIndex = i;
            if ((int)raycastBuildingScript.prefabBlueprints[i].GetComponent<BuildingTypes>().buildingTypeDropdown == buildingType)
            {
                InstantiatePrefabButton(raycastBuildingScript.prefabBlueprints[i].name.Replace("Blueprint", ""), closureIndex);
            }
        }

        previousBuildingType = buildingType;
    }

    public void InstantiatePrefabButton(string buildingButtonName, int prefabNumber)
    {
        buildingButtonPrefab = Resources.Load("Prefabs/Button/BuildingButton");

        buildingButtonPrefab = Instantiate(buildingButtonPrefab);
        buildingButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = buildingButtonName;
        buildingButtonPrefab.GameObject().transform.SetParent(gameButtonsHandlerScript.scrollViewContent.transform, false);

        buildingButtonPrefab.GetComponent<Button>().onClick.AddListener(() => raycastBuildingScript.SelectBlueprint(true, prefabNumber));
    }
}
