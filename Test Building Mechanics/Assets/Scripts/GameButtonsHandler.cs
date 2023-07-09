using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonsHandler : MonoBehaviour
{
    public GameTimeScale gameTimeScaleScript;
    public RaycastBuilding raycastBuildingScript;

    private Object currentBuildingButton;

    public GameObject buildingMenu;
    public GameObject scrollViewContent;
    public GameObject pauseMenuCanvas;

    [HideInInspector] public List<GameObject> buildingTabButtons;

    #region Pause_Menu_Buttons
    public void ResumeGame()
    {
        gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Building_Buttons
    private void Start()
    {
        BuildingTabsListAdder();
        BuildingTabsOnClick(0);
    }

    private void BuildingTabsListAdder()
    {
        int children = buildingMenu.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            int closureIndex = i;

            GameObject child = buildingMenu.transform.GetChild(i).gameObject;
            if (child.name.Contains("BuildingTab"))
            {
                buildingTabButtons.Add(child);
                child.GetComponent<Button>().onClick.AddListener(() => BuildingTabsOnClick(closureIndex));
            }
        }
    }

    private void BuildingTabsOnClick(int tabIndex)
    {
        DestroyPreviousBuildingButtons();
        for (int i = 0; i < buildingTabButtons.Count; i++)
        {
            ColorBlock buildingTabColors = buildingTabButtons[i].GetComponent<Button>().colors;
            buildingTabColors = ColorBlock.defaultColorBlock;
            if (i == tabIndex)
            {
                buildingTabColors.normalColor = Color.white;
                PrefabButtonsInstantiator(i);
            }
            else
            {
                buildingTabColors.normalColor = new Color32(173, 173, 173, 255);
            }
            buildingTabButtons[i].GetComponent<Button>().colors = buildingTabColors;
        }
    }

    private void DestroyPreviousBuildingButtons()
    {
        int children = scrollViewContent.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            Destroy(scrollViewContent.transform.GetChild(i).gameObject);
        }
    }

    private void PrefabButtonsInstantiator(int buildingType)
    {
        currentBuildingButton = Resources.Load("Prefabs/Button/BuildingButton");

        for (int i = 0; i < raycastBuildingScript.prefabBlueprints.Count; i++)
        {
            int closureIndex = i;
            if ((int)raycastBuildingScript.prefabBlueprints[i].GetComponent<BuildingTypes>().buildingTypeDropdown == buildingType)
            {
                currentBuildingButton = Instantiate(currentBuildingButton);
                currentBuildingButton.GetComponentInChildren<TextMeshProUGUI>().text = raycastBuildingScript.prefabBlueprints[i].name.Replace("Blueprint", "");
                currentBuildingButton.GameObject().transform.SetParent(scrollViewContent.transform, false);

                currentBuildingButton.GetComponent<Button>().onClick.AddListener(() => raycastBuildingScript.SelectBlueprint(closureIndex));
            }
        }
    }
    #endregion
}
