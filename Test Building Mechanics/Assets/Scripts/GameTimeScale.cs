using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeScale : MonoBehaviour
{
    [HideInInspector] public bool isGamePaused = false;
    public GameObject player;

    public GameObject pauseMenuCanvas;
    private Camera pauseMenuCamera;
    public GameObject cursor;

    private void Start()
    {
        pauseMenuCamera = pauseMenuCanvas.GetComponentInChildren<Camera>();   
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        isGamePaused = true;
        pauseMenuCamera.transform.position = player.GetComponentInChildren<Camera>().transform.position;
        pauseMenuCamera.transform.rotation = player.GetComponentInChildren<Camera>().transform.rotation;
        player.SetActive(false);
        cursor.SetActive(false);

        pauseMenuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        isGamePaused = false;
        player.SetActive(true);
        cursor.SetActive(true);

        pauseMenuCanvas.SetActive(false);
    }
}
