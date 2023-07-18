using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsHandler : MonoBehaviour
{
    public void StartGame()
    {
        Indestructable.instance.prevScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        Indestructable.instance.prevScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
