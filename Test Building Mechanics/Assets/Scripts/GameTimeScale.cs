using UnityEngine;

public class GameTimeScale : MonoBehaviour
{
    [HideInInspector] public bool isGamePaused = false;
    public GameObject player;

    public GameObject pauseMenuCanvas;
    public Camera pausedCamera;
    public GameObject cursor;

    public void ToggleGameState(GameObject selectedCanvas)
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isGamePaused = true;
            pausedCamera.transform.SetPositionAndRotation(player.GetComponentInChildren<Camera>().transform.position, player.GetComponentInChildren<Camera>().transform.rotation);
            player.SetActive(false);
            cursor.SetActive(false);

            selectedCanvas.SetActive(true);
            pausedCamera.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isGamePaused = false;
            player.SetActive(true);
            cursor.SetActive(true);

            selectedCanvas.SetActive(false);
            pausedCamera.gameObject.SetActive(false);
        }
    }
}
