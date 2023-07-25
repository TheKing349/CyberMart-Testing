using UnityEngine;

public class PreviousSceneManager : MonoBehaviour
{
    public static PreviousSceneManager instance = null;

    public int prevScene = -1;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
