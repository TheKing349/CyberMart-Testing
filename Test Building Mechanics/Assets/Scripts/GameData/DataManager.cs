using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Newtonsoft.Json.UnityConverters.Math;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public BuildingDataHandler buildingDataHandlerScript;
    public PlayerDataHandler playerDataHandlerScript;

    JsonSerializerSettings settings;

    [HideInInspector] public string buildingDataFilePath = "";
    [HideInInspector] public string buildingDirectoryPath = "";

    [HideInInspector] public string playerDataFilePath = "";
    [HideInInspector] public string playerDirectoryPath = "";
    private void Awake()
    {
        settings = new JsonSerializerSettings
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
    }

    private void Start()
    {
        buildingDataHandlerScript.LoadBuildings();

        playerDataHandlerScript.LoadPlayerStats();

        if (File.Exists(buildingDataFilePath))
        {
            InvokeRepeating(nameof(WriteData), 0.0f, 300.0f);
        }
    }

    public void WriteData()
    {
        string buildingJson = JsonConvert.SerializeObject(buildingDataHandlerScript.buildingDataList, Formatting.Indented, settings);
        File.WriteAllText(buildingDataFilePath, buildingJson);

        playerDataHandlerScript.SavePlayerStats();
        string playerJson = JsonConvert.SerializeObject(playerDataHandlerScript.playerData, Formatting.Indented, settings);
        File.WriteAllText(playerDataFilePath, playerJson);
    }

    public void ReadData()
    {
        buildingDataHandlerScript.buildingDataList = JsonConvert.DeserializeObject<List<BuildingData>>(File.ReadAllText(buildingDataFilePath), settings);

        playerDataHandlerScript.playerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(playerDataFilePath), settings);
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
