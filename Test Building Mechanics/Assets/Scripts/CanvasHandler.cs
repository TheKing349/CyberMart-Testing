using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public GameTimeScale gameTimeScaleScript;

    public GameObject pauseMenuCanvas;
    public GameObject buildingMenuCanvas;

    public void TogglePauseCanvas()
    {
        gameTimeScaleScript.ToggleGameState(pauseMenuCanvas);
    }

    public void ToggleBuildingCanvas()
    {
        gameTimeScaleScript.ToggleGameState(buildingMenuCanvas);
    }
}
