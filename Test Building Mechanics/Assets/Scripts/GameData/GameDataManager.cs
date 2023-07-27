using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Newtonsoft.Json.UnityConverters.Math;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public BuildingDataHandler buildingDataHandlerScript;
    public PlayerDataHandler playerDataHandlerScript;
    public SettingsDataHandler settingsDataHandlerScript;

    private JsonSerializerSettings serializerSettings;

    [HideInInspector] public string buildingDataFilePath = "";
    [HideInInspector] public string buildingDirectoryPath = "";

    [HideInInspector] public string playerDataFilePath = "";
    [HideInInspector] public string playerDirectoryPath = "";

    [HideInInspector] public string settingsDataFilePath = "";
    [HideInInspector] public string settingsDirectoryPath = "";

    private void Awake()
    {
        serializerSettings = new JsonSerializerSettings
        {
            Converters = new[]
            {
                new Vector3Converter(),
            },
            ContractResolver = new UnityTypeContractResolver(),
        };

        buildingDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Building" + Path.DirectorySeparatorChar;
        buildingDataFilePath = buildingDirectoryPath + "buildingData.json";
        CreateDirectoryAndFile(buildingDirectoryPath, buildingDataFilePath);

        playerDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Player" + Path.DirectorySeparatorChar;
        playerDataFilePath = playerDirectoryPath + "playerData.json";
        CreateDirectoryAndFile(playerDirectoryPath, playerDataFilePath);

        settingsDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Settings" + Path.DirectorySeparatorChar;
        settingsDataFilePath = settingsDirectoryPath + "settings.json";
        CreateDirectoryAndFile(settingsDirectoryPath, settingsDataFilePath);
    }

    private void Start()
    {
        if (File.ReadAllText(buildingDataFilePath) != "")
        {
            buildingDataHandlerScript.LoadBuildings();
        }
        if (File.ReadAllText(playerDataFilePath) != "")
        {
            playerDataHandlerScript.LoadPlayerStats();
        }
        if (File.ReadAllText(settingsDataFilePath) != "")
        {
            settingsDataHandlerScript.LoadSettings();
        }

        if (File.Exists(buildingDataFilePath))
        {
            InvokeRepeating(nameof(WriteData), 0.0f, 300.0f);
        }
    }

    public void WriteData()
    {
        string buildingJson = JsonConvert.SerializeObject(buildingDataHandlerScript.buildingDataList, Formatting.Indented, serializerSettings);
        File.WriteAllText(buildingDataFilePath, buildingJson);

        playerDataHandlerScript.SavePlayerStats();
        string playerJson = JsonConvert.SerializeObject(playerDataHandlerScript.playerData, Formatting.Indented, serializerSettings);
        File.WriteAllText(playerDataFilePath, playerJson);

        string settingsJson = JsonConvert.SerializeObject(settingsDataHandlerScript.settingsData, Formatting.Indented, serializerSettings);
        File.WriteAllText(settingsDataFilePath, settingsJson);
    }

    public void ReadData()
    {
        buildingDataHandlerScript.buildingDataList = JsonConvert.DeserializeObject<List<BuildingData>>(File.ReadAllText(buildingDataFilePath), serializerSettings);

        playerDataHandlerScript.playerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(playerDataFilePath), serializerSettings);

        settingsDataHandlerScript.settingsData = JsonConvert.DeserializeObject<SettingsData>(File.ReadAllText(settingsDataFilePath), serializerSettings);
    }

    public void CreateDirectoryAndFile(string directoryPath, string filePath)
    {
        if ((!File.Exists(filePath) && !Directory.Exists(directoryPath)))
        {
            Directory.CreateDirectory(directoryPath);

            File.Create(filePath);
        }
        else if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }
}
