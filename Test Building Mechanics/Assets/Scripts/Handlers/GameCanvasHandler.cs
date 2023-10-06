using UnityEngine;

public class GameCanvasHandler : MonoBehaviour
{
    public BuildingButtons buildingButtonsScript;
    public GameTimeScale gameTimeScaleScript;
    public SettingsButtonsHandler settingsButtonsHandlerScript;

    public GameObject pauseMenuCanvas;
    public GameObject buildingMenuCanvas;
    public GameObject settingsMenuCanvas;

    public void TogglePauseCanvas()
    {
        if (!settingsMenuCanvas.activeSelf)
        {
            buildingMenuCanvas.SetActive(false);
            buildingButtonsScript.DeselectBlueprint();
            gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
        }
        else
        {
            settingsButtonsHandlerScript.SaveAndApplyButton();
            settingsMenuCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
        }
    }

    public void ToggleBuildingCanvas()
    {
        if (!pauseMenuCanvas.activeSelf)
        {
            if (!buildingMenuCanvas.activeSelf)
            {
                buildingButtonsScript.SelectBlueprint(buildingButtonsScript.currentPrefabInt);
            }
            buildingMenuCanvas.SetActive(!buildingMenuCanvas.activeSelf);
        }
    }
}
