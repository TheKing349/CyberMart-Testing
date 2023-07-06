using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public GameTimeScale gameTimeScale;

    public GameObject pauseMenuCanvas;
    public GameObject buildingMenuCanvas;

    public void TogglePauseCanvas()
    {
        gameTimeScale.ToggleGameState(pauseMenuCanvas);
    }

    public void ToggleBuildingCanvas()
    {
        gameTimeScale.ToggleGameState(buildingMenuCanvas);
    }
}
