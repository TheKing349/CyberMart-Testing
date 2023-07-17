using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonsHandler : MonoBehaviour
{
    public DataManager dataManagerScript;
    public GameTimeScale gameTimeScaleScript;
    public RaycastBuilding raycastBuildingScript;
    public BuildingButtons buildingButtonsScript;

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
        dataManagerScript.WriteData();

        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        dataManagerScript.WriteData();

        Application.Quit();
    }

    #endregion

    #region Building_Buttons
    private void Awake()
    {
        BuildingTabsListAdder();
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
                child.GetComponent<Button>().onClick.AddListener(() => buildingButtonsScript.BuildingTabsOnClick(closureIndex));
            }
        }
    }
    #endregion
}