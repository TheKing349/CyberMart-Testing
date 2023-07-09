using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
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
            gameTimeScaleScript.ToggleGameState(buildingMenuCanvas);
        }
    }
}
