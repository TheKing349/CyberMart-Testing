using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonsHandler : MonoBehaviour
{
    public GameDataManager gameDataManagerScript;
    public GameTimeScale gameTimeScaleScript;
    public RaycastBuilding raycastBuildingScript;
    public BuildingButtons buildingButtonsScript;
    public SettingsDataHandler settingsDataHandlerScript;

    public GameObject buildingMenu;
    public GameObject scrollViewContent;
    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;

    [HideInInspector] public List<GameObject> buildingTabButtons;

    #region Pause_Menu_Buttons
    public void ResumeGame()
    {
        gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
    }

    public void Settings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

    public void QuitToMenu()
    {
        gameDataManagerScript.WriteData();

        PreviousSceneManager.instance.prevScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        gameDataManagerScript.WriteData();

        Application.Quit();
    }

    #endregion

    #region Building_Buttons
    /*
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
    */
    #endregion

    #region Settings_Menu_Buttons
    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(settingsDataHandlerScript.qualityDropdown.value);
    }

    public void BackButton()
    {
        settingsDataHandlerScript.SaveSettings();

        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
    }
    #endregion
}