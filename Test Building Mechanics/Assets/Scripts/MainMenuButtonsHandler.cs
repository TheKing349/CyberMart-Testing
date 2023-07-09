using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TestBuildingMechanicsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
