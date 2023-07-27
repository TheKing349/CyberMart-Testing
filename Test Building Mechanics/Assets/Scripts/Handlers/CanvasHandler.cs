using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public BuildingButtons buildingButtonsScript;
    public GameTimeScale gameTimeScaleScript;

    public GameObject pauseMenuCanvas;
    public GameObject buildingMenuCanvas;

    public void TogglePauseCanvas()
    {
        if (!buildingMenuCanvas.activeSelf)
        {
            gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
        }
    }

    public void ToggleBuildingCanvas()
    {
        if (!pauseMenuCanvas.activeSelf)
        {
            if (!buildingMenuCanvas.activeSelf)
            {
                buildingButtonsScript.SelectBlueprint(0);
            }
            buildingMenuCanvas.SetActive(!buildingMenuCanvas.activeSelf);
        }
    }
}
