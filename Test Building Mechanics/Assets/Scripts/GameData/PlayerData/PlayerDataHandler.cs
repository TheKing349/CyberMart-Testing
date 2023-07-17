using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    public DataManager dataManagerScript;

    public GameObject player;

    [HideInInspector] public PlayerData playerData = new PlayerData();

    public void SavePlayerStats()
    {
        playerData = new PlayerData
        {
            playerPosition = player.transform.position,
            playerRotation = player.transform.rotation
        };
    }

    public void LoadPlayerStats()
    {
        dataManagerScript.ReadData();

        player.transform.position = playerData.playerPosition;
        player.transform.rotation = playerData.playerRotation;
    }
}
